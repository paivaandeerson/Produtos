using Anderson.Produtos.Domain.Application.ViewModels;
using System.Net.Http.Formatting;
using System.Text.Json;
using TechTalk.SpecFlow.Assist;

namespace Anderson.Produtos.SpecFlow.StepDefinitions
{
    /// <summary>
    /// Data dump could be useful
    /// </summary>
    [Binding]
    public class ProductHandleStepDefinition : BaseEndToEndTest
    {
        private HttpResponseMessage _apiResponse;
        public ProductHandleStepDefinition()
        {
            BaseURL = "/api/Produtos/";
        }

        #region Add

        [Given(@"The database is empty")]
        public async Task GivenTheDatabaseIsEmpty()
        {
            //Arrange
            await StartApiAsync();
        }

        [When(@"I add product")]
        public async Task WhenIAddProduct(Table table)
        {
            //Arrange
            var parameter = table.CreateSet<ProductViewModel>().First();

            // Act            
            _apiResponse = await ClientApi.PostAsync(BaseURL, parameter, new JsonMediaTypeFormatter());
        }

        [Then(@"The result for add should be (.*) and the return should be (.*)")]
        public async Task ThenTheResultForAddShouldBe(string Result, string ReturnContent)
        {
            // Arrange
            var possibleStatusCodes = ResultApi[Result];

            // Assert            
            var responseString = await _apiResponse.Content.ReadAsStringAsync();
            Assert.Contains(ReturnContent, responseString);
            Assert.Contains(_apiResponse.StatusCode, possibleStatusCodes);
        }

        #endregion

        #region Change

        [Given(@"These products exists on the system for change")]
        public async Task GivenThereAreProductOnTheSystemForChange(Table table)
        {
            //Arrange            
            await StartApiAsync();

            var parameters = table.CreateSet<ProductViewModel>();
            var possibleStatusCodes = ResultApi["Success"];

            // Act            
            foreach (var parameter in parameters)
            {
                ApiResponse = await ClientApi.PostAsync(BaseURL, parameter, new JsonMediaTypeFormatter());

                Assert.Contains(ApiResponse.StatusCode, possibleStatusCodes);
            }
        }

        [When(@"I change the product")]
        public async Task WhenIChangeProduct(Table table)
        {
            //Arrange
            var parameter = table.CreateSet<ProductViewModel>().First();

            // Act            
            _apiResponse = await ClientApi.PutAsync($"{BaseURL}{parameter.Id}", parameter, new JsonMediaTypeFormatter());
        }


        [Then(@"The result for change product should be (.*)")]
        public async Task ThenTheResultForChangeShouldBe(string Result)
        {
            // Arrange
            var possibleStatusCodes = ResultApi[Result];

            // Assert            
            var responseString = await _apiResponse.Content.ReadAsStringAsync();
            Assert.Contains(_apiResponse.StatusCode, possibleStatusCodes);
        }

        #endregion

        #region Remove

        [Given(@"These products exists on the system for remove")]
        public async Task GivenThereAreProductOnTheSystemForRemove(Table table)
        {
            //Arrange            
            await StartApiAsync();

            var parameters = table.CreateSet<ProductViewModel>();
            var possibleStatusCodes = ResultApi["Success"];

            // Act            
            foreach (var parameter in parameters)
            {
                ApiResponse = await ClientApi.PostAsync(BaseURL, parameter, new JsonMediaTypeFormatter());

                Assert.Contains(ApiResponse.StatusCode, possibleStatusCodes);
            }
        }

        [When(@"I remove the product (.*)")]
        public async Task WhenIRemoveProduct(long Id)
        {
            // Act            
            _apiResponse = await ClientApi.DeleteAsync($"{BaseURL}{Id}");
        }

        [Then(@"The result for remove product should be (.*)")]
        public async Task ThenTheResultForRemoveShouldBe(string Result)
        {
            // Arrange
            var possibleStatusCodes = ResultApi[Result];

            // Assert            
            var responseString = await _apiResponse.Content.ReadAsStringAsync();
            Assert.Contains(_apiResponse.StatusCode, possibleStatusCodes);
        }

        #endregion



        [When(@"I request for all products on the system")]
        public async Task WhenIRequestForAllProductsOnTheSystem()
        {
            //Arrange
            var url = $"{BaseURL}";

            // Act            
            ApiResponse = await ClientApi.GetAsync(url);
        }

        [Then(@"The return for all products on the system should be")]
        public async Task TheReturnForAllProductsOnTheSystemShouldBe(Table ReturnContent)
        {
            // Assert            
            var responseString = await ApiResponse.Content.ReadAsStringAsync();

            // Act
            var returnContent = ReturnContent.CreateSet<ProductViewModel>();

            Assert.True(
                string.Equals(JsonSerializer.Serialize(returnContent), responseString, StringComparison.OrdinalIgnoreCase)
                );

        }

        [Then(@"The result for all products on the system should be (.*)")]
        public async Task TheResultForAllProductsOnTheSystemShouldBe(string Result)
        {
            // Arrange
            var possibleStatusCodes = ResultApi[Result];

            // Assert            
            Assert.Contains(ApiResponse.StatusCode, possibleStatusCodes);
        }
    }
}
