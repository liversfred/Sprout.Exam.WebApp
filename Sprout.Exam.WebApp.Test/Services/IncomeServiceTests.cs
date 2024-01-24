using FizzWare.NBuilder;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sprout.Exam.Business.Services;
using Sprout.Exam.Common.Confifurations;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Test.Services
{
    [TestClass]
    public class IncomeServiceTests
    {
        [TestMethod]
        public void CalculateNetIncome_ShouldReturnCorrectNetIncome_RegularEmployee()
        {
            // Arrange
            var salarySettings = Options.Create(new SalarySettings
            {
                Regular = 20000M,
                TaxRate = 0.12M
            });
            var incomeService = new IncomeService(salarySettings);
            var calculateRequestDto = Builder<CalculateRequestDto>.CreateNew()
                                        .With(e => e.AbsentDays = 1M)
                                        .Build();
            var regularEmployee = Builder<RegularEmployeeDto>.CreateNew()
                                    .With(e => e.TypeId = (int)EmployeeType.Regular)
                                    .Build();
            var response = 16690.91M;

            // Act
            var result = incomeService.CalculateNetIncome(calculateRequestDto, regularEmployee);

            // Assert
            Assert.AreEqual(response, result);
        }

        [TestMethod]
        public void CalculateNetIncome_ShouldReturnCorrectNetIncome_ContractualEmployee()
        {
            // Arrange
            var salarySettings = Options.Create(new SalarySettings
            {
                Contractual = 500M
            });
            var incomeService = new IncomeService(salarySettings);
            var calculateRequestDto = Builder<CalculateRequestDto>.CreateNew()
                                        .With(e => e.WorkedDays = 15.5M)
                                        .Build();
            var contractualEmployee = Builder<ContractualEmployeeDto>.CreateNew()
                                    .With(e => e.TypeId = (int)EmployeeType.Contractual)
                                    .Build();
            var response = 7750.00M;

            // Act
            var result = incomeService.CalculateNetIncome(calculateRequestDto, contractualEmployee);

            // Assert
            Assert.AreEqual(response, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CalculateNetIncome_ThrowException_NotSupportedEmployeeType()
        {
            // Arrange
            var salarySettings = Options.Create(new SalarySettings
            {
                Regular = 20000M,
                TaxRate = 0.12M,
                Contractual = 500M
            });
            var incomeService = new IncomeService(salarySettings);
            var calculateRequestDto = Builder<CalculateRequestDto>.CreateNew().Build();
            var contractualEmployee = Builder<ContractualEmployeeDto>.CreateNew()
                                    .With(e => e.TypeId = 3)
                                    .Build();

            // Act
            incomeService.CalculateNetIncome(calculateRequestDto, contractualEmployee);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateNetIncome_ThrowException_UnexpectedType()
        {
            // Arrange
            var salarySettings = Options.Create(new SalarySettings
            {
                Regular = 20000M,
                TaxRate = 0.12M,
                Contractual = 500M
            });
            var incomeService = new IncomeService(salarySettings);
            var calculateRequestDto = Builder<CalculateRequestDto>.CreateNew().Build();
            EmployeeDto contractualEmployee = new RegularEmployeeDto()
            {
                TypeId = (int)EmployeeType.Contractual
            };

            // Act
            incomeService.CalculateNetIncome(calculateRequestDto, contractualEmployee);
        }

        private class DefaultTestScope
        {
            public Mock<IOptions<SalarySettings>> SalarySettingsMock { get; set; }

            public DefaultTestScope()
            {
                SalarySettingsMock = new();
            }
        }

    }
}
