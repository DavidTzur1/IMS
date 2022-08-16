using VPNServer.Dto;
using VPNServer.Entities;

namespace VPNServer.Contracts
{
    public interface IScreeningRepository
    {
        public Task<IEnumerable<Screening>> GetScreeningsByCompanyId(int companyId);
        public Task<Screening> GetScreening(int id);
        public Task<Screening> CreateScreening(ScreeningForCreationDto screening);
        public Task UpdateScreening(int id, ScreeningForUpdateDto screening);
        public Task DeleteScreening(int id);
    }
}
