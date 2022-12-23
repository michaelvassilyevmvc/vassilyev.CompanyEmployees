using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployee.Presentation.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            throw new Exception("Exception");
            var companies = _service.CompanyService.GetAllCompanies(false);
            return Ok(companies);

        }
    }
}
