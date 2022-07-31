using VPN.DTO;
using VPN.Entities;

namespace VPN.Contracts
{
    public interface IScreeningDataRepository
    {
        public Task<IEnumerable<ScreeningData>> GetScreeningDataByScreeningID(int screeningID);
        public Task<ScreeningData> GetScreeningData(int id);
        public Task<ScreeningData> CreateScreeningData(ScreeningDataForCreationDto screeningData);
        public Task UpdateScreeningData(int id, ScreeningDataForUpdateDto screeningData);
        public Task DeleteScreeningData(int id);
    }
}
