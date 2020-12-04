using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.Model;
using Anderson.Produtos.Domain.Util;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Anderson.Produtos.UnitTest")]
namespace Anderson.Produtos.Domain
{
    internal record ProdutoDomainModel : IBaseModel
    {
        private const string NomeEObrigatorio = "Nome é obrigatório"; //const p não recriar strings a cada vez que o método rodar
        private const string ValorEObrigatorio = "Valor é obrigatório";

        public long Id { get; init; }
        public string Nome { get; protected set; }
        public decimal Valor { get; protected set; }
        public string ImagemPath { get; set; } //não é boa prática guardar imagem em DB

        public ProdutoDomainModel(string nome,
            string valor, 
            string imagemPath)
        {
            Nome = nome;
            ImagemPath = imagemPath;
            if (decimal.TryParse(valor, out var convertedValor))
            {
                Valor = convertedValor;
            };
        }

        //to EF
        protected ProdutoDomainModel()
        {
        }
        
        //DDD: Toda classe deve ter um motivo para ser alterada
        public ProdutoDomainModel RealizarManutencao(ProdutoViewModel viewModel)
        {
            return this with 
            { 
                Nome = viewModel.Nome, 
                ImagemPath = viewModel.ImagemPath ?? ImagemPath, 
                Valor = viewModel.Valor 
            };
        }

        public bool IsValid(IValidationResult validationResult)
        {
            if (string.IsNullOrEmpty(Nome))
                validationResult.AddError(NomeEObrigatorio);

            if (Valor is default(decimal))
                validationResult.AddError(ValorEObrigatorio);


            return !validationResult.Erros?.Any() ?? true;
        }
    }
}
