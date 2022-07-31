using VPN.DTO;
using VPN.Entities;

namespace VPN.Contracts
{
    public interface IPABXRepository
    {
        public Task<IEnumerable<PABX>> GetPABXsByGroupID(int groupID);
        public Task<PABX> GetPABX(int id);
        public Task<PABX> CreatePABX(PABXDTO pabx);
        public Task UpdatePABX(int id, PABXDTO pabx);
        public Task DeletePABX(int id);
    }
}
