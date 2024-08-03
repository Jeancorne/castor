using Api.Application.Interface;
using Api.Common.Dto.ClsEmployees;
using Api.Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeesServices _employeesServices;

        public EmployeeController(IEmployeesServices employeesServices)
        {
            _employeesServices = employeesServices;
        }

        /// <summary>
        /// Registrar un empleado
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost("registerEmployee")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult<IActionResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> registerEmployee(RegisterEmployeeDto employeeDto)
        {
            try
            {
                return Ok(await _employeesServices.registerEmployee(employeeDto));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiException(e));
            }
        }

        /// <summary>
        /// Actualizar un empleado
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPut("updateEmployee")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult<IActionResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> updateEmployee(UpdateEmplooyeeDto employeeDto)
        {
            try
            {
                return Ok(await _employeesServices.updateEmployee(employeeDto));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiException(e));
            }
        }

        /// <summary>
        /// Listado de los empleados paginado
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        [HttpPost("getListEmployees")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult<IActionResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> getListEmployees(FilterListEmployeesDto filterDto)
        {
            try
            {
                return Ok(await _employeesServices.getListEmployees(filterDto));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiException(e));
            }
        }

        /// <summary>
        /// Detalle del empleado por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getDetailEmployeeById/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult<IActionResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> getDetailEmployeeById(int id)
        {
            try
            {
                return Ok(await _employeesServices.getDetailEmployeeById(id));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiException(e));
            }
        }

        /// <summary>
        /// Obtiene el listado de empleos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getAllListJobs")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult<IActionResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> getAllListJobs()
        {
            try
            {
                return Ok(await _employeesServices.getAllListJobs());
            }
            catch (Exception e)
            {
                return BadRequest(new ApiException(e));
            }
        }
    }
}