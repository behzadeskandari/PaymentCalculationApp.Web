using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IMapper Mapper { get; }
        private IEmployeeService EmployeeService { get; }

        public EmployeeController(IMapper mapper,IEmployeeService employeeService)
        {
            Mapper = mapper;
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
        public IActionResult UpdateEmployeeForMonth([FromBody] DateTime month,EmployeeDto employeeDto)
        {


            // Retrieve the employee from your data store (e.g., database)
            var existingEmployee = EmployeeService.GetEmployeeAsync(employeeDto.Id);

            if (existingEmployee == null)
            {
                return NotFound($"Employee with ID {employeeDto.Id} not found.");
            }

            
            // Update the existing employee's information for the specified month
            EmployeeService.UpdateEmployeeMonthAsync(month, employeeDto);
            
            // Save the updated information to your data store

            return Ok(existingEmployee); // Return the updated employee as confirmation
        }


        [HttpDelete("delete-info/{employeeId}/{targetMonth}")]
        public IActionResult DeleteEmployeeInfoForMonth(int employeeId, DateTime targetMonth)
        {
            // Retrieve the employee from the database based on the employeeId
            var existingEmployee = EmployeeService.GetEmployeeAsync(employeeId);

            if (existingEmployee is null)
            {
                return NotFound("Employee not found.");
            }
            var entity = Mapper.Map<EmployeeDelete>(existingEmployee.Result);


            var result = EmployeeService.DeleteEmployeeAsync(entity);
           
            return Ok(result);
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
            var result =  EmployeeService.CalculateSalaryEmployee(addressCreate);

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
