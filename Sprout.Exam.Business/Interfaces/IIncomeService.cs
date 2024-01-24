using Sprout.Exam.Common.DataTransferObjects;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IIncomeService
    {
        decimal CalculateNetIncome(CalculateRequestDto calculateRequest, EmployeeDto employee);
    }
}
