using PaymentCalculation.Domain.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.Repositories
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate);
        Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate);
        Task UpdateEmployeeMonthAsync(DateTime month,int employeeId);

        Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter);
        Task<List<EmployeeList>> GetAllEmployeesAsync();
        Task<EmployeeDetails> GetEmployeeAsync(int id);

        Task DeleteEmployeeAsync(EmployeeDelete employeeDelete);

        Task<double> CalculateSalaryEmployee(EmployeeCreate employeeCreate);

        Task<List<EmployeeList>> GetCustomData(EmployeeFilter employeeFilter);
    }
}
