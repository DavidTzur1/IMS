using VPN.DTO;
using VPN.Entities;

namespace VPN.Contracts
{
    public interface IRangeRepository
    {
        public Task<IEnumerable<Entities.Range>> GetRangesByPABXID(int pabxID);
        public Task<Entities.Range> GetRange(int id);
        public Task<Entities.Range> CreateRange(RangeDTO range);
        public Task UpdateRange(int id, RangeDTO range);
        public Task DeleteRange(int id);
    }
}
