using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Application.ViewModels
{
    public class EmployeeDto
    {
        public int TotalHoursWorked { get; set; }
        public int StandardHours { get; set; }
        public double OvertimeRate { get; set; }
        public string EmployeeName { get; set; }

    }

}
