namespace Api.Core.Entities
{
    public abstract class BaseEntity<IdType>
    {
        public virtual IdType? Id { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
    }
}