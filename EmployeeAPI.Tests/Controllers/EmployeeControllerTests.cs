using EmployeeAPI.Controllers;
using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class EmployeeControllerTests
{
    private readonly Mock<IEmployeeService> _mockEmployeeService;
    private readonly EmployeeController _employeeController;

    public EmployeeControllerTests()
    {
        _mockEmployeeService = new Mock<IEmployeeService>();
        _employeeController = new EmployeeController(_mockEmployeeService.Object);
    }
    
    [Fact]
    public async Task GetEmployees_ReturnsOkResult_WithPagedEmployees()
    {
        // Arrange
        var pagedResponse = new PagedResponse<Employee>(
            new List<Employee>
            {
            new Employee { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" }
            },
            1, 1, 10);

        _mockEmployeeService.Setup(service => service.GetEmployeesPagedAsync(1, 10))
                            .ReturnsAsync(pagedResponse);

        // Act
        var result = await _employeeController.GetEmployees(1, 10);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<PagedResponse<Employee>>(okResult.Value);
        Assert.Single(returnValue.Data);
    }

    [Fact]
    public async Task GetEmployee_ReturnsOkResult_WithEmployee()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employee = new Employee { Id = employeeId, FirstName = "John", LastName = "Doe" };

        _mockEmployeeService.Setup(service => service.GetEmployeeByIdAsync(employeeId))
                            .ReturnsAsync(employee);

        // Act
        var result = await _employeeController.GetEmployee(employeeId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Employee>(okResult.Value);
        Assert.Equal(employeeId, returnValue.Id);
    }

    [Fact]
    public async Task GetEmployee_ReturnsNotFound_WhenEmployeeNotFound()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        _mockEmployeeService.Setup(service => service.GetEmployeeByIdAsync(employeeId))
                            .ReturnsAsync((Employee)null);

        // Act
        var result = await _employeeController.GetEmployee(employeeId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostEmployee_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var employee = new Employee { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };

        _mockEmployeeService.Setup(service => service.AddEmployeeAsync(It.IsAny<Employee>()))
                            .Returns(Task.CompletedTask);

        // Act
        var result = await _employeeController.PostEmployee(employee);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(employee.Id, ((Employee)createdAtActionResult.Value).Id);
    }

    [Fact]
    public async Task PutEmployee_ReturnsNoContent()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employee = new Employee { Id = employeeId, FirstName = "John", LastName = "Doe" };

        _mockEmployeeService.Setup(service => service.UpdateEmployeeAsync(employee))
                            .Returns(Task.CompletedTask);

        // Act
        var result = await _employeeController.PutEmployee(employeeId, employee);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PutEmployee_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employee = new Employee { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };

        // Act
        var result = await _employeeController.PutEmployee(employeeId, employee);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteEmployee_ReturnsNoContent()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employee = new Employee { Id = employeeId };

        _mockEmployeeService.Setup(service => service.GetEmployeeByIdAsync(employeeId))
                            .ReturnsAsync(employee);
        _mockEmployeeService.Setup(service => service.DeleteEmployeeAsync(employeeId))
                            .Returns(Task.CompletedTask);

        // Act
        var result = await _employeeController.DeleteEmployee(employeeId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteEmployee_ReturnsNotFound_WhenEmployeeNotFound()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        _mockEmployeeService.Setup(service => service.GetEmployeeByIdAsync(employeeId))
                            .ReturnsAsync((Employee)null);

        // Act
        var result = await _employeeController.DeleteEmployee(employeeId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task SearchEmployees_ReturnsOkResult_WithPagedEmployees()
    {
        // Arrange
        var pagedResponse = new PagedResponse<Employee>(
            new List<Employee>
            {
            new Employee { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" }
            },
            1, 1, 10);

        _mockEmployeeService.Setup(service => service.SearchEmployeesAsync(1, 10, "John"))
                            .ReturnsAsync(pagedResponse);

        // Act
        var result = await _employeeController.SearchEmployees(1, 10, "John");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<PagedResponse<Employee>>(okResult.Value);
        Assert.Single(returnValue.Data);
    }

}
