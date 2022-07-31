using VPN.DTO;
using VPN.Entities;

namespace VPN.Contracts
{
    public interface IScreeningRepository
    {
        public Task<IEnumerable<Screening>> GetScreeningsByCompanyID(int companyID);
        public Task<Screening> GetScreening(int id);
        public Task<Screening> CreateScreening(ScreeningForCreationDto screening);
        public Task UpdateScreening(int id, ScreeningForUpdateDto screening);
        public Task DeleteScreening(int id);
    }
}
