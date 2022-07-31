
using Dapper;
using System.Data;
using VPN.Context;
using VPN.Contracts;
using VPN.DTO;
using VPN.Entities;

namespace VPN.Repository
{
    public class RangeRepository : IRangeRepository
    {
        private readonly DapperContext _context;
        public RangeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Entities.Range> CreateRange(RangeDTO range)
        {
            var procedureName = "CreateRange";
            var parameters = new DynamicParameters();           
            parameters.Add("Name", range.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("PABXID", range.PABXID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("CLIRange", range.CLIRange, DbType.String, ParameterDirection.Input);
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
                var createdRange = new Entities.Range
                {
                    ID = id,
                    Name = range.Name,
                    PABXID = range.PABXID,
                    CLIRange = range.CLIRange,
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

        public async Task<IEnumerable<Entities.Range>> GetRangesByPABXID(int pabxID)
        {
            var procedureName = "GetRangesByPABXID";
            using (var connection = _context.CreateConnection())
            {
                var ranges = await connection.QueryAsync<Entities.Range>(procedureName, new { pabxID }, commandType: CommandType.StoredProcedure);
                return ranges.ToList();
            }
        }

        public async Task<Entities.Range> GetRange(int id)
        {
            var procedureName = "GetRange";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<Entities.Range>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

       
        public async Task UpdateRange(int id, RangeDTO range)
        {
            var procedureName = "UpdateRange";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Int32);
            parameters.Add("Name", range.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("PABXID", range.PABXID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("CLIRange", range.CLIRange, DbType.String, ParameterDirection.Input);
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



