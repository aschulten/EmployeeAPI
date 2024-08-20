using EmployeeAPI.Domain.Entities;

public static class TestDataHelper
{
    public static IEnumerable<Employee> GetSampleEmployees()
    {
        return new List<Employee>
        {
            new Employee { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", LastSalary = 50000 },
            new Employee { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", LastSalary = 60000 },
            new Employee { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Smith", LastSalary = 55000 }
        };
    }
}
