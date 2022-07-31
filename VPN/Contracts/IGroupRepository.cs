using VPN.DTO;
using VPN.Entities;

namespace VPN.Contracts
{
    public interface IGroupRepository
    {
        public Task<IEnumerable<Group>> GetGroups();
        public Task<IEnumerable<Group>> GetGroupsByCompanyID(int companyID);
        public Task<Group> GetGroup(int id);
        public Task<Group> CreateGroup(GroupDTO group);
        public Task UpdateGroup(int id, GroupDTO group);
        public Task DeleteGroup(int id);
    }
}
