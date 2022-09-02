using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.Model;
using Anderson.Produtos.Domain.Util;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Anderson.Produtos.UnitTest")]
namespace Anderson.Produtos.Domain
{
    internal record ProductDomainModel : IBaseModel
    {
        public long Id { get; init; }
        public string Name { get; protected set; }
        public decimal Value { get; protected set; }
        public string ImagePath { get; set; } //Don't persist images

        public ProductDomainModel(string nome,
            string valor, 
            string imagemPath)
        {
            Name = nome;
            ImagePath = imagemPath;
            if (decimal.TryParse(valor, out var convertedValor))
            {
                Value = convertedValor;
            };
        }

        //to EF
        protected ProductDomainModel()
        {
        }
        
        //DDD: Every class should have a reason why it's being changed
        public ProductDomainModel Maintain(ProductViewModel viewModel)
        {
            return this with 
            { 
                Name = viewModel.Name, 
                ImagePath = viewModel.ImagePath ?? ImagePath, 
                Value = viewModel.Value 
            };
        }

        public bool IsValid(IValidationResult validationResult)
        {
            const string NameIsRequired = "Nome é obrigatório"; 
            const string ValueIsRequired = "Valor é obrigatório";
            const string ImageProductIsRequired = "Imagem é obrigatório";

            if (string.IsNullOrEmpty(Name))
                validationResult.AddError(NameIsRequired);

            if (string.IsNullOrEmpty(ImagePath))
                validationResult.AddError(ImageProductIsRequired);

            if (Value is default(decimal))
                validationResult.AddError(ValueIsRequired);


            return !validationResult.Errors?.Any() ?? true;
        }
    }
}
