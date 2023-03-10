using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployee.Presentation.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId)
        {
            var employees = await _service.EmployeeService.GetEmployeesAsync(companyId, false);
            return Ok(employees);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = await _service.EmployeeService.GetEmployeeAsync(companyId, id, false);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
            {
                return BadRequest("EmployeeDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var employeeToReturn = await _service.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, false);
            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, 
            Guid id)
        {
            await _service.EmployeeService.DeleteEmployeeForCompanyAsync(
                    companyId,
                    id,
                    false
                );
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id,
            [FromBody] EmployeeForUpdateDto employee)
        {
            if(employee is null)
            {
                return BadRequest("EmployeeForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }


            await _service.EmployeeService.UpdateEmployeeForCompanyAsync(
                    companyId,
                    id,
                    employee,
                    false,
                    true
                );

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(
            Guid companyId,
            Guid id,
            [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc
            )
        {
            if(patchDoc is null)
            {
                return BadRequest("patchDoc object sent from client is null");
            }

            var result = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id, false, true);
            patchDoc.ApplyTo(result.employeeToPatch);

            TryValidateModel(result.employeeToPatch);
            
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }


            await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntity);
            return NoContent();

        }
    }
}
