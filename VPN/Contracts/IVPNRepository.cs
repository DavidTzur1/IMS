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

        //////////////Prov/////////////////////////

        public Task<IEnumerable<CompanyModel>> GetCompanies();
        public Task<CompanyModel> GetCompany(int id);
        public Task CreateCompany(CompanyModel company);
        public Task UpdateCompany(int id, CompanyModel company);
        public Task DeleteCompany(int id);
    }
}
