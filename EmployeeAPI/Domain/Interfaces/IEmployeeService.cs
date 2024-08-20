using EmployeeAPI.Domain.Entities;

namespace EmployeeAPI.Domain.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<PagedResponse<Employee>> GetEmployeesPagedAsync(int pageNumber, int pageSize);
        Task<PagedResponse<Employee>> SearchEmployeesAsync(int pageNumber, int pageSize, string searchTerm);
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Guid id);
    }
}