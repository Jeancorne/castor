using Api.Common.Dto.ClsEmployees;
using Api.Common.Utils;
using Api.Core.Entities;

namespace Api.Application.Interface
{
    public interface IEmployeesServices
    {
        Task<ApiResponse<ClsEmployees?>> registerEmployee(RegisterEmployeeDto employeeDto);

        Task<ApiResponse<ClsEmployees?>> updateEmployee(UpdateEmplooyeeDto employeeDto);

        Task<ApiResponse<ClsEmployees?>> getDetailEmployeeById(int id);

        Task<ApiResponse<MetaData<IEnumerable<ListEmployeesDto>>>> getListEmployees(FilterListEmployeesDto filter);

        Task<ApiResponse<List<ClsJobs?>>> getAllListJobs();
    }
}