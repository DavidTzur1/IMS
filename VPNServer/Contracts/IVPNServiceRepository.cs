using VPNServer.Models;

namespace VPNServer.Contracts
{
    public interface IVPNServiceRepository
    {
        public Task<UserModel> GetUserModelByCLI(string cli);
        public Task<ScreeningItemModel> BestMatchScreeningItemByNumber(string screeningIds, string number);
        public Task<UserModel> GetUserModelByCalledNumber(int companyId,string calledNumber);
        public Task<UserModel> GetUserModelByPrivateNumber(int companyId, string calledNumber);
        public Task<RangeModel> GetRangeModelByPrivateRange(int companyId, string calledNumber);
        public Task<RangeModel> GetRangeModelByPublicRange(int companyId, string calledNumber);


        
    }
}
