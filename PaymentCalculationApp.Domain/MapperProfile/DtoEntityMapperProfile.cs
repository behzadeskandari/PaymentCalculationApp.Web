using AutoMapper;
using PaymentCalculation.Domain.Dtos.Employee;
using PaymentCalculation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Domain.MapperProfile
{
    public class DtoEntityMapperProfile : Profile
    {
        public DtoEntityMapperProfile()
        {
            //CreateMap<ApplicationUser, UserDto>().ReverseMap();
            
            CreateMap<EmployeeCreate, Employee>();
            CreateMap<EmployeeUpdate, Employee>();
            CreateMap<Employee, EmployeeDetails>().ForMember(dest => dest.Id, opt => opt.Ignore());
//                .ForMember(dest => dest.Job, opt => opt.Ignore())
//                .ForMember(dest => dest.Address, opt => opt.Ignore()); //if have nested property with handle it this way : Comment for mr farid 
            CreateMap<Employee, EmployeeList>();

        }
    }
}
