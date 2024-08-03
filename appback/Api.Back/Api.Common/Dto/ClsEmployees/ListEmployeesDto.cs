namespace Api.Common.Dto.ClsEmployees
{
    public class ListEmployeesDto
    {
        public int Id { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Identification { get; set; } = default!;

        public string UrlImage { get; set; } = default!;

        public DateTime DateRegistry { get; set; } = default!;

        public Guid IdJobTitle { get; set; }

        public string? TxtJob { get; set; } = null;
    }
}