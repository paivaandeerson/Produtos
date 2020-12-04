using Anderson.Produtos.Domain;
using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.Util;
using NSubstitute;
using Xunit;

namespace Anderson.Produtos.UnitTest
{
    public class ProdutoDomainModelTest
    {
        private readonly IValidationResult _validationResult = Substitute.For<IValidationResult>();

        [Fact]
        public void ValoresValidos_RealizarManutencao()
        {
            // Arrange
            var target = new ProdutoDomainModel("Teste", "1234.12", "~/test.img");

            // Act
            var result = target.RealizarManutencao(new ProdutoViewModel
            {
                Nome = "abcd",
                Valor = 1234.56M,
                ImagemPath= "~/abcd",
            });

            // Assert
            Assert.Equal("abcd", result.Nome);
            Assert.Equal(1234.56M, result.Valor);
            Assert.Equal("~/abcd", result.ImagemPath);
            _validationResult.DidNotReceiveWithAnyArgs().AddError(string.Empty);
        }

        [Fact]
        public void ValoresValidos_NaoDeveAddErro()
        {
            // Arrange
            var target = new ProdutoDomainModel("Teste", "1234.12", "~/test.img");

            // Act
            var result = target.IsValid(_validationResult);

            // Assert
            Assert.True(result);
            _validationResult.DidNotReceiveWithAnyArgs().AddError(string.Empty);
        }

        [Fact]
        public void NomeVazio_DeveAddErro()
        {
            // Arrange
            var target = new ProdutoDomainModel("", "1234.12", "~/test.img");
            var expectedMessage = "Nome é obrigatório";
            _validationResult.Erros.Returns(new[] { expectedMessage });

            // Act
            var result = target.IsValid(_validationResult);

            // Assert
            Assert.False(result);
            _validationResult.Received(1).AddError(expectedMessage);
        }

        [Fact]
        public void ValorVazio_DeveAddErro()
        {
            // Arrange
            var target = new ProdutoDomainModel("Teste", "", "~/test.img");
            var expectedMessage = "Valor é obrigatório";
            _validationResult.Erros.Returns(new[] { expectedMessage });

            // Act
            var result = target.IsValid(_validationResult);

            // Assert
            Assert.False(result);
            _validationResult.Received(1).AddError(expectedMessage);
        }

        [Fact]
        public void ValorInvalido_DeveAddErro()
        {
            // Arrange
            var target = new ProdutoDomainModel("Teste", "abcde", "~/test.img");
            var expectedMessage = "Valor é obrigatório";
            _validationResult.Erros.Returns(new[] { expectedMessage });

            // Act
            var result = target.IsValid(_validationResult);

            // Assert
            Assert.False(result);
            _validationResult.Received(1).AddError(expectedMessage);
        }
    }
}
