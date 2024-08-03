using System.ComponentModel.DataAnnotations;

namespace Api.Core.Entities
{
    public class ClsUser : BaseEntity<Guid>
    {
        public ClsUser()
        {
            Id = Guid.NewGuid();
        }

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