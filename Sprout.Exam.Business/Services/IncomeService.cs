using Microsoft.Extensions.Options;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.Confifurations;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.Business.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly SalarySettings _salarySettings;

        public IncomeService(IOptions<SalarySettings> salarySettings) 
        {
            _salarySettings = salarySettings.Value ?? throw new ArgumentNullException(nameof(salarySettings), "SalarySettings must not be null.");
        }

        public decimal CalculateNetIncome(CalculateRequestDto calculateRequest, EmployeeDto employee)
        {
            switch (employee.TypeId)
            {
                case (int)EmployeeType.Regular:
                    if (employee is RegularEmployeeDto regularEmployee)
                    {
                        regularEmployee.Salary = _salarySettings.Regular;
                        regularEmployee.TaxRate = _salarySettings.TaxRate;
                        regularEmployee.AbsentDays = calculateRequest.AbsentDays;
                    }
                    else throw new InvalidOperationException($"Unexpected type for RegularEmployeeDto. EmployeeId: {employee.Id}");
                    break;
                case (int)EmployeeType.Contractual:
                    if (employee is ContractualEmployeeDto contractualEmployee)
                    {
                        contractualEmployee.DailyRate = _salarySettings.Contractual;
                        contractualEmployee.WorkedDays = calculateRequest.WorkedDays;
                    }
                    else throw new InvalidOperationException($"Unexpected type for ContractualEmployeeDto. EmployeeId: {employee.Id}");
                    break;

                default:
                    throw new NotImplementedException($"Mapping for employee type id: {employee.TypeId} is not implemented.");
            }

            decimal netIncome = employee.CalculateNetIncome();
            return netIncome > 0 ? RoundDecimalPlaces(netIncome) : 0;
        }

        private decimal RoundDecimalPlaces(decimal value, int decimalCount = 2)
        {
            return Math.Round(value, decimalCount);
        }
    }
}
