using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNService.Contracts
{
    

    public interface IRepositoryWrapper
    {
        IScreeningRepository Screening { get; }
        IScreeningItemRepository ScreeningItem { get; }
        Task SaveAsync();
    }

}
