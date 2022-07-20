using VPN.Models;

namespace VPN.Contracts
{
    public interface IVPNRepository
    {
        public Task<RangeModel> GetRangeByPrivateRange(int companyID, string number);
        public Task<RangeModel> GetRangeByCLIRange(int companyID, string number);
        public Task<ScreeningDataModel> GetScreeningDataByNumber(string screeningIDs, string number);
        public Task<UserModel> GetUserByCLI(string cli);
        public Task<UserModel> GetUserByPrivateNumber(int companyID, string number);
        public Task<UserModel> GetUserByCalledNumber(int companyID, string number);
    }
}
