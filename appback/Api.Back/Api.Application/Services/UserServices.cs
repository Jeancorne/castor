using Api.Application.Interface;
using Api.Common.Dictionary.ClsUser;
using Api.Common.Dictionary.Generic;
using Api.Common.Dto.ClsUser;
using Api.Common.Resources;
using Api.Common.Utils;
using Api.Core.Entities;
using Api.Infraestructure.Interface;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Api.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private string _KeySecret;
        private readonly LanguageSettings _languageSettings;
        private MessageResource _messageResource = new MessageResource();

        public UserServices(IUnitOfWork unitOfWork, IConfiguration configuration, LanguageSettings languageSettings)
        {
            _unitOfWork = unitOfWork;
            _KeySecret = configuration["KeySecret"]!;
            _languageSettings = languageSettings;
        }

        public async Task<ApiResponse<string>> loginSystem(LoginUserDto login)
        {
            ClsUser? user = (await _unitOfWork.ClsUser.WhereAsync(a => a.Email == login.Email)).FirstOrDefault();
            if (user == null) _messageResource.GetMessage(ClsUserLanguage.Folder, _languageSettings.Language, ClsUserLanguage.IncorrectLogin, true);
            string valPasswordHash = passwordHash(login.Password!);
            if (valPasswordHash != user!.Password) _messageResource.GetMessage(ClsUserLanguage.Folder, _languageSettings.Language, ClsUserLanguage.IncorrectLogin, true);
            Jwt jwtHelper = new Jwt(_KeySecret);
            List<string> listToken = new List<string>();
            string token = jwtHelper.createToken(listToken, user.Id, user.Name, user.LastName);
            return new ApiResponse<string>
            {
                Success = true,
                Message = _messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageConsulted),
                Data = token
            };
        }

        public async Task<ApiResponse<bool>> registerUser(AddUserDto userDto)
        {
            validateEmailUser(userDto.Email);
            ClsUser user = new ClsUser();
            user.Name = userDto.Name;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Password = passwordHash(userDto.Password);
            await _unitOfWork.ClsUser.Add(user);
            int count = await _unitOfWork.SaveChangesAsync();
            string txtRes = count > 0 ? (_messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageRegistered)) : (_messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageErrorRegister));
            return new ApiResponse<bool>
            {
                Success = count > 0,
                Message = txtRes,
                Data = count > 0
            };
        }

        public bool validateEmailUser(string? email)
        {
            ClsUser? user = _unitOfWork.ClsUser.Where(a => a.Email == email).FirstOrDefault();
            if (user != null) _messageResource.GetMessage(ClsUserLanguage.Folder, _languageSettings.Language, ClsUserLanguage.EmailAlreadyExist, true, email);
            return true;
        }

        public static string passwordHash(string password)
        {
            byte[] hash = SHA1.HashData(Encoding.UTF8.GetBytes(password));
            return string.Concat(hash.Select(b => b.ToString("x3")));
        }
    }
}