
using Dapper;
using System.Data;
using VPNServer.Context;
using VPNServer.Contracts;
using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(UserForCreationDto user)
        {
            var procedureName = "CreateUser";
            var parameters = new DynamicParameters();
            parameters.Add("CLI", user.CLI, DbType.String, ParameterDirection.Input);
            parameters.Add("Name", user.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("PABXId", user.PABXId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("IsDeactivated", user.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsCompanyCallsOnly", user.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsVirtualOnNet", user.IsVirtualOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("PrivateNumber", user.PrivateNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("CalledNumber", user.CalledNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("ChargingNumber", user.ChargingNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("BarringList", user.BarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("AllowanceList", user.AllowanceList, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var createdUser = new User
                {
                    Id = id,
                    Name = user.Name,
                    PABXId = user.PABXId,
                    IsDeactivated = user.IsDeactivated,
                    IsCompanyCallsOnly = user.IsCompanyCallsOnly,
                    IsVirtualOnNet = user.IsVirtualOnNet,
                    PrivateNumber = user.PrivateNumber,
                    CalledNumber = user.CalledNumber,
                    ChargingNumber = user.ChargingNumber,
                    AllowanceList = user.AllowanceList,
                    BarringList = user.BarringList


                };
                return createdUser;
            }
        }

        public async Task DeleteUser(int id)
        {
            var procedureName = "DeleteUser";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<User>> GetUsersByPABXId(int pabxId)
        {
            var procedureName = "GetUsersByPABXId";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(procedureName, new { pabxId }, commandType: CommandType.StoredProcedure);
                return users.ToList();
            }
        }

        public async Task<User> GetUser(int id)
        {
            var procedureName = "GetUser";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(procedureName, new { id }, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<User> GetUserByCLI(string cli)
        {
            var procedureName = "GetUserByCLI";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(procedureName, new { cli }, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task UpdateUser(int id, UserForUpdateDto user)
        {
            var procedureName = "UpdateUser";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("CLI", user.CLI, DbType.String, ParameterDirection.Input);
            parameters.Add("Name", user.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("PABXId", user.PABXId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("IsDeactivated", user.IsDeactivated, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsCompanyCallsOnly", user.IsCompanyCallsOnly, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("IsVirtualOnNet", user.IsVirtualOnNet, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("PrivateNumber", user.PrivateNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("CalledNumber", user.CalledNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("ChargingNumber", user.ChargingNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("BarringList", user.BarringList, DbType.String, ParameterDirection.Input);
            parameters.Add("AllowanceList", user.AllowanceList, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
