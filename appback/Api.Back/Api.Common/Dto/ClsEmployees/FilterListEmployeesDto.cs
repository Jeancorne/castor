using Api.Common.Utils;

namespace Api.Common.Dto.ClsEmployees
{
    public class FilterListEmployeesDto : Pagination
    {
        public string? Identification { get; set; } = null;
        public string? Name { get; set; } = null;
    }
}