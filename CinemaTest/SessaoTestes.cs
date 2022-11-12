using Cinema.Controllers.ModelsController;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTest
{
    public class SessaoTestes : IClassFixture<WebApplicationFactory<SessaoModelsController>>
    {
        private readonly WebApplicationFactory<SessaoModelsController> _factore;
        public SessaoTestes(WebApplicationFactory<SessaoModelsController> factory)
        {
            _factore = factory;
        }


        [Theory]
        [InlineData("SessaoModels/Index")]
        [InlineData("SessaoModels/Create")]
        [InlineData("SessaoModels/Delete")]
        [InlineData("SessaoModels/Details")]
        [InlineData("SessaoModels/Edit")]

        public async Task TestAllPages(string url)
        {
            var client = _factore.CreateClient();

            var response = await client.GetAsync(url);
            int code = (int)response.StatusCode;
            Assert.Equal(200, code);
        }


    }
}

