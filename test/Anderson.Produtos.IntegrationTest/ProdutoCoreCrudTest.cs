using Anderson.Produtos.Domain;
using Anderson.Produtos.Domain.Application.Implementations;
using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.EFImplementations.Repository;
using Anderson.Produtos.Domain.Repository;
using Anderson.Produtos.Domain.Repository.EFImplementations;
using Anderson.Produtos.Domain.Util;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Anderson.Produtos.IntegrationTest
{
    public class ProdutoCoreCrudTest
    {
        private readonly IProdutoFileStorage _produtoFileSystem = Substitute.For<IProdutoFileStorage>();
        private readonly IProdutoRepository _produtoRepository;
        private readonly IValidationResult _validationResult;
        private readonly ProdutoContext _context;

        public ProdutoCoreCrudTest()
        {
            var sqliteConnection = new SqliteConnection("DataSource=Test;Mode=Memory");
            sqliteConnection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<ProdutoContext>()
                .UseSqlite(sqliteConnection)
                .Options;

            _context = new ProdutoContext(optionsBuilder);
            _produtoRepository = new ProdutoRepository(_context);
            _validationResult = new ValidationResult();
        }

        [Fact]
        public async Task ValoresValidos_DeveCriarComSucesso()
        {
            // Arrange
            var target = InstanciarIntegracao();
            var vm = new ProdutoViewModel
            {
                Nome = "Produto Aleatório",
                ImagemPath = "~/fakepath/produto.jpg",
                ImagemArquivo = Substitute.For<IFormFile>(),
                Valor = 1234.50M
            };
            _produtoFileSystem.InsertAsync(Arg.Any<MemoryStream>()).Returns(vm.ImagemPath);

            // Act
            await target.CreateAsync(vm);

            // Assert
            var actual = (await _produtoRepository.GetAllAsync()).FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(actual.Nome, vm.Nome);
            Assert.Equal(actual.Valor, vm.Valor);
            Assert.Equal(actual.ImagemPath, vm.ImagemPath);
        }

        [Fact]
        public async Task ValoresValidos_DeveAtualizarComSucesso()
        {
            // Arrange
            var target = InstanciarIntegracao();
            var vmInsercao = new ProdutoViewModel
            {
                Nome = "Produto Aleatório",
                ImagemArquivo = Substitute.For<IFormFile>(),
                Valor = 1234.50M
            };

            await target.CreateAsync(vmInsercao);

            var vmAlteracao = new ProdutoViewModel
            {
                Id = 1,
                Nome = "Produto Com Nome Alterado",
                ImagemPath = "~/fakepath/fotoAlterada.jpg",
                Valor = 54321.00M
            };
            _produtoFileSystem.InsertAsync(Arg.Any<MemoryStream>()).Returns(vmAlteracao.ImagemPath);

            // Act
            await target.UpdateAsync(vmAlteracao);

            // Assert
            var actual = await _produtoRepository.GetByIdAsync(vmAlteracao.Id);

            Assert.False(_validationResult.Erros.Any());
            Assert.NotNull(actual);
            Assert.Equal(actual.Nome, vmAlteracao.Nome);
            Assert.Equal(actual.Valor, vmAlteracao.Valor);
            Assert.Equal(actual.ImagemPath, vmAlteracao.ImagemPath);
        }

        [Fact]
        public async Task IdExistente_DeveRemoverComSucesso()
        {
            // Arrange
            var target = InstanciarIntegracao();
            var vmInsercao = new ProdutoViewModel
            {
                Nome = "Produto Aleatório",
                ImagemPath = "~/fakepath/produto.jpg",
                Valor = 1234.50M
            };

            await target.CreateAsync(vmInsercao);

            // Act
            await target.DeleteAsync(1);
            var actual = await target.GetByIdAsync(1);

            // Assert
            Assert.True(_validationResult.Erros.Any(x=>x == "Produto não encontrado"));
            Assert.Null(actual);
        }


        [Fact]
        public async Task ImagemPathVazio_DeveAddErro()
        {
            // Arrange
            var expectedMessage = "Imagem é obrigatório";

            // Arrange
            var target = InstanciarIntegracao();
            var vm = new ProdutoViewModel
            {
                Nome = "Produto Aleatório",
                Valor = 1234.50M
            };
            _produtoFileSystem.InsertAsync(Arg.Any<MemoryStream>()).Returns(vm.ImagemPath);

            // Act
            await target.CreateAsync(vm);

            // Assert
            Assert.Equal(_validationResult.Erros.First(), expectedMessage);
        }

        private ProdutosAppService InstanciarIntegracao()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdutoViewModel, ProdutoDomainModel>();
                cfg.CreateMap<ProdutoDomainModel, ProdutoViewModel>();
            });

            return new ProdutosAppService(_produtoRepository,
                _produtoFileSystem,
                _validationResult,
                new Mapper(configuration));
        }
    }
}
