using Anderson.Produtos.Domain.Repository;
using Anderson.Produtos.Domain.Repository.EFImplementations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.EFImplementations.Repository
{
    /// <summary>
    /// Async in all IO operations to avoid IO Blocking
    /// </summary>
    internal class ProductRepository : IProductRepository
    {
        private readonly ProductContext _produtoContext;

        public ProductRepository(ProductContext produtoContext)
        {
            _produtoContext = produtoContext;
        }

        public ITransaction BeginTransacion() => new ProductTransaction(_produtoContext.Database.BeginTransaction());

        //this result should be paged and cached
        public Task<ProductDomainModel[]> GetAllAsync() => _produtoContext
                .Products
                .AsNoTrackingWithIdentityResolution()
                .ToArrayAsync();

        public async Task<ProductDomainModel> GetByIdAsync(long id) => await _produtoContext
            .Products
            .AsNoTrackingWithIdentityResolution()
            .SingleOrDefaultAsync(x => x.Id == id)//No duplication, this linq extension guarantees
            .ConfigureAwait(false);

        public async Task CreateAsync(ProductDomainModel model)
        {
            await _produtoContext.Products.AddAsync(model);
            await _produtoContext.SaveChangesAsync();
            //_produtoContext.Entry(model).State = EntityState.Detached; //Due automated tests
        }

        public Task UpdateAsync(ProductDomainModel model)
        {
            _produtoContext.Entry(model).State = EntityState.Modified;
            return _produtoContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            _produtoContext.Remove(await GetByIdAsync(id));
            await _produtoContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
