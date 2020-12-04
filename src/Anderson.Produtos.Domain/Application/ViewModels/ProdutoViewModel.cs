using Microsoft.AspNetCore.Http;
using System.IO;

namespace Anderson.Produtos.Domain.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string ImagemPath { get; set; }
        public IFormFile ImagemArquivo { get; set; }
    }
}
