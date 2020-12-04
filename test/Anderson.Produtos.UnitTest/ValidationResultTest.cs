using Anderson.Produtos.Domain;
using System.Linq;
using Xunit;

namespace Anderson.Produtos.UnitTest
{
    public class ValidationResultTest
    {
        [Fact]
        public void AddMensagemErro_DeveConterErroNaLista()
        {
            // Arrange
            var target = new ValidationResult();
            var expectedCount = 1;
            var errorMessage = "teste";

            // Act
            target.AddError(errorMessage);

            // Assert
            Assert.Equal(expectedCount, target.Erros.Count);            
            Assert.Equal(errorMessage, target.Erros.First());            
        }
    }
}
