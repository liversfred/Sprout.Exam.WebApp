using AutoMapper;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.Common.Entities;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                return employees.Select(employee => MapEmployeeDto(employee)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                return MapEmployeeDto(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                return await _employeeRepository.SaveEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(EditEmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);

                if (updatedEmployee == null) return null;

                return MapEmployeeDto(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                await _employeeRepository.DeleteEmployeeAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private EmployeeDto MapEmployeeDto(Employee employee)
        {
            switch (employee.EmployeeTypeId)
            {
                case (int)EmployeeType.Regular:
                    return _mapper.Map<RegularEmployeeDto>(employee);
                case (int)EmployeeType.Contractual:
                    return _mapper.Map<ContractualEmployeeDto>(employee);
                default:
                    throw new NotImplementedException($"Mapping for employee type id: {employee.EmployeeTypeId} is not implemented.");
            }
        }
    }
}
