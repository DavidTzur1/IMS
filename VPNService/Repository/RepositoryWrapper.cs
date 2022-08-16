using VPNService.Contracts;
using VPNService.Entities;

namespace VPNService.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IScreeningRepository _screening;
        private IScreeningItemRepository _screeningItem;
        public IScreeningRepository Screening
        {
            get
            {
                if (_screening == null)
                {
                    _screening = new ScreeningRepository(_repoContext);
                }
                return _screening;
            }
        }
        public IScreeningItemRepository ScreeningItem
        {
            get
            {
                if (_screeningItem == null)
                {
                    _screeningItem = new ScreeningItemRepository(_repoContext);
                }
                return _screeningItem;
            }
        }
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
