using Microsoft.AspNetCore.Http;

namespace Anderson.Produtos.Domain.Application.ViewModels
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string ImagePath { get; set; }
        public IFormFile? ImageStream { get; set; }
    }
}
