using SIPSorcery.SIP;
using System.Net;
using VPN.Contracts;
using VPN.Models;

namespace VPN.Services
{
    public class VPNService : BackgroundService
    {
        private readonly ILogger _logger = null;
        private readonly IVPNRepository _repository;

        private static SIPTransport _sipTransport;
        private static int _listenPort = SIPConstants.DEFAULT_SIP_PORT;


        /////////Status service///////////////////////////////////

        public enum Status
        {
            Success,
            NotFound,
            Deactivated,
            Barring,
            NotImplemented

        }
      

        /// <summary>
        /// /
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public VPNService(IVPNRepository repository, ILogger<VPNService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start ExecuteAsync");

            _sipTransport = new SIPTransport();

            // Wire up the transport layer so SIP requests and responses have somewhere to go.
            _sipTransport.SIPTransportRequestReceived += SIPTransportRequestReceived;
            _sipTransport.SIPTransportResponseReceived += SIPTransportResponseReceived;

            // Use default options to set up a SIP channel.
            var sipChannel = new SIPUDPChannel(new IPEndPoint(IPAddress.Any, _listenPort));
            _sipTransport.AddSIPChannel(sipChannel);
            var ipv6SipChannel = new SIPUDPChannel(new IPEndPoint(IPAddress.IPv6Any, _listenPort));
            _sipTransport.AddSIPChannel(ipv6SipChannel);


            return Task.CompletedTask;
        }

        /// <summary>
        /// Handler for processing incoming SIP requests.
        /// </summary>
        /// <param name="localSIPEndPoint">The end point the request was received on.</param>
        /// <param name="remoteEndPoint">The end point the request came from.</param>
        /// <param name="sipRequest">The SIP request received.</param>

        private Task SIPTransportResponseReceived(SIPEndPoint localSIPEndPoint, SIPEndPoint remoteEndPoint, SIPResponse sipResponse)
        {
            throw new NotImplementedException();
        }

