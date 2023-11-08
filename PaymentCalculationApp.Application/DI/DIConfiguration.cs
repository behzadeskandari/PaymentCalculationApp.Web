using Microsoft.Extensions.DependencyInjection;
using PaymentCalculation.Domain.Repositories;
using PaymentCalculation.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.DI
{
    public class DIConfiguration
    {
        public static void RegisterServices(IServiceCollection services)
        {


            
            services.AddScoped<IEmployeeService, EmployeeService>();
            
        }
    }
}
