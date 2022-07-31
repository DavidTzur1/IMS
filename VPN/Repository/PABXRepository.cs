using Dapper;
using System.Data;
using VPN.Context;
using VPN.Contracts;
using VPN.DTO;
using VPN.Entities;

namespace VPN.Repository
{
    public class PABXRepository : IPABXRepository
    {
        private readonly DapperContext _context;
        public PABXRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<PABX> CreatePABX(PABXDTO pabx)
        {
            var procedureName = "CreatePABX";
            var parameters = new DynamicParameters();
            parameters.Add("Name", pabx.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("GroupID", pabx.GroupID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("IsDeactivated", pabx.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsForceOnNet", pabx.IsForceOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsCompanyCallsOnly", pabx.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("BarringList", pabx.BarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("AllowanceList", pabx.AllowanceList, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var createdPABX = new PABX
                {
                    ID = id,
                    Name = pabx.Name,
                    GroupID = pabx.GroupID,
                    IsDeactivated = pabx.IsDeactivated,
                    IsForceOnNet = pabx.IsForceOnNet,
                    IsCompanyCallsOnly = pabx.IsCompanyCallsOnly,
                    AllowanceList = pabx.AllowanceList,
                    BarringList = pabx.BarringList


                };
                return createdPABX;
            }
        }

        public async Task DeletePABX(int id)
        {
            var procedureName = "DeletePABX";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PABX>> GetPABXsByGroupID(int groupID)
        {
            var procedureName = "GetPABXsByGroupID";
            using (var connection = _context.CreateConnection())
            {
                var pabxs = await connection.QueryAsync<PABX>(procedureName, new { groupID }, commandType: CommandType.StoredProcedure);
                return pabxs.ToList();
            }
        }

        public async Task<PABX> GetPABX(int id)
        {
            var procedureName = "GetPABX";
            using (var connection = _context.CreateConnection())
            {
                var pabx = await connection.QuerySingleOrDefaultAsync<PABX>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return pabx;
            }
        }

        public async Task UpdatePABX(int id, PABXDTO pabx)
        {
            var procedureName = "UpdatePABX";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Int32);
            parameters.Add("Name", pabx.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("GroupID", pabx.GroupID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("IsDeactivated", pabx.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsForceOnNet", pabx.IsForceOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsCompanyCallsOnly", pabx.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("BarringList", pabx.BarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("AllowanceList", pabx.AllowanceList, DbType.String, ParameterDirection.Input); ;
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

