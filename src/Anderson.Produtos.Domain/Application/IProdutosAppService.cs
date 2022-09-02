using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.Application.ViewModels
{
    public interface IProductAppService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIdAsync(long id);
        Task CreateAsync(ProductViewModel model);
        Task UpdateAsync(ProductViewModel model);
        Task DeleteAsync(long id);
    }
}
