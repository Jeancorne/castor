using Api.Application.Interface;
using Api.Common.Dictionary.ClsEmployees;
using Api.Common.Dictionary.Generic;
using Api.Common.Dto.ClsEmployees;
using Api.Common.Resources;
using Api.Common.Utils;
using Api.Core.Entities;
using Api.Infraestructure.Interface;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Api.Application.Services
{
    public class EmployeesServices : IEmployeesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private string _hostImages;
        private string _diskImages;
        private readonly LanguageSettings _languageSettings;
        private MessageResource _messageResource = new MessageResource();

        public EmployeesServices(IUnitOfWork unitOfWork, IMapper mapper, LanguageSettings languageSettings, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _languageSettings = languageSettings;
            _hostImages = configuration["HostImages"]!;
            _diskImages = configuration["DiskImages"]!;
        }

        public async Task<ApiResponse<ClsEmployees?>> registerEmployee(RegisterEmployeeDto employeeDto)
        {
            List<ClsEmployees> employeeDocument = await getEmployeeByDocument(employeeDto.Identification);
            if (employeeDocument.Count > 0) _messageResource.GetMessage(ClsEmployeeLanguage.Folder, _languageSettings.Language, ClsEmployeeLanguage.EmployeeByDocumentAlreadyExist, true, employeeDto.Identification);
            ClsEmployees employee = _mapper.Map<ClsEmployees>(employeeDto);
            employee.UrlImage = null;
            if (employeeDto.Base64 != null && employeeDto.FileName != null)
            {
                employee.UrlImage = await SaveImage(employeeDto.Base64, employeeDto.FileName);
            }
            await _unitOfWork.ClsEmployees.Add(employee);
            int count = await _unitOfWork.SaveChangesAsync();
            string txtRes = count > 0 ? (_messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageRegistered)) : (_messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageErrorRegister));
            return new ApiResponse<ClsEmployees?>
            {
                Success = count > 0,
                Message = txtRes,
                Data = count > 0 ? employee : null
            };
        }

        public async Task<string> SaveImage(string base64, string fileName, string? oldUrl = null)
        {
            string newUrl = "";
            if (base64 != null && fileName != null)
            {
                byte[] fileBytes = Convert.FromBase64String(base64);
                string folderPath = _diskImages;
                string fileNameFinal = $"{Guid.NewGuid().ToString()}{fileName}";
                string fullPath = Path.Combine(folderPath, fileNameFinal);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                await File.WriteAllBytesAsync(fullPath, fileBytes);
                newUrl = $"{_hostImages}{fileNameFinal}";
            }
            if (oldUrl != null)
            {
                string url = oldUrl;
                string fileNameOld = Path.GetFileName(url);
                string filePath = Path.Combine(_diskImages, fileNameOld);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            return newUrl;
        }

        public async Task<ApiResponse<ClsEmployees?>> getDetailEmployeeById(int id)
        {
            ClsEmployees? employee = _unitOfWork.ClsEmployees.Where(a => a.Id == id).FirstOrDefault();
            if (employee == null) _messageResource.GetMessage(ClsEmployeeLanguage.Folder, _languageSettings.Language, ClsEmployeeLanguage.EmployeeByIdNotExist, true);
            return new ApiResponse<ClsEmployees?>
            {
                Success = true,
                Message = _messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageUpdated),
                Data = employee
            };
        }

        public async Task<ApiResponse<ClsEmployees?>> updateEmployee(UpdateEmplooyeeDto employeeDto)
        {
            ClsEmployees? employee = _unitOfWork.ClsEmployees.Where(a => a.Id == employeeDto.Id).FirstOrDefault();
            if (employee == null) _messageResource.GetMessage(ClsEmployeeLanguage.Folder, _languageSettings.Language, ClsEmployeeLanguage.EmployeeByIdNotExist, true);
            List<ClsEmployees> employeeDocument = await getEmployeeByDocument(employeeDto.Identification);
            employeeDocument = employeeDocument.Where(a => a.Id != employeeDto.Id).ToList();
            if (employeeDocument.Count > 0) _messageResource.GetMessage(ClsEmployeeLanguage.Folder, _languageSettings.Language, ClsEmployeeLanguage.EmployeeByDocumentAlreadyExist, true, employeeDto.Identification);
            employee = setDataEmployee(employee!, employeeDto);

            if (employeeDto.Base64 != null && employeeDto.FileName != null)
            {
                employee.UrlImage = await SaveImage(employeeDto.Base64, employeeDto.FileName, employee.UrlImage);
            }
            _unitOfWork.ClsEmployees.Update(employee);
            int count = await _unitOfWork.SaveChangesAsync();
            string txtRes = count > 0 ? (_messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageUpdated)) : (_messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageErrorUpdated));
            return new ApiResponse<ClsEmployees?>
            {
                Success = count > 0,
                Message = txtRes,
                Data = count > 0 ? employee : null
            };
        }

        public async Task<List<ClsEmployees>> getEmployeeByDocument(string identification)
        {
            IEnumerable<ClsEmployees> lsEmployees = await _unitOfWork.ClsEmployees.WhereAsync(a => a.Identification == identification);
            return lsEmployees.ToList();
        }

        public async Task<ApiResponse<List<ClsJobs?>>> getAllListJobs()
        {
            IEnumerable<ClsJobs?> lsJobs = await _unitOfWork.ClsJobs.GetAllAsync();
            return new ApiResponse<List<ClsJobs?>>
            {
                Success = true,
                Message = _messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageConsulted),
                Data = lsJobs.ToList()
            };
        }

        public async Task<ApiResponse<MetaData<IEnumerable<ListEmployeesDto>>>> getListEmployees(FilterListEmployeesDto filter)
        {
            List<ClsEmployees?> lsEmployees = _unitOfWork.ClsEmployees.GetAll().ToList().OrderByDescending(a => a.CreatedAt).ToList();
            ViewListDto<ListEmployeesDto> view = getTableEmployees(lsEmployees, filter);
            MetaData<IEnumerable<ListEmployeesDto>> metadata = new MetaData<IEnumerable<ListEmployeesDto>>(view.List)
            {
                CountTotal = view!.Count,
                TotalCount = view.List.Count,
                PageSize = view.List.PageSize,
                CurrentPage = view.List.CurrentPage,
                TotalPages = view.List.TotalPages,
                HasNextPage = view.List.HasNextPage,
                HasPreviousPage = view.List.HasPreviousPage,
            };
            return new ApiResponse<MetaData<IEnumerable<ListEmployeesDto>>>
            {
                Success = true,
                Message = _messageResource.GetMessage(GenericLanguage.Folder, _languageSettings.Language, GenericLanguage.MessageConsulted),
                Data = metadata
            };
        }

        private ViewListDto<ListEmployeesDto> getTableEmployees(List<ClsEmployees> lsData, FilterListEmployeesDto filter)
        {
            ViewListDto<ListEmployeesDto> viewDto = new ViewListDto<ListEmployeesDto>();
            filter.PageNumber = filter.PageNumber == 0 ? 1 : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? 10 : filter.PageSize;
            List<ListEmployeesDto> lsView = new List<ListEmployeesDto>();
            if (filter.Identification != null)
            {
                filter.Identification = filter.Identification.Trim().ToLower();
                lsData = lsData.Where(a => a.Identification.Trim().Contains(filter.Identification)).ToList();
            }
            if (filter.Name != null)
            {
                filter.Name = filter.Name.Trim().ToLower();
                lsData = lsData.Where(a => a.Name.Trim().ToLower().Contains(filter.Name)).ToList();
            }
            List<ClsJobs?> lsJobs = _unitOfWork.ClsJobs.GetAll().ToList();
            foreach (var item in lsData ?? Enumerable.Empty<ClsEmployees>())
            {
                string? txtJob = item.IdJobTitle != Guid.Empty ? (lsJobs.FirstOrDefault(e => e.Id == item.IdJobTitle)?.Name) : (null);
                lsView.Add(new ListEmployeesDto()
                {
                    Id = item.Id,
                    Identification = item.Identification,
                    Name = item.Name,
                    UrlImage = item.UrlImage,
                    DateRegistry = item.DateRegistry,
                    IdJobTitle = item.IdJobTitle,
                    TxtJob = txtJob
                });
            }
            viewDto.Count = lsData?.Count();
            PagedList<ListEmployeesDto> paged = PagedList<ListEmployeesDto>.Create(lsView, filter.PageNumber, filter.PageSize);
            viewDto.List = paged;
            return viewDto;
        }

        private ClsEmployees setDataEmployee(ClsEmployees employee, UpdateEmplooyeeDto newEmployee)
        {
            employee.Identification = newEmployee.Identification;
            employee.Name = newEmployee.Name;
            employee.UrlImage = newEmployee.UrlImage;
            employee.DateRegistry = newEmployee.DateRegistry;
            employee.IdJobTitle = newEmployee.IdJobTitle;
            return employee;
        }
    }
}