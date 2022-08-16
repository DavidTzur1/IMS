using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPNService.Entities.Models;

namespace VPNService.Contracts
{
    public interface IScreeningRepository : IRepositoryBase<Screening>
    {
        Task<IEnumerable<Screening>> GetAllScreeningsAsync();
        Task<Screening> GetScreeningByIdAsync(int id);
    }
}
