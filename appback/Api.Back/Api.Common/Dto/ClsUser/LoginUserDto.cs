using System.ComponentModel.DataAnnotations;

namespace Api.Common.Dto.ClsUser
{
    public class LoginUserDto
    {
        [Required]
        public string? Email { get; set; } = null;

        [Required]
        public string? Password { get; set; } = null;
    }
}