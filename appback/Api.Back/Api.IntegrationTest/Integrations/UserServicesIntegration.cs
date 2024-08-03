using Api.Common.Dto.ClsUser;
using Api.Common.Utils;
using Api.Infraestructure.Data;
using Api.Infraestructure.Injection;
using Api.IntegrationTest.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit.Extensions.Ordering;

namespace Api.IntegrationTest.Integrations
{
    [Collection("UserServicesIntegration"), Order(1)]
    public class UserServicesIntegration : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public string _url = "https://localhost:7084/";
        public HttpClientHelper _httpClientHelper;

        public UserServicesIntegration(WebApplicationFactory<Program> factory)
        {
            DbContextOptions<DBContext> options = new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase("TestDatabase").Options;
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DBContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                    var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
                    services.AddDbContext(configuration, true);
                });
            });
            _httpClientHelper = new HttpClientHelper(_factory);
        }

        [Theory, Order(1)]
        [InlineData("api/v1/user/registerUser")]
        public async Task IntegrationRegisterUser(string url)
        {
            try
            {
                url = $"{_url}{url}";
                AddUserDto data = new AddUserDto()
                {
                    Name = "name",
                    LastName = "apellido",
                    Email = "prueba1@gmail.com",
                    Password = "password"
                };
                ApiResponse<object> response1 = await _httpClientHelper.PostAsync(url, data);
                ApiResponse<object> response2 = await _httpClientHelper.PostAsync(url, data);
                Assert.True(response1.Success);
                Assert.True(!response2.Success);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Theory, Order(2)]
        [InlineData("api/v1/user/loginSystem")]
        public async Task IntegrationLoginSystem(string url)
        {
            try
            {
                LoginUserDto loginUserDto = new LoginUserDto();
                loginUserDto.Email = "prueba1@gmail.com";
                loginUserDto.Password = "password";
                url = $"{_url}{url}";
                ApiResponse<object> response1 = await _httpClientHelper.PostAsync(url, loginUserDto);
                Assert.True(response1.Success);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}