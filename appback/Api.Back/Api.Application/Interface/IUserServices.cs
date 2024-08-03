using Api.Common.Dto.ClsUser;
using Api.Common.Utils;

namespace Api.Application.Interface
{
    public interface IUserServices
    {
        Task<ApiResponse<string>> loginSystem(LoginUserDto login);

        Task<ApiResponse<bool>> registerUser(AddUserDto userDto);

        bool validateEmailUser(string? email);
    }
}