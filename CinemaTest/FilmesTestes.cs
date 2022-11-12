using Cinema.Controllers.ModelsController;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CinemaTest
{
    public class FilmesTestes : IClassFixture<WebApplicationFactory<FilmesModelsController>>
    {
        private readonly WebApplicationFactory<FilmesModelsController> _factore;
        private readonly HttpClient _client;
        public FilmesTestes(WebApplicationFactory<FilmesModelsController> factory)
        {
            _factore = factory;
            _client = _factore.CreateClient();
        }


        [Theory]
        [InlineData("FilmesModels/Index")]
        [InlineData("FilmesModels/Create")]
        [InlineData("FilmesModels/Delete")]
        [InlineData("FilmesModels/Details")]
        [InlineData("FilmesModels/Edit")]

        public async Task TestAllPages(string url)
        {
            var client = _factore.CreateClient();

            var response = await client.GetAsync(url);
            int code = (int)response.StatusCode;
            Assert.Equal(200, code);
        }


        [Theory]
        [InlineData("O Senhor dos Anéis: A Sociedade do Anél")]

        public async Task TesteFilmes(string titulo)
        {
            var response = await _client.GetAsync("FilmesModels/Index");
            var pageContent = await response.Content.ReadAsStringAsync();

            var contentString = pageContent.ToString();

            Assert.Contains(titulo, contentString);
        }
    }
}
