using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Contracts
{
    public interface IScreeningItemRepository
    {
        public Task<IEnumerable<ScreeningItem>> GetScreeningItemByScreeningId(int screeningId);
        public Task<ScreeningItem> GetScreeningItem(int id);
        public Task<ScreeningItem> CreateScreeningItem(ScreeningItemForCreationDto screeningItem);
        public Task UpdateScreeningItem(int id, ScreeningItemForUpdateDto screeningItem);
        public Task DeleteScreeningItem(int id);
    }
}
