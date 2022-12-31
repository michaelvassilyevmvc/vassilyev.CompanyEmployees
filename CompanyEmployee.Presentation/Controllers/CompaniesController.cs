using CompanyEmployee.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

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
            var companies = _service.CompanyService.GetAllCompanies(false);
            return Ok(companies);

        }

        [HttpGet("{id:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, false);
            return Ok(company);
        }

        [HttpGet("collection/{ids}", Name ="CompanyCollection")]
        public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var companies = _service.CompanyService.GetByIds(ids, false);
            return Ok(companies);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if(company is null)
            {
                return BadRequest("CompanyForCreateDto object is null");
            }

            var createdCompany = _service.CompanyService.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new {id=createdCompany.Id}, createdCompany);

        }

        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection([FromBody]IEnumerable<CompanyForCreationDto> company)
        {
            var result = _service.CompanyService.CreateCompanyCollection(company);
            return CreatedAtRoute("CompanyCollection",new {result.ids}, result.companies    );
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCompany(Guid id)
        {
            _service.CompanyService.DeleteCompany(id, false);
            return NoContent();
        }
    }
}
