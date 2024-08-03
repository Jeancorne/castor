using System.ComponentModel.DataAnnotations;

namespace Api.Core.Entities
{
    public class ClsEmployees : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Identification { get; set; } = default!;

        public string? UrlImage { get; set; } = null;

        [Required]
        public DateTime DateRegistry { get; set; } = default!;

        [Required]
        public Guid IdJobTitle { get; set; }

        public virtual ClsJobs? IdJobTitleNavigation { get; set; } = null;
    }
}