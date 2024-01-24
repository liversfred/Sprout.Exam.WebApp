using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Test.Controllers
{
    [TestClass]
    public class EmployeesControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsOkObjectResult_WhenServiceSucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var regularEmployees = Builder<RegularEmployeeDto>.CreateListOfSize(2).Build();
            var contractualEmployees = Builder<ContractualEmployeeDto>.CreateListOfSize(2).Build();
            var response = regularEmployees.Concat<EmployeeDto>(contractualEmployees).ToList();

            testScope.EmployeeServiceMock.Setup(p => p.GetAllEmployeesAsync()).ReturnsAsync(response);

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var model = okObjectResult.Value as List<EmployeeDto>;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task GetById_ReturnsOkObjectResult_WhenServiceSucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var response = Builder<RegularEmployeeDto>.CreateNew().Build();

            testScope.EmployeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(response);

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var model = okObjectResult.Value as EmployeeDto;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task GetById_ThrowException_WhenEmployeeIsNull()
        {
            // Arrange
            var testScope = new DefaultTestScope();

            testScope.EmployeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(It.IsAny<int>()))
                  .ThrowsAsync(new InvalidOperationException("Employee not found."));

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue(result is ObjectResult);

            var internalServerErrorResult = (ObjectResult)result;
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
            Assert.AreEqual("Internal Server Error: Employee not found.", internalServerErrorResult.Value);
        }

        [TestMethod]
        public async Task Put_ReturnsOkObjectResult_WhenServiceSucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = Builder<EditEmployeeDto>.CreateNew().Build();
            var response = Builder<RegularEmployeeDto>.CreateNew().Build();

            testScope.EmployeeServiceMock.Setup(service => service.UpdateEmployeeAsync(It.IsAny<EditEmployeeDto>())).ReturnsAsync(response);

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.Put(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var model = okObjectResult.Value as EmployeeDto;
            Assert.IsNotNull(model);
            Assert.AreEqual(model, response);
        }

        [TestMethod]
        public async Task Put_ThrowException_WhenEmployeeIsNull()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = Builder<EditEmployeeDto>.CreateNew().Build();

            testScope.EmployeeServiceMock.Setup(service => service.UpdateEmployeeAsync(It.IsAny<EditEmployeeDto>()))
                  .ThrowsAsync(new InvalidOperationException("Employee not found."));

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.Put(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue(result is ObjectResult);

            var internalServerErrorResult = (ObjectResult)result;
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
            Assert.AreEqual("Internal Server Error: Employee not found.", internalServerErrorResult.Value);
        }

        [TestMethod]
        public async Task Post_ReturnsOkObjectResult_WhenServiceSucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = Builder<CreateEmployeeDto>.CreateNew().Build();
            var response = 1;

            testScope.EmployeeServiceMock.Setup(service => service.SaveEmployeeAsync(It.IsAny<CreateEmployeeDto>())).ReturnsAsync(response);

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.Post(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));

            var okObjectResult = result as CreatedResult;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(201, okObjectResult.StatusCode);
            Assert.AreEqual(okObjectResult.Value, response);
        }

        [TestMethod]
        public async Task Delete_ReturnsOkObjectResult_WhenServiceSucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = 1;

            testScope.EmployeeServiceMock.Setup(service => service.DeleteEmployeeAsync(It.IsAny<int>())).Verifiable();

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.Delete(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ThrowException_WhenEmployeeIsNull()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = 1;

            testScope.EmployeeServiceMock.Setup(service => service.DeleteEmployeeAsync(It.IsAny<int>()))
                        .ThrowsAsync(new InvalidOperationException("Employee not found."));

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.Delete(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue(result is ObjectResult);

            var internalServerErrorResult = (ObjectResult)result;
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
            Assert.AreEqual("Internal Server Error: Employee not found.", internalServerErrorResult.Value);
        }

        [TestMethod]
        public async Task Calculate_ReturnsCorrectNetIncome_WhenServiceSucceeds()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var response = Builder<RegularEmployeeDto>.CreateNew().Build();
            var request = Builder<CalculateRequestDto>.CreateNew().Build();

            testScope.EmployeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(response);
            testScope.IncomeServiceMock.Setup(service => service.CalculateNetIncome(It.IsAny<CalculateRequestDto>(), It.IsAny<EmployeeDto>())).Returns(500M);

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.Calculate(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<decimal>));

            var okResult = (OkObjectResult)result.Result;
            Assert.AreEqual(500M, okResult.Value);
        }

        [TestMethod]
        public async Task Calculate_ThrowException_WhenEmployeeIsNull()
        {
            // Arrange
            var testScope = new DefaultTestScope();
            var request = Builder<CalculateRequestDto>.CreateNew().Build();

            testScope.EmployeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(It.IsAny<int>()))
                  .ThrowsAsync(new InvalidOperationException("Employee not found."));

            var controller = new EmployeesController(testScope.EmployeeServiceMock.Object, testScope.IncomeServiceMock.Object);

            // Act
            var result = await controller.Calculate(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<decimal>));
            Assert.IsTrue(result.Result is ObjectResult);

            var internalServerErrorResult = (ObjectResult)result.Result;
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
            Assert.AreEqual("Internal Server Error: Employee not found.", internalServerErrorResult.Value);
        }

        private class DefaultTestScope
        {
            public Mock<IEmployeeService> EmployeeServiceMock { get; set; }
            public Mock<IIncomeService> IncomeServiceMock { get; set; }

            public DefaultTestScope()
            {
                EmployeeServiceMock = new();
                IncomeServiceMock = new();
            }
        }

    }
}
