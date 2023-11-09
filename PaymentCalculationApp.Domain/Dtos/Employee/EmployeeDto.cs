using PaymentCalculationApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.Dtos.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => FirstName + ' ' + LastName;
        public int TotalHoursWorked { get; set; }

        public DateTime Month { get; set; }
        public int StandardHours { get; set; }
        public double OvertimeRate { get; set; }

        public int BaseSalary { get; set; }

        public int RightToAttract { get; set; }

        public int ReceiveRoundTripFees { get; set; }
        ICollection<MonthlySalary> MonthlySalaries { get; set; }
    }

}
