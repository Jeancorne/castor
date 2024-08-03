using System.ComponentModel.DataAnnotations;

namespace Api.Common.Dto.ClsEmployees
{
    public class UpdateEmplooyeeDto
    {
        [Required]
        public int Id { get; set; } = default!;

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Identification { get; set; } = default!;

        public string? UrlImage { get; set; } = null;

        [Required]
        public DateTime DateRegistry { get; set; }

        [Required]
        public Guid IdJobTitle { get; set; }

        public string? Base64 { get; set; } = null;

        public string? FileName { get; set; } = null;
    }
}