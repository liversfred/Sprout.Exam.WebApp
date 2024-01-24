using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sprout.Exam.Business.Services;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.Common.Entities;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Test.Services
{
    [TestClass]
    public class EmployeeServiceTests
    {
        [TestMethod]
        public async Task GetAllEmployeesAsync_ReturnsAllEmployees_WhenRepositorySucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var random = new Random();
            var response = Builder<Employee>.CreateListOfSize(5)
                .All()
                .With(e => e.EmployeeTypeId = (int)(EmployeeType)random.Next(1, 3))
                .Build().ToList();

            testScope.EmployeeRepositoryMock.Setup(p => p.GetAllEmployeesAsync()).ReturnsAsync(response);

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            var result = await service.GetAllEmployeesAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<EmployeeDto>));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() != 0);
        }

        [TestMethod]
        public async Task GetEmployeeByIdAsync_ReturnsRegularEmployee_WhenRepositorySucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var random = new Random();
            var employee = Builder<Employee>.CreateNew()
                            .With(e => e.EmployeeTypeId = (int)EmployeeType.Regular)
                            .Build();
            var response = Builder<RegularEmployeeDto>.CreateNew()
                            .With(e => e.TypeId = (int)EmployeeType.Regular)
                            .Build();

            testScope.EmployeeRepositoryMock.Setup(p => p.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);
            testScope.MapperMock.Setup(m => m.Map<RegularEmployeeDto>(It.IsAny<Employee>())).Returns(response);

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            var result = await service.GetEmployeeByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, response);
        }

        [TestMethod]
        public async Task GetEmployeeByIdAsync_ReturnsContractualEmployee_WhenRepositorySucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var random = new Random();
            var employee = Builder<Employee>.CreateNew()
                            .With(e => e.EmployeeTypeId = (int)EmployeeType.Contractual)
                            .Build();
            var response = Builder<ContractualEmployeeDto>.CreateNew()
                            .With(e => e.TypeId = (int)EmployeeType.Contractual)
                            .Build();

            testScope.EmployeeRepositoryMock.Setup(p => p.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);
            testScope.MapperMock.Setup(m => m.Map<ContractualEmployeeDto>(It.IsAny<Employee>())).Returns(response);

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            var result = await service.GetEmployeeByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EmployeeDto));
            Assert.AreEqual(result, response);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetEmployeeByIdAsync_ThrowException_WhenEmployeeIsNull()
        {
            // Arrange
            var testScope = new DefaultTestScope();

            testScope.EmployeeRepositoryMock.Setup(p => p.GetEmployeeByIdAsync(It.IsAny<int>()))
                  .ThrowsAsync(new InvalidOperationException("Employee not found."));

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            await service.GetEmployeeByIdAsync(1);
        }

        [TestMethod]
        public async Task SaveEmployeeAsync_ReturnsEmployeeId_WhenRepositorySucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = Builder<CreateEmployeeDto>.CreateNew().Build();
            var employee = Builder<Employee>.CreateNew().Build();
            var response = 1;

            testScope.EmployeeRepositoryMock.Setup(p => p.SaveEmployeeAsync(It.IsAny<Employee>())).ReturnsAsync(response);
            testScope.MapperMock.Setup(m => m.Map<Employee>(It.IsAny<CreateEmployeeDto>())).Returns(employee);

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            var result = await service.SaveEmployeeAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(result, response);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task SaveEmployeeAsync_ThrowException_WhenEmployeeIsNull()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = Builder<CreateEmployeeDto>.CreateNew().Build();

            testScope.EmployeeRepositoryMock.Setup(p => p.SaveEmployeeAsync(It.IsAny<Employee>()))
                  .ThrowsAsync(new Exception("Failed to save employee."));

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            await service.SaveEmployeeAsync(request);
        }

        [TestMethod]
        public async Task UpdateEmployeeAsync_ReturnsEmployee_WhenRepositorySucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = Builder<EditEmployeeDto>.CreateNew()
                            .Build();
            var employee = Builder<Employee>.CreateNew()
                            .With(e => e.EmployeeTypeId = (int)EmployeeType.Regular)
                            .Build();
            var updatedEmployee = Builder<Employee>.CreateNew()
                            .With(e => e.EmployeeTypeId = (int)EmployeeType.Regular)
                            .Build();
            var response = Builder<RegularEmployeeDto>.CreateNew().Build();

            testScope.EmployeeRepositoryMock.Setup(p => p.UpdateEmployeeAsync(It.IsAny<Employee>())).ReturnsAsync(updatedEmployee);
            testScope.MapperMock.Setup(m => m.Map<Employee>(It.IsAny<EditEmployeeDto>())).Returns(employee);
            testScope.MapperMock.Setup(m => m.Map<EmployeeDto>(It.IsAny<Employee>())).Returns(response);

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            var result = await service.UpdateEmployeeAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RegularEmployeeDto));
            Assert.AreEqual(result, response);
        }

        [TestMethod]
        public async Task UpdateEmployeeAsync_ReturnsNull_WhenEmployeeIsNull()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = Builder<EditEmployeeDto>.CreateNew().Build();
            var employee = Builder<Employee>.CreateNew()
                            .With(e => e.EmployeeTypeId = (int)EmployeeType.Regular)
                            .Build();

            testScope.EmployeeRepositoryMock.Setup(p => p.UpdateEmployeeAsync(It.IsAny<Employee>())).ReturnsAsync((Employee)null);
            testScope.MapperMock.Setup(m => m.Map<Employee>(It.IsAny<EditEmployeeDto>())).Returns(employee);

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            var result = await service.UpdateEmployeeAsync(request);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteEmployeeAsync_ReturnsEmployee_WhenRepositorySucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = 1;

            testScope.EmployeeRepositoryMock.Setup(p => p.DeleteEmployeeAsync(It.IsAny<int>())).Verifiable();

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            await service.DeleteEmployeeAsync(request);

            // Assert
            testScope.EmployeeRepositoryMock.Verify(repo => repo.DeleteEmployeeAsync(1), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteEmployeeAsync_ThrowException_WhenEmployeeIsNull()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = 1;

            testScope.EmployeeRepositoryMock.Setup(p => p.DeleteEmployeeAsync(It.IsAny<int>()))
                        .ThrowsAsync(new Exception("Employee not found."));

            var service = new EmployeeService(testScope.EmployeeRepositoryMock.Object, testScope.MapperMock.Object);

            // Act
            await service.DeleteEmployeeAsync(request);
        }

        private class DefaultTestScope
        {
            public Mock<IEmployeeRepository> EmployeeRepositoryMock { get; set; }
            public Mock<IMapper> MapperMock { get; set; }

            public DefaultTestScope()
            {
                EmployeeRepositoryMock = new();
                MapperMock = new();
            }
        }

    }
}
