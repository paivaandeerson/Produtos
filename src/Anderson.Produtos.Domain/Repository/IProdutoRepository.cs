using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.Repository
{
    internal interface IProductRepository
    {
        ITransaction BeginTransacion();
        Task<ProductDomainModel[]> GetAllAsync();
        Task<ProductDomainModel> GetByIdAsync(long id);
        Task CreateAsync(ProductDomainModel model);
        Task UpdateAsync(ProductDomainModel model);
        Task DeleteAsync(long id);
    }
}
