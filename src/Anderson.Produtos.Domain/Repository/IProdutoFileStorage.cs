using System.IO;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.Repository
{
    public interface IProdutoFileStorage
    {
        Task<string> InsertAsync(Stream imagem);
    }
}
