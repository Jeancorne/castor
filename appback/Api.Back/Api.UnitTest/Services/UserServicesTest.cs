using Api.Application.Services;
using Api.Common.Dto.ClsUser;
using Api.Common.Utils;
using Api.Core.Entities;
using Api.Infraestructure.Interface;
using Api.UnitTest.Queries;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Api.UnitTest.Services
{
    public class UserServicesTest
    {
        private Mock<IUnitOfWork> _unit = new Mock<IUnitOfWork>();
        private Mock<IRepository<ClsUser, Guid>> _repoClsUser = new();
        private ClsUserRepoMock repoUserMock = new ClsUserRepoMock();
        private IConfiguration configuration;
        private UserServices userServices;

        public UserServicesTest()
        {
            _repoClsUser = repoUserMock.GetRepo();
            _unit.Setup(a => a.ClsUser).Returns(_repoClsUser.Object);
            _unit.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);
            Dictionary<string, string> inMemorySettings = new Dictionary<string, string> {{ "KeySecret", "StrONG-AuThEnTicate-App-Server-aaawwww" } };
            configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            LanguageSettings lang = new LanguageSettings();
            lang.Language = "es_ES";
            userServices = new UserServices(_unit.Object, configuration, lang);
        }

        [Fact]
        public async Task registerUser()
        {
            try
            {
                //Datos
                AddUserDto user = new AddUserDto
                {
                    Name = "name",
                    LastName = "apellido",
                    Email = "prueba1@gmail.com",
                    Password = "123"
                };

                //Validación del correo
                await Assert.ThrowsAsync<BusinessException>(async () => await userServices.registerUser(user));

                //Ejecución ok
                user.Email = "pruebasinerror@gmail.com";
                ApiResponse<bool> actOk = await userServices.registerUser(user);
                Assert.NotNull(actOk);
                Assert.True(actOk.Success);
            }
            catch (Exception)
            {

                throw;
            }            
        }

        [Fact]
        public async Task loginSystem()
        {
            try
            {
                //Datos
                LoginUserDto user = new LoginUserDto
                {
                    Email = "prueba1@gmail.com",
                    Password = "123"
                };
                ApiResponse<string> actOk = await userServices.loginSystem(user);
                Assert.NotNull(actOk);
                Assert.True(actOk.Success);
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        [Fact]
        public async Task validateEmailUser()
        {
            try
            {
                string emailFail = "prueba1@gmail.com";
                string emailOk = "prueba2@gmail.com";
                await Assert.ThrowsAsync<BusinessException>(async () => userServices.validateEmailUser(emailFail));                
                bool resOk = userServices.validateEmailUser(emailOk);                
                Assert.True(resOk);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}