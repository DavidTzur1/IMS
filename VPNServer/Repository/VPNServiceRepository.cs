using Dapper;
using System.Data;
using VPNServer.Context;
using VPNServer.Contracts;
using VPNServer.Dto;
using VPNServer.Entities;
using VPNServer.Models;

namespace VPNServer.Repository
{
    public class VPNServiceRepository : IVPNServiceRepository
    {
        private readonly DapperContext _context;
        public VPNServiceRepository(DapperContext context)
        {
            _context = context;
        }



        public async Task<UserModel> GetUserModelByCLI(string cli)
        {
            var procedureName = "GetUserModelByCLI";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<UserModel>(procedureName, new { cli }, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<ScreeningItemModel> BestMatchScreeningItemByNumber(string screeningIds, string number)
        {
            var procedureName = "BestMatchScreeningItemByNumber";
            var parameters = new DynamicParameters();
            parameters.Add("screeningIDs", screeningIds, DbType.String, ParameterDirection.Input);
            parameters.Add("number", number, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var screeningData = await connection.QueryFirstOrDefaultAsync<ScreeningItemModel>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return screeningData;
            }

        }

        public async Task<UserModel> GetUserModelByCalledNumber(int companyId,string number)
        {
            var procedureName = "GetUserModelByCalledNumber";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<UserModel>(procedureName, new { companyId, number }, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<UserModel> GetUserModelByPrivateNumber(int companyId, string number)
        {
            var procedureName = "GetUserModelByPrivateNumber";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<UserModel>(procedureName, new { companyId, number }, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<RangeModel> GetRangeModelByPublicRange(int companyId, string number)
        {
            var procedureName = "GetRangeModelByPublicRange";
            using (var connection = _context.CreateConnection())
            {
                var range = await connection.QuerySingleOrDefaultAsync<RangeModel>(procedureName, new { companyId, number }, commandType: CommandType.StoredProcedure);
                return range;
            }
        }

        public async Task<RangeModel> GetRangeModelByPrivateRange(int companyId, string number)
        {
            var procedureName = "GetRangeModelByPrivateRange";
            using (var connection = _context.CreateConnection())
            {
                var range = await connection.QuerySingleOrDefaultAsync<RangeModel>(procedureName, new { companyId, number }, commandType: CommandType.StoredProcedure);
                return range;
            }
        }

    }
}

