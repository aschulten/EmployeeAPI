using EmployeeAPI.Infrastructure.Data;
using EmployeeAPI.Infrastructure.Repositories;
using EmployeeAPI.Tests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Tests.Repositories
{
    public class EmployeeRepositoryTests : IClassFixture<DbContextFixture>
    {
        private EmployeeContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<EmployeeContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new EmployeeContext(options);
        }

        [Fact]
        public async Task GetEmployeesPagedAsync_ReturnsPagedData_WithCorrectOrder()
        {
            // Arrange
            using var context = CreateContext();
            var employees = TestDataHelper.GetSampleEmployees()
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToList();
            context.Employees.AddRange(employees);
            context.SaveChanges();

            var repository = new EmployeeRepository(context);

            // Act
            var result = await repository.GetEmployeesPagedAsync(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(employees.First().LastName, result.First().LastName);
            Assert.Equal(employees.First().FirstName, result.First().FirstName);
            Assert.Equal(employees[1].LastName, result.Last().LastName);
            Assert.Equal(employees[1].FirstName, result.Last().FirstName);
        }

        [Fact]
        public async Task SearchEmployeesAsync_ReturnsEmptyList_ForEmptySearchTerm()
        {
            // Arrange
            using var context = CreateContext();
            var employees = TestDataHelper.GetSampleEmployees()
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToList();
            context.Employees.AddRange(employees);
            context.SaveChanges();

            var repository = new EmployeeRepository(context);

            // Act
            var result = await repository.SearchEmployeesAsync(1, 10, "NonExistent");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        [Fact]
        public async Task SearchEmployeesAsync_ReturnsFilteredData_WithCorrectOrder()
        {
            // Arrange
            using var context = CreateContext();
            var employees = TestDataHelper.GetSampleEmployees()
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToList();
            context.Employees.AddRange(employees);
            context.SaveChanges();
            var stringToSearch = employees.First().LastName;
            

            var repository = new EmployeeRepository(context);

            // Act
            var result = await repository.SearchEmployeesAsync(1, 10, stringToSearch);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(employees[0].LastName, result.First().LastName);
            Assert.Equal(employees[0].FirstName, result.First().FirstName);
            Assert.Equal(employees[1].LastName, result.Last().LastName);
            Assert.Equal(employees[1].FirstName, result.Last().FirstName);
        }

        [Fact]
        public async Task SearchEmployeesAsync_ReturnsPagedData_ForMultiplePages()
        {
            // Arrange
            using var context = CreateContext();
            var employees = TestDataHelper.GetSampleEmployees()
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToList();
            context.Employees.AddRange(employees);
            context.SaveChanges();


            var repository = new EmployeeRepository(context);

            // Act
            var page1 = await repository.SearchEmployeesAsync(1, 1, "Doe");
            var page2 = await repository.SearchEmployeesAsync(2, 1, "Doe");

            // Assert
            Assert.Single(page1);
            Assert.Single(page2);
        }
    }
}
