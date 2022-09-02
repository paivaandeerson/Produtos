using Anderson.Produtos.Domain;
using System.Linq;
using Xunit;

namespace Anderson.Produtos.UnitTest
{
    public class ValidationResultTest
    {
        [Fact]
        public void AddErrorMessage_MustContainErrorInList()
        {
            // Arrange
            var target = new ValidationResult();
            var expectedCount = 1;
            var errorMessage = "teste";

            // Act
            target.AddError(errorMessage);

            // Assert
            Assert.Equal(expectedCount, target.Errors.Count);            
            Assert.Equal(errorMessage, target.Errors.First());            
        }
    }
}
