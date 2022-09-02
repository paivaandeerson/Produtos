using System.IO;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.Repository
{
    public interface IProductFileStorage
    {
        Task<string> InsertAsync(Stream imagem);
    }
}
