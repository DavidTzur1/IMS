

using VPNServer.Dto;
using VPNServer.Entities;
using Range = VPNServer.Entities.Range;

namespace VPNServer.Contracts
{
    public interface IRangeRepository
    {
        public Task<IEnumerable<Range>> GetRangesByPABXId(int pabxId);
        public Task<Range> GetRange(int id);
        public Task<Range> CreateRange(RangeForCreationDto range);
        public Task UpdateRange(int id, RangeForUpdateDto range);
        public Task DeleteRange(int id);
    }
}
