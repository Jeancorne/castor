namespace Api.Common.Dto.ClsJobs
{
    public class ListJobsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}