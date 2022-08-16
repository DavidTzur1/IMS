using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Contracts
{
    public interface IPABXRepository
    {
        public Task<PABX> GetPABX(int id);
        public Task<IEnumerable<PABX>> GetPABXsByGroupId(int groupId);
        public Task<PABX> CreatePABX(PABXForCreationDto group);
        public Task UpdatePABX(int id, PABXForUpdateDto group);
        public Task DeletePABX(int id);
    }
}
