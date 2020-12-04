using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.Repository.EFImplementations
{
    public class ProdutoTransaction : ITransaction
    {
        private readonly IDbContextTransaction _transaction;

        public ProdutoTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task CommitAsync() => await _transaction.CommitAsync();

        public async Task RollbackAsync() => await _transaction.RollbackAsync();

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // called via myClass.Dispose(). 
                    // OK to use any private object references           
                    _transaction.Dispose();
                }
                // Release unmanaged resources.
                // Set large fields to null.     
                _disposed = true;
            }
        }

        public void Dispose() // Implement IDisposable
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}