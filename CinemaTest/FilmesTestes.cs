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
        public FilmesTestes(WebApplicationFactory<FilmesModelsController> factory)
        {
            _factore = factory;
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
    }
}
