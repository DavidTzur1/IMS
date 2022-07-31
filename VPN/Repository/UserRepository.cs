using Dapper;
using System.Data;
using VPN.Context;
using VPN.Contracts;
using VPN.DTO;
using VPN.Entities;

namespace VPN.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(UserDTO user)
        {
            var procedureName = "CreateUser";
            var parameters = new DynamicParameters();
            parameters.Add("CLI", user.CLI, DbType.String, ParameterDirection.Input);
            parameters.Add("Name", user.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("PABXID", user.PABXID, DbType.Int32, ParameterDirection.Input);
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
                    ID = id,
                    Name = user.Name,
                    PABXID = user.PABXID,
                    IsDeactivated = user.IsDeactivated,                   
                    IsCompanyCallsOnly = user.IsCompanyCallsOnly,
                    IsVirtualOnNet = user.IsVirtualOnNet,
                    PrivateNumber= user.PrivateNumber,
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

        public async Task<IEnumerable<User>> GetUsersByPABXID(int pabxID)
        {
            var procedureName = "GetUsersByPABXID";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(procedureName, new { pabxID }, commandType: CommandType.StoredProcedure);
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

        public async Task<User> GetUserByCLI(int cli)
        {
            var procedureName = "GetUser";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(procedureName, new { cli }, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task UpdateUser(int id, UserDTO user)
        {
            var procedureName = "UpdateUser";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Int32);
            parameters.Add("CLI", user.CLI, DbType.String, ParameterDirection.Input);
            parameters.Add("Name", user.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("PABXID", user.PABXID, DbType.Int32, ParameterDirection.Input);
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


