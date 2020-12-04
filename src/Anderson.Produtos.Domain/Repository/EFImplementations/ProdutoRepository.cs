using Anderson.Produtos.Domain.Repository;
using Anderson.Produtos.Domain.Repository.EFImplementations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.EFImplementations.Repository
{
    //Assincronismo pq não queremos IO Blocking no server
    internal class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoContext _produtoContext;

        public ProdutoRepository(ProdutoContext produtoContext)
        {
            _produtoContext = produtoContext;
        }

        public ITransaction BeginTransacion() => new ProdutoTransaction(_produtoContext.Database.BeginTransaction());

        //deveria ser paginado e cacheado
        public async Task<IEnumerable<ProdutoDomainModel>> GetAllAsync() => await _produtoContext
                .ProdutoModels
                .AsNoTrackingWithIdentityResolution()
                .ToArrayAsync()
                .ConfigureAwait(false);

        public async Task<ProdutoDomainModel> GetByIdAsync(long id) => await _produtoContext
            .ProdutoModels
            .AsNoTracking()
            .SingleOrDefaultAsync(x=>x.Id == id)//Garante que não há id duplicado!
            .ConfigureAwait(false);

        public async Task CreateAsync(ProdutoDomainModel model)
        {
            await _produtoContext.ProdutoModels.AddAsync(model);
            await _produtoContext.SaveChangesAsync();
            _produtoContext.Entry(model).State = EntityState.Detached; //por conta dos testes de criação e alteração
        }

        public async Task UpdateAsync(ProdutoDomainModel model)
        {
            _produtoContext.Entry(model).State = EntityState.Modified;

            await _produtoContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
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
