using System.ComponentModel.DataAnnotations;

namespace Api.Common.Dto.ClsUser
{
    public class AddUserDto
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string LastName { get; set; } = default!;

        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}