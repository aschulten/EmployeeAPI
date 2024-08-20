using EmployeeAPI.Application.Services;
using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeAPI.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldReturnOrderedEmployees()
        {
            // Arrange
            var sampleEmployees = TestDataHelper.GetSampleEmployees();
            _employeeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(sampleEmployees);

            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            Assert.Equal(sampleEmployees.Count(), result.Count());
            Assert.Equal(sampleEmployees.First().LastName, result.First().LastName);
            Assert.Equal(sampleEmployees.Last().LastName, result.Last().LastName);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldReturnEmptyList_WhenNoEmployees()
        {
            // Arrange
            _employeeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Employee>());

            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            Assert.Empty(result);
        }
        [Fact]
        public async Task GetEmployeesPagedAsync_ShouldReturnPagedEmployees()
        {
            // Arrange
            var sampleEmployees = TestDataHelper.GetSampleEmployees();
            _employeeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(sampleEmployees);
            _employeeRepositoryMock.Setup(repo => repo.GetEmployeesPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
                                   .ReturnsAsync(sampleEmployees.Take(2));

            // Act
            var result = await _employeeService.GetEmployeesPagedAsync(1, 2);

            // Assert
            Assert.Equal(2, result.Data.Count());
            Assert.Equal(sampleEmployees.Count(), result.TotalCount);
        }

        [Fact]
        public async Task GetEmployeesPagedAsync_ShouldReturnEmptyList_WhenNoEmployees()
        {
            // Arrange
            _employeeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Employee>());
            _employeeRepositoryMock.Setup(repo => repo.GetEmployeesPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
                                   .ReturnsAsync(new List<Employee>());

            // Act
            var result = await _employeeService.GetEmployeesPagedAsync(1, 2);

            // Assert
            Assert.Empty(result.Data);
            Assert.Equal(0, result.TotalCount);
        }
        [Fact]
        public async Task SearchEmployeesAsync_ShouldReturnFilteredEmployees()
        {
            // Arrange
            var sampleEmployees = TestDataHelper.GetSampleEmployees();
            var lastNameToSearch = sampleEmployees.First().LastName;
            _employeeRepositoryMock.Setup(repo => repo.SearchEmployeesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                                   .ReturnsAsync(sampleEmployees.Where(e => e.LastName.Contains(lastNameToSearch)).ToList());

            // Act
            var result = await _employeeService.SearchEmployeesAsync(1, 2, lastNameToSearch);

            // Assert
            Assert.True(sampleEmployees.Where(x => x.LastName == lastNameToSearch).Count() <= result.Data.Count());
            Assert.Equal(sampleEmployees.Where(x => x.LastName == lastNameToSearch).Count(), result.TotalCount);
        }

        [Fact]
        public async Task SearchEmployeesAsync_ShouldReturnEmptyList_WhenNoMatches()
        {
            // Arrange
            _employeeRepositoryMock.Setup(repo => repo.SearchEmployeesAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                                   .ReturnsAsync(new List<Employee>());

            // Act
            var result = await _employeeService.SearchEmployeesAsync(1, 2, "NonExistent");

            // Assert
            Assert.Empty(result.Data);
            Assert.Equal(0, result.TotalCount);
        }

    }
}
