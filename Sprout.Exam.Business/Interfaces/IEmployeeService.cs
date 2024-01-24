using Sprout.Exam.Common.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<int> SaveEmployeeAsync(CreateEmployeeDto employee);
        Task<EmployeeDto> UpdateEmployeeAsync(EditEmployeeDto employee);
        Task DeleteEmployeeAsync(int id);
    }
}
