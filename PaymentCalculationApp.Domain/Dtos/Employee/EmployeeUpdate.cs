using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.Dtos.Employee
{
    public record EmployeeUpdate(string FirstName, string LastName, DateTime Month, int TotalHoursWorked, int StandardHours, double OvertimeRate,int BaseSalary,int RightToAttract,int ReceiveRoundTripFees);
}
