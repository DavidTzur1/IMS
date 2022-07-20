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
           
       

    }
}


