using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.Repository
{
    internal interface IProdutoRepository
    {
        ITransaction BeginTransacion();
        Task<IEnumerable<ProdutoDomainModel>> GetAllAsync();
        Task<ProdutoDomainModel> GetByIdAsync(long id);
        Task CreateAsync(ProdutoDomainModel model);
        Task UpdateAsync(ProdutoDomainModel model);
        Task DeleteAsync(long id);
    }
}
