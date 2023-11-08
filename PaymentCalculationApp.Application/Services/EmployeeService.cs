using AutoMapper;
using PaymentCalculation.Domain.Dtos.Employee;
using PaymentCalculation.Domain.Entities;
using PaymentCalculation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OvertimePolicies;

namespace PaymentCalculation.Application.Services
{
    public class EmployeeService : IEmployeeService
    {

        public IMapper Mapper { get; }
        public IGenericRepository<Employee> EmployeeRepository { get; }
        
        public EmployeeService(IMapper mapper, IGenericRepository<Employee> employeeRepository)
        {
            Mapper = mapper;
            EmployeeRepository = employeeRepository;
        }

        public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
        {
            
            var entity = Mapper.Map<Employee>(employeeCreate);
            await EmployeeRepository.InsertAsync(entity);
            await EmployeeRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
        {
            var entity = await EmployeeRepository.GetByIdAsync(employeeDelete.id);
            EmployeeRepository.Delete(entity);
            await EmployeeRepository.SaveChangesAsync();
        }

        public async Task<EmployeeDetails> GetEmployeeAsync(int id)
        {
            var entity = await EmployeeRepository
                .GetByIdAsync(id);

            return Mapper.Map<EmployeeDetails>(entity);
        }

        public async Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter)
        {


            Expression<Func<Employee, bool>> firstNameFilter = (employee) => employeeFilter.FirstName == null ? true :
            employee.FirstName.Contains(employeeFilter.FirstName);

            Expression<Func<Employee, bool>> lastNameFilter = (employee) => employeeFilter.LastName == null ? true :
           employee.LastName.Contains(employeeFilter.LastName);

           
            var enteties = await EmployeeRepository.GetFilteredAysnc(new Expression<Func<Employee, bool>>[]
            {
                firstNameFilter, lastNameFilter
            }, employeeFilter.Skip, employeeFilter.Take);

            var mapper = Mapper.Map<List<EmployeeList>>(enteties);
            return mapper;
        }

        public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
        {
            
            var entity = Mapper.Map<Employee>(employeeUpdate);
            
            EmployeeRepository.Update(entity);
            await EmployeeRepository.SaveChangesAsync();

        }

        public Task<double> CalculateSalaryEmployee(EmployeeCreate employeeCreate)
        {
            
            OvertimePolicies.OvertimePolicies overtimePolicies = new();
            var PaymentSalaryResult = employeeCreate.BaseSalary + 
                                           employeeCreate.RightToAttract + 
                                           employeeCreate.ReceiveRoundTripFees - overtimePolicies.CalculateOvertimePay(employeeCreate.RightToAttract, employeeCreate.BaseSalary);

            return Task.Run(() =>
            {
                return PaymentSalaryResult;
            });
        }

        public async Task<List<EmployeeList>> GetAllEmployeesAsync()
        {
            var result = EmployeeRepository.GetAllEmployeesAsync();

            var entity = Mapper.Map<List<EmployeeList>>(result);

            return entity;

        }

        public async Task<List<EmployeeList>> GetCustomData(EmployeeFilter employeeFilter)
        {
            var result  = await GetEmployeesAsync(employeeFilter);
            var entity = Mapper.Map<List<EmployeeList>>(result);
            return entity;

        }

        public async Task UpdateEmployeeMonthAsync(DateTime month,int employeeId)
        {
           var result =  EmployeeRepository.GetByIdAsync(employeeId);

            if (result is not null)
            {
                var entity = Mapper.Map<Employee>(result);
                entity.Month = month;
                var entityUpdate = Mapper.Map<EmployeeUpdate>(entity);
                await UpdateEmployeeAsync(entityUpdate);
            }
        }
    }
}
