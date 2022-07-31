using Dapper;
using System.Data;
using VPN.Context;
using VPN.Contracts;
using VPN.DTO;
using VPN.Entities;

namespace VPN.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DapperContext _context;
        public GroupRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Group> CreateGroup(GroupDTO group)
        {
            var procedureName = "CreateGroup";
            var parameters = new DynamicParameters();
            parameters.Add("Name", group.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("CompanyID", group.CompanyID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("IsDeactivated", group.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsForceOnNet", group.IsForceOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsCompanyCallsOnly", group.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("BarringList", group.BarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("AllowanceList", group.AllowanceList, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var createdGroup = new Group
                {
                    ID = id,
                    Name = group.Name,
                    CompanyID = group.CompanyID,
                    IsDeactivated = group.IsDeactivated,
                    IsForceOnNet = group.IsForceOnNet,
                    IsCompanyCallsOnly = group.IsCompanyCallsOnly,
                    AllowanceList = group.AllowanceList,
                    BarringList = group.BarringList


                };
                return createdGroup;
            }
        }

        public async Task DeleteGroup(int id)
        {
            var procedureName = "DeleteGroup";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Group>> GetGroupsByCompanyID(int companyID)
        {
            var procedureName = "GetCompanyGroups";
            using (var connection = _context.CreateConnection())
            {
                var groups = await connection.QueryAsync<Group>(procedureName, new { companyID }, commandType: CommandType.StoredProcedure);
                return groups.ToList();
            }
        }

        public async Task<Group> GetGroup(int id)
        {
            var procedureName = "GetGroup";
            using (var connection = _context.CreateConnection())
            {
                var group = await connection.QuerySingleOrDefaultAsync<Group>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return group;
            }
        }

        public Task<IEnumerable<Group>> GetGroups()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateGroup(int id, GroupDTO group)
        {
            var procedureName = "UpdateGroup";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Int32);
            parameters.Add("Name", group.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("CompanyID", group.CompanyID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("IsDeactivated", group.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsForceOnNet", group.IsForceOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsCompanyCallsOnly", group.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("BarringList", group.BarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("AllowanceList", group.AllowanceList, DbType.String, ParameterDirection.Input); ;
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
