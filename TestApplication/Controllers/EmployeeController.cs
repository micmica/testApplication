using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Models;
using TestApplication.Models.DtoModels;
using TestApplication.Models.QueryModels;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeQuery model)
        {
            try
            {
                string titleString = model.Title;

                if (Enum.TryParse(titleString, ignoreCase: true, out TitleEnum titleEnumValue))
                {
                    var employee = await _employeeService.CreateEmployeeAsync(
                        model.Email,
                        titleEnumValue,
                        model.CompanyIds
                    );

                    var dto = _mapper.Map<EmployeeDto>( employee );
                    return Ok(dto);
                }
                else
                {
                    // in case string cannot be converted to a titleenum
                    throw new ArgumentException("Invalid title string");
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { key = ex.Key, message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}