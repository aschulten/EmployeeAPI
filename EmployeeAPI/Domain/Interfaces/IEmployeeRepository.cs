using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Infrastructure.Repositories;

namespace EmployeeAPI.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesPagedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Employee>> SearchEmployeesAsync(int pageNumber, int pageSize, string searchTerm);
    }
}