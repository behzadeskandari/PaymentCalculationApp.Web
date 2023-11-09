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
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using PaymentCalculationApp.Domain.Entities;

namespace PaymentCalculation.Application.Services
{
    public class EmployeeService : IEmployeeService
    {

        public IMapper Mapper { get; }
        public IGenericRepository<Employee> EmployeeRepository { get; }
        public IGenericRepository<MonthlySalary> MonthlySalaryRepository { get; }
        
        public EmployeeService(IMapper mapper, IGenericRepository<Employee> employeeRepository, IGenericRepository<MonthlySalary> monthlySalaryRepository)
        {
            Mapper = mapper;
            EmployeeRepository = employeeRepository;
            MonthlySalaryRepository = monthlySalaryRepository;
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
        public async Task DeleteEmployeeAsync(EmployeeDetails employeeDetails, DateTime targetMonth)
        {
            var entity = await EmployeeRepository.GetByIdAsync(employeeDetails.Id);
            if (entity == null)
            {
                throw new Exception("Employee not found.");
            }
            var result = await MonthlySalaryRepository.GetAysnc(null, null, ms => ms.EmployeeId == employeeDetails.Id && ms.Month.Year == targetMonth.Year && ms.Month.Month == targetMonth.Month);

            if (result == null)
            {
                throw new Exception("Information for the specified month not found.");
            }
            EmployeeRepository.Delete(entity);
            MonthlySalaryRepository.DeleteRange(result);
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

        public double CalculateSalaryEmployee(EmployeeCreate employeeCreate)
        {
            
            OvertimePolicies.OvertimePolicies overtimePolicies = new();
            var PaymentSalaryResult = employeeCreate.BaseSalary + 
                                           employeeCreate.RightToAttract + 
                                           employeeCreate.ReceiveRoundTripFees - overtimePolicies.CalculateOvertimePay(employeeCreate.RightToAttract, employeeCreate.BaseSalary);

            
           return PaymentSalaryResult;
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

        public async Task UpdateEmployeeMonthAsync(DateTime month,EmployeeDto employeeDto)
        {
           var result =  EmployeeRepository.GetByIdAsync(employeeDto.Id);

            if (result is not null)
            {
                var entity = Mapper.Map<Employee>(result);
                if (entity.Month == month)
                {
                    entity.StandardHours = employeeDto.StandardHours;
                    entity.TotalHoursWorked = employeeDto.TotalHoursWorked;
                    entity.FirstName = employeeDto.FirstName;
                    entity.LastName = employeeDto.LastName;
                    entity.BaseSalary = employeeDto.BaseSalary;
                    entity.Month = employeeDto.Month;
                    entity.RightToAttract = employeeDto.RightToAttract;
                    entity.ReceiveRoundTripFees = employeeDto.ReceiveRoundTripFees;
                    entity.OvertimeRate = employeeDto.OvertimeRate;
                    var EmpUpdate =  Mapper.Map<EmployeeCreate>(employeeDto);

                    var CalculateResult = CalculateSalaryEmployee(EmpUpdate);

                    entity.MonthlySalaries = new List<MonthlySalary>()
                    {
                        new MonthlySalary()
                        {
                            Month = month,
                            EmployeeId = employeeDto.Id,
                            SalaryAmount = CalculateResult,
                        }
                    };
                }

                var entityUpdate = Mapper.Map<EmployeeUpdate>(entity);
                await UpdateEmployeeAsync(entityUpdate);
            }
        }

        

    }
}
