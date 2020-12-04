using System;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.Repository
{
    public interface ITransaction : IDisposable
    {
        Task CommitAsync();

        Task RollbackAsync();
    }
}
