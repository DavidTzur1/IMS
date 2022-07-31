using VPN.DTO;
using VPN.Entities;

namespace VPN.Contracts
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsersByPABXID(int pabxID);
        public Task<User> GetUser(int id);
        public Task<User> GetUserByCLI(int cli);
        public Task<User> CreateUser(UserDTO user);
        public Task UpdateUser(int id, UserDTO user);
        public Task DeleteUser(int id);
    }
}
