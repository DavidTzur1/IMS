
using Dapper;
using System.Data;
using VPNServer.Context;
using VPNServer.Contracts;
using VPNServer.Dto;
using VPNServer.Entities;
using Range = VPNServer.Entities.Range;

namespace VPNServer.Repository
{
    public class RangeRepository : IRangeRepository
    {
        private readonly DapperContext _context;
        public RangeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Range> CreateRange(RangeForCreationDto range)
        {
            var procedureName = "CreateRange";
            var parameters = new DynamicParameters();           
            parameters.Add("Name", range.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("PABXId", range.PABXId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PublicRange", range.PublicRange, DbType.String, ParameterDirection.Input);
            parameters.Add("PrivateRange", range.PrivateRange, DbType.String, ParameterDirection.Input);
            parameters.Add("PublicToPrivateDigitsRemove", range.PublicToPrivateDigitsRemove, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PublicToPrivatePrefixAdd", range.PublicToPrivatePrefixAdd, DbType.String, ParameterDirection.Input);
            parameters.Add("PrivateToDestDigitsRemove", range.PrivateToDestDigitsRemove, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PrivateToDestPrefixAdd", range.PrivateToDestPrefixAdd, DbType.String, ParameterDirection.Input);
            parameters.Add("PrivateToChargingDigitsRemove", range.PrivateToChargingDigitsRemove, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PrivateToChargingPrefixAdd", range.PrivateToChargingPrefixAdd, DbType.String, ParameterDirection.Input);
            parameters.Add("IsVirtualOnNet", range.IsVirtualOnNet, DbType.Boolean, ParameterDirection.Input);


            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var createdRange = new Range
                {
                    Id = id,
                    Name = range.Name,
                    PABXId = range.PABXId,
                    PublicRange = range.PublicRange,
                    PrivateRange = range.PrivateRange, 
                    PublicToPrivateDigitsRemove = range.PublicToPrivateDigitsRemove,
                    PublicToPrivatePrefixAdd = range.PublicToPrivatePrefixAdd,
                    PrivateToDestDigitsRemove = range.PrivateToDestDigitsRemove,
                    PrivateToDestPrefixAdd = range.PrivateToDestPrefixAdd,
                    PrivateToChargingDigitsRemove = range.PrivateToChargingDigitsRemove,
                    PrivateToChargingPrefixAdd = range.PrivateToChargingPrefixAdd,
                    IsVirtualOnNet = range.IsVirtualOnNet


                };
                return createdRange;
            }
        }

        public async Task DeleteRange(int id)
        {
            var procedureName = "DeleteRange";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Range>> GetRangesByPABXId(int pabxID)
        {
            var procedureName = "GetRangesByPABXId";
            using (var connection = _context.CreateConnection())
            {
                var ranges = await connection.QueryAsync<Range>(procedureName, new { pabxID }, commandType: CommandType.StoredProcedure);
                return ranges.ToList();
            }
        }

        public async Task<Range> GetRange(int id)
        {
            var procedureName = "GetRange";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<Range>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

       
        public async Task UpdateRange(int id, RangeForUpdateDto range)
        {
            var procedureName = "UpdateRange";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", range.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("PABXId", range.PABXId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PublicRange", range.PublicRange, DbType.String, ParameterDirection.Input);
            parameters.Add("PrivateRange", range.PrivateRange, DbType.String, ParameterDirection.Input);
            parameters.Add("PublicToPrivateDigitsRemove", range.PublicToPrivateDigitsRemove, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PublicToPrivatePrefixAdd", range.PublicToPrivatePrefixAdd, DbType.String, ParameterDirection.Input);
            parameters.Add("PrivateToDestDigitsRemove", range.PrivateToDestDigitsRemove, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PrivateToDestPrefixAdd", range.PrivateToDestPrefixAdd, DbType.String, ParameterDirection.Input);
            parameters.Add("PrivateToChargingDigitsRemove", range.PrivateToChargingDigitsRemove, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PrivateToChargingPrefixAdd", range.PrivateToChargingPrefixAdd, DbType.String, ParameterDirection.Input);
            parameters.Add("IsVirtualOnNet", range.IsVirtualOnNet, DbType.Boolean, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}



