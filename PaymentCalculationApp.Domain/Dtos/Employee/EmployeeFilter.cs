using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.Dtos.Employee
{
    public record EmployeeFilter(string? FirstName, string? LastName, int? TotalHoursWorked, int? StandardHours,int? OvertimeRate,int? BaseSalary, int? RightToAttract,int? ReceiveRoundTripFees,int? Skip, int? Take);

}
