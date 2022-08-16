
using Dapper;
using Microsoft.EntityFrameworkCore;
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
    public  class ScreeningRepository : RepositoryBase<Screening>, IScreeningRepository
    {
        public ScreeningRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public void Create(Entities.Models.Screening entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entities.Models.Screening entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Entities.Models.Screening> FindByCondition(Expression<Func<Entities.Models.Screening, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Screening>> GetAllScreeningsAsync()
        {
            return await FindAll()
                .OrderBy(ow => ow.Name)
                .ToListAsync();

        }

        public async Task<Screening> GetScreeningByIdAsync(int id)
        {
            return await FindByCondition(item => item.ID.Equals(id)).FirstOrDefaultAsync();
            //throw new NotImplementedException();
        }

        public void Update(Entities.Models.Screening entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<Entities.Models.Screening> IRepositoryBase<Entities.Models.Screening>.FindAll()
        {
            throw new NotImplementedException();
        }

       

        Task<Entities.Models.Screening> IScreeningRepository.GetScreeningByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
