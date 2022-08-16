using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Contracts
{
    public interface IUserRepository
    {
        public Task<User> GetUser(int id);
        public Task<User> GetUserByCLI(string cli);
        public Task<IEnumerable<User>> GetUsersByPABXId(int pabxId);
        public Task<User> CreateUser(UserForCreationDto user);
        public Task UpdateUser(int id, UserForUpdateDto user);
        public Task DeleteUser(int id);
    }
}
