using Anderson.Produtos.Domain.Application.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http.Formatting;
using TechTalk.SpecFlow.Assist;

namespace Anderson.Produtos.SpecFlow.StepDefinitions
{
    public class BaseEndToEndTest
    {
        public IDictionary<string, HttpStatusCode[]> ResultApi { get; } = new Dictionary<string, HttpStatusCode[]>
        {
            { "Success", new [] { HttpStatusCode.OK, HttpStatusCode.Created, HttpStatusCode.NoContent } },
            { "Error", new [] { HttpStatusCode.BadRequest, HttpStatusCode.UnprocessableEntity, HttpStatusCode.Conflict } },
            { "NoContent", new [] { HttpStatusCode.NotFound } },
            { "NoAccess", new [] { HttpStatusCode.Unauthorized, HttpStatusCode.Forbidden } }
        };


        public string BaseURL { get; set; } = "/api/Produtos/";
        protected HttpResponseMessage ApiResponse { get; set; }
        protected HttpClient ClientApi { get; private set; }
        protected string BearerToken { get; }

        protected async Task StartApiAsync()
        {
            if (ClientApi is not null)
                return;

            // Arrange
            var testServer = new TestServer(new WebHostBuilder()
                .UseEnvironment("NarrowIntegratedTest")
                .ConfigureAppConfiguration((context, builder) =>
                {
                    //Override appsettings values
                    builder.AddInMemoryCollection(
                           new Dictionary<string, string>
                           {
                               ["ConnectionStrings:ProductDB"] = "DataSource=:memory:",
                               ["AccessControl"] = "www.test.com/api/login"
                           });
                })
                .UseStartup<WebAPI.Startup>());

            await testServer.Host.StartAsync();
            ClientApi = testServer.CreateClient();

            //var response = await _client.PostAsync("login", new LoginViewModel { Username = "teste", Password = 123456 }, new JsonMediaTypeFormatter());
            //BearerToken = await response.Content.ReadAsStringAsync();
            //Client.DefaultRequestHeaders.Add("Authorization", $"bearer {_bearerToken}");
        }


        [Fact(Skip = "Temp")]
        public async Task Crud_ShouldBeSuccess()
        {
            await StartApiAsync();

            // Act & Asset
            await Post();
            await Get();
            await Put();
            await Get_Id();
            await Delete();
            await Get_Id_NoContent();
        }

        protected async Task Post()
        {
            //Arrange            
            var @return = "1";

            // Act            
            var response = await ClientApi.PostAsync(BaseURL, new ProductViewModel { Name = "teste", Value = 1234.45M }, new JsonMediaTypeFormatter());
            var expectedStatusCode = HttpStatusCode.Created;

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(@return, responseString);
        }

        protected async Task Get()
        {
            //Arrange            
            var @return = "[{\"id\":1,\"name\":\"teste\",\"value\":1234.45,\"imagePath\":null,\"imageStream\":null}]";
            var expectedStatusCode = HttpStatusCode.OK;

            // Act
            var response = await ClientApi.GetAsync(BaseURL);

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(@return, responseString);
        }

        protected async Task Put()
        {
            // Arrange
            var expectedStatusCode = HttpStatusCode.NoContent;

            // Act
            var response = await ClientApi.PutAsync($"{BaseURL}1", new ProductViewModel { Id = 1, Name = "ALTERADO", Value = 1234.45M }, new JsonMediaTypeFormatter());

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }

        protected async Task Get_Id()
        {
            //Arrange            
            var @return = "{\"id\":1,\"name\":\"ALTERADO\",\"value\":1234.45,\"imagePath\":null,\"imageStream\":null}";
            var expectedStatusCode = HttpStatusCode.OK;

            // Act
            var response = await ClientApi.GetAsync($"{BaseURL}1");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(@return, responseString);
        }

        protected async Task Delete()
        {
            //Arrange            
            var expectedStatusCode = HttpStatusCode.NoContent;

            // Act
            var responseDelete = await ClientApi.DeleteAsync($"{BaseURL}1");

            // Assert
            var responseString = await responseDelete.Content.ReadAsStringAsync();
            Assert.Equal(expectedStatusCode, responseDelete.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }

        protected async Task Get_Id_NoContent()
        {
            //Arrange            
            var expectedStatusCode = HttpStatusCode.NoContent;

            // Act
            var response = await ClientApi.GetAsync($"{BaseURL}1");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }
    }
}