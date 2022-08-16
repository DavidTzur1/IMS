using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IScreeningRepository : IRepositoryBase<Screening>
    {
        Task<IEnumerable<Screening>> GetAllScreeningsAsync();
        Task<Screening> GetScreeningByIdAsync(int id);
    }
}
