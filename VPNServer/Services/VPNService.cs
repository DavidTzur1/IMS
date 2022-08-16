using SIPSorcery.SIP;
using System.Net;
using VPNServer.Contracts;
using VPNServer.Models;

namespace VPNServer.Services
{
    public class VPNService : BackgroundService
    {
        private readonly ILogger _logger = null;
        private readonly IVPNServiceRepository _repository;

        private static SIPTransport _sipTransport;
        private static int _listenPort = SIPConstants.DEFAULT_SIP_PORT;
        //private static int _listenPort = 6600;

        /////////Status service///////////////////////////////////

        public enum Status
        {
            Success,
            NotFound,
            Deactivated,
            Barring,
            NotImplemented

        }

        public VPNService(IVPNServiceRepository repository, ILogger<VPNService> logger)
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
                data.CallId = sipRequest.Header.CallId;
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
                    var user = await _repository.GetUserModelByCLI(data.NationalCLI);
                    data.CompanyId = user != null ? user.CompanyId : -1;
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
                    else if (!string.IsNullOrEmpty(user.BarringList.Trim()))
                    {
                        _logger.LogDebug($"BarringList:{user.BarringList} NationalDN:{data.NationalDN}");
                        var barring = await _repository.BestMatchScreeningItemByNumber(user.BarringList, data.NationalDN);
                        if (barring != null)
                        {

                            var allowance = string.IsNullOrEmpty(user.AllowanceList) ? null : await _repository.BestMatchScreeningItemByNumber(user.AllowanceList, data.NationalDN);
                            if (allowance == null)
                            {
                                data.Info = $"The barring number is {barring.Number} from screeingID {barring.ScreeningId} ({barring.ScreeningName})";
                                data.Status = Status.Barring.ToString();
                                await _sipTransport.SendResponseAsync(SIPResponse.GetResponse(sipRequest, SIPResponseStatusCodesEnum.RequestTerminated, null));
                                return;
                            }

                        }
                    }

                    ////////////////////Translation//////////////////////////////////////////////////////////////////////////////////////////////
                    UserModel userDN;
                    RangeModel range;
                    string billingInfo = string.Empty;
                    
                   
                    if (data.DN.StartsWith("0") || data.DN.StartsWith("1") || data.DN.StartsWith("+") || data.DN.StartsWith("*") || data.DN.Length > 8 )
                    {
                        
                        if ((userDN = await _repository.GetUserModelByCalledNumber(user.CompanyId, data.NationalDN)) != null)
                        {
                            billingInfo = userDN.IsForceOnNet || userDN.IsVirtualOnNet ? "1" : "0";
                            data.DNTranslation = data.DN;

                        }
                        else if((range = await _repository.GetRangeModelByPublicRange(user.CompanyId, data.NationalDN)) != null)
                        {
                            billingInfo = range.IsForceOnNet  ? "1" : "0";
                            data.DNTranslation = data.DN;
                        }
                        else
                        {
                            billingInfo = "0";
                            data.DNTranslation = data.DN;
                        }
                       
                    }
                    else 
                    {
                        if ((userDN = await _repository.GetUserModelByPrivateNumber(user.CompanyId, data.NationalDN)) != null)
                        {
                            billingInfo = userDN.IsForceOnNet || userDN.IsVirtualOnNet ? "1" : "0";
                            data.DNTranslation = userDN.CalledNumber;
                        }
                        else if((range = await _repository.GetRangeModelByPrivateRange(user.CompanyId, data.NationalDN)) != null)
                        {
                            billingInfo = range.IsForceOnNet ? "1" : "0";
                            data.DNTranslation = range.PrivateToDestPrefixAdd + data.DN.Substring(range.PrivateToDestDigitsRemove);
                           
                        }
                        else
                        {
                            data.DNTranslation =data.DN.ToString();
                            billingInfo = "0";
                        }

                        
                    }

                    sipRequest.Header.Routes.PopRoute();
                    sipRequest.Header.UnknownHeaders.Add($"Billing-Info:{billingInfo}");
                    sipRequest.URI.User = data.InternationalDNTranslation;
                    await _sipTransport.SendRequestAsync(remoteEndPoint, sipRequest);
                    data.Status = Status.Success.ToString();
                    return;

                }
                else if (sipRequest.Method == SIPMethodsEnum.OPTIONS)
                {
                    data.Method = "OPTIONS";
                    SIPResponse optionsResponse = SIPResponse.GetResponse(sipRequest, SIPResponseStatusCodesEnum.Ok, null);
                    await _sipTransport.SendResponseAsync(optionsResponse);
                    data.Status = Status.Success.ToString(); ;
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
                _logger.LogError(ex.Message, ex);
            }
            finally
            {
                _logger.LogInformation(data.ToString());
            }
            //throw new NotImplementedException();
        }
    }
}
