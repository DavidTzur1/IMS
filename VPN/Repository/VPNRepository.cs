using Dapper;
using System.Data;
using VPN.Context;
using VPN.Contracts;
using VPN.Models;

namespace VPN.Repository
{
    public class VPNRepository : IVPNRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<VPNRepository> _logger;
        public VPNRepository(DapperContext context, ILogger<VPNRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserModel> GetUserByCLI(string cli)
        {
            var procedureName = "GetUserByCLI";
            var parameters = new DynamicParameters();
            parameters.Add("cli", cli, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<UserModel>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
        public async Task<RangeModel> GetRangeByPrivateRange(int companyID, string number)
        {
            var procedureName = "GetRangeByPrivateRange";
            var parameters = new DynamicParameters();
            parameters.Add("companyID", companyID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("number", number, DbType.String, ParameterDirection.Input);
           
                using (var connection = _context.CreateConnection())
                {
                    var range = await connection.QueryFirstOrDefaultAsync<RangeModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return range;
                }
            
        }
        public async Task<RangeModel> GetRangeByCLIRange(int companyID, string number)
        {
            var procedureName = "GetRangeByCLIRange";
            var parameters = new DynamicParameters();
            parameters.Add("companyID", companyID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("number", number, DbType.String, ParameterDirection.Input);
           
                using (var connection = _context.CreateConnection())
                {
                    var range = await connection.QueryFirstOrDefaultAsync<RangeModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return range;
                }
           
        }
        public async Task<ScreeningDataModel> GetScreeningDataByNumber(string screeningIDs, string number)
        {
            var procedureName = "GetScreeningDataByNumber";
            var parameters = new DynamicParameters();
            parameters.Add("screeningIDs", screeningIDs, DbType.String, ParameterDirection.Input);
            parameters.Add("number", number, DbType.String, ParameterDirection.Input);
            
                using (var connection = _context.CreateConnection())
                {
                    var screeningData = await connection.QueryFirstOrDefaultAsync<ScreeningDataModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return screeningData;
                }
           
        }
        public async Task<UserModel> GetUserByPrivateNumber(int companyID, string number)
        {
            var procedureName = "GetUserByPrivateNumber";
            var parameters = new DynamicParameters();
            parameters.Add("companyID", companyID, DbType.String, ParameterDirection.Input);
            parameters.Add("number", number, DbType.String, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var user = await connection.QueryFirstOrDefaultAsync<UserModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return user;
                }
           
        }
        public async Task<UserModel> GetUserByCalledNumber(int companyID, string number)
        {
            var procedureName = "GetUserByCalledNumber";
            var parameters = new DynamicParameters();
            parameters.Add("companyID", companyID, DbType.String, ParameterDirection.Input);
            parameters.Add("number", number, DbType.String, ParameterDirection.Input);
            
                using (var connection = _context.CreateConnection())
                {
                    var user = await connection.QueryFirstOrDefaultAsync<UserModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return user;
                }
            }

        public async Task<IEnumerable<CompanyModel>> GetCompanies()
        {
            var procedureName = "GetCompanies";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<CompanyModel>(procedureName, commandType: CommandType.StoredProcedure);
                return companies.ToList();
            }
        }

        public async Task<CompanyModel> GetCompany(int id)
        {
            var procedureName = "GetCompany";
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<CompanyModel>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return company;
            }
        }

        public async Task CreateCompany(CompanyModel company)
        {
            var procedureName = "CreateCompany";
            var parameters = new DynamicParameters();
            parameters.Add("companyID", company.CompanyID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("companyName", company.CompanyName, DbType.String, ParameterDirection.Input);
            parameters.Add("billingID", company.BillingID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("isDeactivated", company.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("isForceOnNet", company.IsForceOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("isCompanyCallsOnly", company.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("outgoingCallBarringList", company.OutgoingCallBarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("outgoingCallAllowanceList", company.OutgoingCallAllowanceList, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateCompany(int id, CompanyModel company)
        {
            var procedureName = "UpdateCompany";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("companyName", company.CompanyName, DbType.String, ParameterDirection.Input);
            parameters.Add("billingID", company.BillingID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("isDeactivated", company.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("isForceOnNet", company.IsForceOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("isCompanyCallsOnly", company.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("outgoingCallBarringList", company.OutgoingCallBarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("outgoingCallAllowanceList", company.OutgoingCallAllowanceList, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteCompany(int id)
        {
            //var query = "DELETE FROM Companies WHERE Id = @Id";
            var procedureName = "UpdateCompany";
            using (var connection = _context.CreateConnection())
            {
                //await connection.ExecuteAsync(query, new { id });
                await connection.ExecuteAsync(procedureName, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GroupModel>> GetGroups(int companyID)
        {
            var procedureName = "GetGroups";
            using (var connection = _context.CreateConnection())
            {
                var groups = await connection.QueryAsync<GroupModel>(procedureName, new { companyID }, commandType: CommandType.StoredProcedure);
                return groups.ToList();
            }
        }



    }
}


