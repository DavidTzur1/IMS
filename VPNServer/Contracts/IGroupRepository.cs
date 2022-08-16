using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Contracts
{
    public interface IGroupRepository
    {
        public Task<Group> GetGroupById(int id);
        public Task<IEnumerable<Group>> GetGroupsByCompanyId(int companyID);
        public Task<Group> CreateGroup(GroupForCreationDto group);
        public Task UpdateGroup(int id, GroupForUpdateDto group);
        public Task DeleteGroup(int id);
    }
}
