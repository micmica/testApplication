using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Models.DtoModels;
using TestApplication.Models.QueryModels;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyQuery model)
        {
            try
            {
                var company = await _companyService.CreateCompanyAsync(model);
                var dto = _mapper.Map<CompanyDto>(company);

                return Ok(dto);
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
