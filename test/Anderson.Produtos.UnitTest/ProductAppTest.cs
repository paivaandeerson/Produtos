using Anderson.Produtos.Domain;
using Anderson.Produtos.Domain.Application.Implementations;
using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.Repository;
using Anderson.Produtos.Domain.Repository.EFImplementations;
using Anderson.Produtos.Domain.Util;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Anderson.Produtos.UnitTest
{
    public class ProductAppTest
    {
        private readonly IProductFileStorage _produtoFileSystem = Substitute.For<IProductFileStorage>();
        private readonly IProductRepository _produtoRepository = Substitute.For<IProductRepository>();
        private readonly IValidationResult _validationResult = Substitute.For<IValidationResult>();
        private readonly ProductContext _context = Substitute.For<ProductContext>();

        [Fact(Skip = "Temp")]
        public async Task ValoresValidos_DeveCriarComSucesso()
        {
            // Arrange
            var target = InstanciarIntegracao();
            var vm = new ProductViewModel
            {
                Name = "Produto Aleatório",
                ImagePath = "~/fakepath/produto.jpg",
                ImageStream = Substitute.For<IFormFile>(),
                Value = 1234.50M
            };
            _produtoFileSystem.InsertAsync(Arg.Any<MemoryStream>()).Returns(vm.ImagePath);

            // Act
            await target.CreateAsync(vm);

            // Assert
            var actual = (await _produtoRepository.GetAllAsync()).FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(actual.Name, vm.Name);
            Assert.Equal(actual.Value, vm.Value);
            Assert.Equal(actual.ImagePath, vm.ImagePath);
        }

        [Fact(Skip = "Temp")]
        public async Task ValoresValidos_DeveAtualizarComSucesso()
        {
            // Arrange
            var target = InstanciarIntegracao();
            var vmInsercao = new ProductViewModel
            {
                Name = "Produto Aleatório",
                ImageStream = Substitute.For<IFormFile>(),
                Value = 1234.50M
            };

            await target.CreateAsync(vmInsercao);

            var vmAlteracao = new ProductViewModel
            {
                Id = 1,
                Name = "Produto Com Nome Alterado",
                ImagePath = "~/fakepath/fotoAlterada.jpg",
                Value = 54321.00M
            };
            _produtoFileSystem.InsertAsync(Arg.Any<MemoryStream>()).Returns(vmAlteracao.ImagePath);

            // Act
            await target.UpdateAsync(vmAlteracao);

            // Assert
            var actual = await _produtoRepository.GetByIdAsync(vmAlteracao.Id);

            Assert.False(_validationResult.Errors.Any());
            Assert.NotNull(actual);
            Assert.Equal(actual.Name, vmAlteracao.Name);
            Assert.Equal(actual.Value, vmAlteracao.Value);
            Assert.Equal(actual.ImagePath, vmAlteracao.ImagePath);
        }

        [Fact(Skip = "Temp")]
        public async Task IdExistente_DeveRemoverComSucesso()
        {
            // Arrange
            var target = InstanciarIntegracao();
            var vmInsercao = new ProductViewModel
            {
                Name = "Produto Aleatório",
                ImagePath = "~/fakepath/produto.jpg",
                Value = 1234.50M
            };

            await target.CreateAsync(vmInsercao);

            // Act
            await target.DeleteAsync(1);
            var actual = await target.GetByIdAsync(1);

            // Assert
            Assert.True(_validationResult.Errors.Any(x=>x == "Produto não encontrado"));
            Assert.Null(actual);
        }

        [Fact(Skip = "Temp")]
        public async Task ImagemPathVazio_DeveAddErro()
        {
            // Arrange
            var expectedMessage = "Imagem é obrigatório";

            // Arrange
            var target = InstanciarIntegracao();
            var vm = new ProductViewModel
            {
                Name = "Produto Aleatório",
                Value = 1234.50M
            };
            _produtoFileSystem.InsertAsync(Arg.Any<MemoryStream>()).Returns(vm.ImagePath);

            // Act
            await target.CreateAsync(vm);

            // Assert
            Assert.Equal(_validationResult.Errors.First(), expectedMessage);
        }

        private ProductAppService InstanciarIntegracao()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductViewModel, ProductDomainModel>();
                cfg.CreateMap<ProductDomainModel, ProductViewModel>();
            });

            return new ProductAppService(_produtoRepository,
                _produtoFileSystem,
                _validationResult,
                new Mapper(configuration));
        }
    }
}
