using Anderson.Produtos.Domain.Application.ViewModels;
using RestSharp;
using System.Net;
using System.Net.Http.Formatting;
using System.Reflection.Metadata;
using System.Text.Json;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.CommonModels;
using Xunit.Sdk;

namespace Anderson.Produtos.SpecFlow.StepDefinitions
{
    [Binding]
    public sealed class ProductRetrieveStepDefinition : BaseEndToEndTest
    {
        public ProductRetrieveStepDefinition()
        {
            BaseURL = "/api/Produtos/";
        }

        //appconfig em portugues? 

        #region Retrieve All

        [Given(@"These products exists on the system for retrieve all")]
        public async Task GivenThereAreProductOnTheSystemForRetrieveAll(Table table)
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

        [When(@"I request for all products")]
        public async Task WhenIRequestFor()
        {
            //Arrange
            var url = $"{BaseURL}";

            // Act            
            ApiResponse = await ClientApi.GetAsync(url);
        }

        [Then(@"The return for for all products should be")]
        public async Task ThenTheReturnForRequestShouldBe(Table ReturnContent)
        {
            // Assert            
            var responseString = await ApiResponse.Content.ReadAsStringAsync();

            var returnContent = ReturnContent.CreateSet<ProductViewModel>();
            Assert.True(
                string.Equals(JsonSerializer.Serialize(returnContent), responseString, StringComparison.OrdinalIgnoreCase)
                    );
        }

        #endregion


        #region Retrieve One

        [Given(@"These products exists on the system for retrieve one")]
        public async Task GivenThereAreProductOnTheSystemForRetrieveOne(Table table)
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

        [When(@"I request for Id (.*)")]
        public async Task WhenIRequestForId(long Id)
        {
            //Arrange
            var url = $"{BaseURL}{Id}";

            // Act            
            ApiResponse = await ClientApi.GetAsync(url);
        }

        [Then(@"The return for request of product should be")]
        public async Task ThenTheReturnForRequestOfProductShouldBe(Table ReturnContent)
        {
            // Assert            
            var responseString = await ApiResponse.Content.ReadAsStringAsync();

            // Act
            foreach (var returnContent in ReturnContent.CreateSet<ProductViewModel>())
            {
                Assert.True(
                    string.Equals(JsonSerializer.Serialize(returnContent), responseString, StringComparison.OrdinalIgnoreCase)
                    );
            }
        }

        #endregion


        [Then(@"The result for retrieve should be (.*)")]
        public async Task ThenTheResultForRetrieveShouldBe(string Result)
        {
            // Arrange
            var possibleStatusCodes = ResultApi[Result];

            // Assert            
            Assert.Contains(ApiResponse.StatusCode, possibleStatusCodes);
        }
    }
}