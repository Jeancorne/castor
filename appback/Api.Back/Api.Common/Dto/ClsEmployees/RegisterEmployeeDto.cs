using System.ComponentModel.DataAnnotations;

namespace Api.Common.Dto.ClsEmployees
{
    public class RegisterEmployeeDto
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Identification { get; set; } = default!;

        [Required]
        public DateTime DateRegistry { get; set; } = default!;

        [Required]
        public Guid IdJobTitle { get; set; } = default!;

        public string? Base64 { get; set; } = null;

        public string? FileName { get; set; } = null;
    }
}