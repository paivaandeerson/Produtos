using Anderson.Produtos.Domain.Application.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Anderson.Produtos.IntegrationTest
{
    public class ProdutoEndToEndTest
    {
        private IHost _host;
        private HttpClient _client;
        //(success)
        //create        
        //getbyid
        //update
        //remove

        [Fact(Skip = "Demora na implementação")]
        public async Task Crud_DeveResultarEmSucesso()
        {
            await ArrangeClient();

            // Act & Asset
            await Post();
            await Get();
            await Get_Id();
            await Put();
            await Delete();
        }

        private async Task ArrangeClient()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<WebAPI.Startup>();
                });

            _host = await hostBuilder.StartAsync();
            _client = _host.GetTestClient();
        }

        private async Task Post()
        {
            //Arrange            
            var @return = "[]";

            // Act
            var response = await _client.PostAsJsonAsync("/Produtos/",
                JsonSerializer.Serialize(new ProdutoViewModel { Nome = "teste", Valor = 1234.45M }));

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(@return, responseString);
        }

        private async Task Get()
        {
            //Arrange            
            var @return = "[]";

            // Act
            var response = await _client.GetAsync("/Produtos/");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(@return, responseString);
        }

        private async Task Put()
        {
            // Act
            var response = await _client.PutAsJsonAsync("/api/Produtos/",
                JsonSerializer.Serialize(new ProdutoViewModel { Id = 1, Nome = "ALTERADO", Valor = 1234.45M }));

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(string.Empty, responseString);
        }

        private async Task Get_Id()
        {
            // Arrange
            await ArrangeClient();

            // Act
            var response = await _client.GetAsync("/api/Produtos/1");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //Assert.Equal("Hello World!", responseString);
        }

        private async Task Delete()
        {
            // Arrange
            await ArrangeClient();

            // Act
            var response = await _client.DeleteAsync("/api/Produtos/1");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Hello World!", responseString);
        }

    }
}
