
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VPNService.Contracts;
using VPNService.Entities;
using VPNService.Entities.Models;

namespace VPNService.Repository
{
  
    public class ScreeningItemRepository : RepositoryBase<ScreeningItem>, IScreeningItemRepository
    {
        public ScreeningItemRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public void Create(Entities.Models.ScreeningItem entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entities.Models.ScreeningItem entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Entities.Models.ScreeningItem> FindByCondition(Expression<Func<Entities.Models.ScreeningItem, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Update(Entities.Models.ScreeningItem entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<Entities.Models.ScreeningItem> IRepositoryBase<Entities.Models.ScreeningItem>.FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
