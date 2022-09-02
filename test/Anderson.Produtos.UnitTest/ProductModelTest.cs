using Anderson.Produtos.Domain;
using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.Util;
using NSubstitute;
using Xunit;

namespace Anderson.Produtos.UnitTest
{
    public class ProductDomainModelTest
    {
        private readonly IValidationResult _validationResult = Substitute.For<IValidationResult>();

        [Fact]
        public void ValidValues_Maintain()
        {
            // Arrange
            var target = new ProductDomainModel("Teste", "1234.12", "~/test.img");

            // Act
            var result = target.Maintain(new ProductViewModel
            {
                Name = "abcd",
                Value = 1234.56M,
                ImagePath= "~/abcd",
            });

            // Assert
            Assert.Equal("abcd", result.Name);
            Assert.Equal(1234.56M, result.Value);
            Assert.Equal("~/abcd", result.ImagePath);
            _validationResult.DidNotReceiveWithAnyArgs().AddError(string.Empty);
        }

        [Fact]
        public void ValoresValues_NoErrors()
        {
            // Arrange
            var target = new ProductDomainModel("Teste", "1234.12", "~/test.img");

            // Act
            var result = target.IsValid(_validationResult);

            // Assert
            Assert.True(result);
            _validationResult.DidNotReceiveWithAnyArgs().AddError(string.Empty);
        }

        [Fact]
        public void EmptyName_MustAddError()
        {
            // Arrange
            var target = new ProductDomainModel("", "1234.12", "~/test.img");
            var expectedMessage = "Nome é obrigatório";
            _validationResult.Errors.Returns(new[] { expectedMessage });

            // Act
            var result = target.IsValid(_validationResult);

            // Assert
            Assert.False(result);
            _validationResult.Received(1).AddError(expectedMessage);
        }

        [Fact]
        public void EmptyValue_MustAddError()
        {
            // Arrange
            var target = new ProductDomainModel("Teste", "", "~/test.img");
            var expectedMessage = "Valor é obrigatório";
            _validationResult.Errors.Returns(new[] { expectedMessage });

            // Act
            var result = target.IsValid(_validationResult);

            // Assert
            Assert.False(result);
            _validationResult.Received(1).AddError(expectedMessage);
        }

        [Fact]
        public void InvalidValue_MustAddError()
        {
            // Arrange
            var target = new ProductDomainModel("Teste", "abcde", "~/test.img");
            var expectedMessage = "Valor é obrigatório";
            _validationResult.Errors.Returns(new[] { expectedMessage });

            // Act
            var result = target.IsValid(_validationResult);

            // Assert
            Assert.False(result);
            _validationResult.Received(1).AddError(expectedMessage);
        }
    }
}
