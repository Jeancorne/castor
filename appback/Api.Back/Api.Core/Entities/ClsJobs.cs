using Newtonsoft.Json;

namespace Api.Core.Entities
{
    public class ClsJobs : BaseEntity<Guid>
    {
        public ClsJobs()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; set; } = default!;

        [JsonIgnore]
        public virtual ICollection<ClsEmployees> ClsEmployees { get; set; } = new List<ClsEmployees>();
    }
}