using PaymentCalculation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculationApp.Domain.Entities
{
    public class MonthlySalary : BaseEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } // Foreign key to Employee
        public Employee Employee { get; set; } // Navigation property
        public DateTime Month { get; set; } // The month for which the salary is recorded
        public double SalaryAmount { get; set; } // The salary amount for the month
    }
}
