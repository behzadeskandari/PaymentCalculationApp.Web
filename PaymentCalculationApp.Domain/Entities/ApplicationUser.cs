using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.Entities
{
    public class ApplicationUser : IdentityUser<string>
    {

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + LastName;
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
