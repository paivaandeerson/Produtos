using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.Application.ViewModels
{
    public interface IProdutosAppService
    {
        Task<IEnumerable<ProdutoViewModel>> GetAllAsync();
        Task<ProdutoViewModel> GetByIdAsync(long id);
        Task CreateAsync(ProdutoViewModel model);
        Task UpdateAsync(ProdutoViewModel model);
        Task DeleteAsync(long id);
    }
}
