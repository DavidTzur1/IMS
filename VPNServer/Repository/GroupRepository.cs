
using Dapper;
using System.Data;
using VPNServer.Context;
using VPNServer.Contracts;
using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DapperContext _context;
        public GroupRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Group> CreateGroup(GroupForCreationDto group)
        {
            var procedureName = "CreateGroup";
            var parameters = new DynamicParameters();
            parameters.Add("Name", group.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("CompanyId", group.CompanyId, DbType.Int32, ParameterDirection.Input);
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
                    Id = id,
                    Name = group.Name,
                    CompanyId = group.CompanyId,
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

        public async Task<IEnumerable<Group>> GetGroupsByCompanyId(int companyId)
        {
            var procedureName = "GetGroupsByCompanyId";
            using (var connection = _context.CreateConnection())
            {
                var groups = await connection.QueryAsync<Group>(procedureName, new { companyId }, commandType: CommandType.StoredProcedure);
                return groups.ToList();
            }
        }

        public async Task<Group> GetGroupById(int id)
        {
            var procedureName = "GetGroup";
            using (var connection = _context.CreateConnection())
            {
                var group = await connection.QuerySingleOrDefaultAsync<Group>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return group;
            }
        }

       

        public async Task UpdateGroup(int id, GroupForUpdateDto group)
        {
            var procedureName = "UpdateGroup";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", group.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("CompanyId", group.CompanyId, DbType.Int32, ParameterDirection.Input);
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