        private async Task SIPTransportRequestReceived(SIPEndPoint localSIPEndPoint, SIPEndPoint remoteEndPoint, SIPRequest sipRequest)
        {
            VPNServiceModel data = new VPNServiceModel();
            try
            {
                data.CallID = sipRequest.Header.CallId;
                if (sipRequest.Method == SIPMethodsEnum.BYE)
                {
                    throw new NotImplementedException();
                }
                else if (sipRequest.Method == SIPMethodsEnum.CANCEL)
                {
                    throw new NotImplementedException();
                }
                else if (sipRequest.Method == SIPMethodsEnum.INVITE)
                {
                    data.Method = "INVITE";
                    // Response 100 Trying
                    SIPResponse optionsResponse = SIPResponse.GetResponse(sipRequest, SIPResponseStatusCodesEnum.Trying, null);
                    await _sipTransport.SendResponseAsync(optionsResponse);

                    //Get CLI from the P-Asserted-Identity and DN from Reqest Line
                    string pai = sipRequest.Header.GetUnknownHeaderValue("P-Asserted-Identity");
                    data.CLI = Helpers.FindTextBetween(pai, "sip:", "@");
                    data.DN = sipRequest.URI.User;

                    //Get the User info from the repository if not found send Terminated(487) 
                    var user = await _repository.GetUserByCLI(data.NationalCLI);
                    data.CompanyID = user!=null?user.CompanyID:-1;
                    if (user == null)
                    {
                        data.Status = Status.NotFound.ToString();
                        await _sipTransport.SendResponseAsync(SIPResponse.GetResponse(sipRequest, SIPResponseStatusCodesEnum.RequestTerminated, null));
                        return;
                    }

                    else if (user.IsDeactivated)
                    {
                        data.Status = Status.Deactivated.ToString();
                        await _sipTransport.SendResponseAsync(SIPResponse.GetResponse(sipRequest, SIPResponseStatusCodesEnum.RequestTerminated, null));
                        return;
                    }
                    else if (!string.IsNullOrEmpty(user.OutgoingCallBarringList.Trim()))
                    {
                        _logger.LogDebug($"OutgoingCallBarringList:{user.OutgoingCallBarringList} NationalDN:{data.NationalDN}");
                        var barring = await _repository.GetScreeningDataByNumber(user.OutgoingCallBarringList, data.NationalDN);
                        if (barring != null)
                        {
                            
                            var allowance = string.IsNullOrEmpty(user.OutgoingCallAllowanceList) ? null : await _repository.GetScreeningDataByNumber(user.OutgoingCallAllowanceList, data.NationalDN);
                            if (allowance == null)
                            {
                                data.Info = $"The barring number is {barring.Number} from screeingID {barring.ScreeningID} ({barring.ScreeningName})";
                                data.Status =Status.Barring.ToString();
                                await _sipTransport.SendResponseAsync(SIPResponse.GetResponse(sipRequest, SIPResponseStatusCodesEnum.RequestTerminated, null));
                                return;
                            }

                        }
                    }

                    ////////////////////Translation//////////////////////////////////////////////////////////////////////////////////////////////
                    UserModel userDN;
                    RangeModel range;
                    if (data.DN.StartsWith("0") || data.DN.StartsWith("1") || data.DN.StartsWith("+") || data.DN.StartsWith("*"))
                    {
                        sipRequest.Header.Routes.PopRoute();
                        data.DNTranslation = data.DN;
                        await _sipTransport.SendRequestAsync(remoteEndPoint, sipRequest);
                        data.Status = Status.Success.ToString();
                        return;
                    }
                    else if ((userDN = await _repository.GetUserByPrivateNumber(user.CompanyID, data.NationalDN)) != null)
                    {
                        data.DNTranslation = userDN.CalledNumber;
                        sipRequest.Header.Routes.PopRoute();
                        sipRequest.URI.User = data.InternationalDNTranslation;
                        await _sipTransport.SendRequestAsync(remoteEndPoint, sipRequest);
                        data.Status = EndReasonModel.Success.ToString();
                        return;
                    }
                    else if ((range = await _repository.GetRangeByPrivateRange(user.CompanyID, data.NationalDN)) != null)
                    {
                        data.DNTranslation = range.PrivateToDestPrefixAdd + data.DN.Substring(range.PrivateToDestDigitsRemove);
                        sipRequest.Header.Routes.PopRoute();
                        sipRequest.URI.User = data.InternationalDNTranslation;
                        await _sipTransport.SendRequestAsync(remoteEndPoint, sipRequest);
                        data.Status = EndReasonModel.Success.ToString();
                        return;
                    }
                    else
                    {
                        sipRequest.Header.Routes.PopRoute();
                        await _sipTransport.SendRequestAsync(remoteEndPoint, sipRequest);
                        data.Status = EndReasonModel.Success.ToString();
                    }
                }
                else if (sipRequest.Method == SIPMethodsEnum.OPTIONS)
                {
                    data.Method = "OPTIONS";
                    SIPResponse optionsResponse = SIPResponse.GetResponse(sipRequest, SIPResponseStatusCodesEnum.Ok, null);
                    await _sipTransport.SendResponseAsync(optionsResponse);
                    data.Status = EndReasonModel.Success.ToString();
                }
                else if (sipRequest.Method == SIPMethodsEnum.ACK)
                {
                    data.Method = "ACK";
                }

                else
                {
                    _logger.LogDebug("SIP " + sipRequest.Method + " request received but no processing has been set up for it, rejecting.");
                }




            }
            catch (NotImplementedException)
            {
               // _logger.LogDebug(sipRequest.Method + " request processing not implemented for " + sipRequest.URI.ToParameterlessString() + " from " + remoteEndPoint + ".");
                data.Method = sipRequest.Method.ToString();
                data.Status = Status.NotImplemented.ToString();

                SIPResponse notImplResponse = SIPResponse.GetResponse(sipRequest, SIPResponseStatusCodesEnum.NotImplemented, null);
                await _sipTransport.SendResponseAsync(notImplResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
            }
            finally
            {
                _logger.LogInformation(data.ToString());
            }
            //throw new NotImplementedException();
        }
    }
}
