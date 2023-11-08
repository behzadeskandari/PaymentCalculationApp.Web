using Microsoft.AspNetCore.Mvc;
using PaymentCalculation.Domain.Dtos.Employee;
using PaymentCalculation.Domain.Entities;
using PaymentCalculation.Domain.Repositories;
using PaymentCalculationApp.Web.Helper;
using System.Threading.Tasks;

namespace PaymentCalculation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService EmployeeService { get; }

        public EmployeeController(IEmployeeService employeeService)
        {
            EmployeeService = employeeService;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateEmployee(EmployeeCreate addressCreate)
        {
            var id = await EmployeeService.CreateEmployeeAsync(addressCreate);
            return Ok(id);

        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdate addressUpdate)
        {
            await EmployeeService.UpdateEmployeeAsync(addressUpdate);
            return Ok();
        }

        [HttpPut("update/{employeeId}")]
        public IActionResult UpdateEmployeeForMonth(int employeeId,[FromBody] DateTime month)
        {
            // Retrieve the employee from your data store (e.g., database)
            var existingEmployee = EmployeeService.GetEmployeeAsync(employeeId);

            if (existingEmployee == null)
            {
                return NotFound($"Employee with ID {employeeId} not found.");
            }

            
            // Update the existing employee's information for the specified month
            EmployeeService.UpdateEmployeeMonthAsync(month, employeeId);
            
            // Save the updated information to your data store

            return Ok(existingEmployee); // Return the updated employee as confirmation
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteEmployee(EmployeeDelete addressUpdate)
        {
            await EmployeeService.DeleteEmployeeAsync(addressUpdate);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var t = await EmployeeService.GetEmployeeAsync(id);
            return Ok(t);

        }

        [HttpGet]
        [Route("Get/Json")]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilter employeeFilter)
        {
            var t = await EmployeeService.GetEmployeesAsync(employeeFilter);
            return Ok(t);

        }

        [HttpGet("xml")]
        [Produces("application/xml")]
        public async Task<IActionResult> GetEmployeesAsXml([FromQuery] EmployeeFilter employeeFilter)
        {
            var t = await EmployeeService.GetEmployeesAsync(employeeFilter);
            return Ok(t);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CalculateSalaryEmployee(EmployeeCreate addressCreate)
        {
            var result = await EmployeeService.CalculateSalaryEmployee(addressCreate);

            return Ok(result);

        }

        [HttpGet]
        [Produces("application/custom")] // Specify the custom media type
        [Route("CustomData")]
        public async Task<IActionResult>  GetCustomData([FromQuery] EmployeeFilter employeeFilter)
        {
            var data = await EmployeeService.GetCustomData(employeeFilter);
            return Ok(data);
        }
    }
}
