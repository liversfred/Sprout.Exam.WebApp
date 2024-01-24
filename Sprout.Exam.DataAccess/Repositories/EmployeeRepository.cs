using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Common.Entities;
using Sprout.Exam.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SproutDbContext _dbContext;

        public EmployeeRepository(SproutDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _dbContext.Employee.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Employee.FindAsync(id) ?? throw new InvalidOperationException("Employee not found.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveEmployeeAsync(Employee employee)
        {
            try
            {
                _dbContext.Employee.Add(employee);
                await _dbContext.SaveChangesAsync();

                return employee.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                var existingEmployee = await _dbContext.Employee.FindAsync(employee.Id) ?? throw new InvalidOperationException("Employee not found.");

                existingEmployee.FullName = employee.FullName;
                existingEmployee.Birthdate = employee.Birthdate;
                existingEmployee.TIN = employee.TIN;
                existingEmployee.EmployeeTypeId = employee.EmployeeTypeId;

                await _dbContext.SaveChangesAsync();

                return existingEmployee;
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
                var employeeToDelete = _dbContext.Employee.Find(id) ?? throw new InvalidOperationException("Employee not found.");
                _dbContext.Employee.Remove(employeeToDelete);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
