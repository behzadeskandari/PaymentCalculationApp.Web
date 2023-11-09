using PaymentCalculationApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.Dtos.Employee
{
    public record EmployeeFilter(string? FirstName, string? LastName, int? TotalHoursWorked, int? StandardHours,int? OvertimeRate,int? BaseSalary, int? RightToAttract,int? ReceiveRoundTripFees, ICollection<MonthlySalary> MonthlySalaries, int? Skip, int? Take);

}
