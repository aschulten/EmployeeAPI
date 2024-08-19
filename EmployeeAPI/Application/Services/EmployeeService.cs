using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Interfaces;

namespace EmployeeAPI.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.OrderBy(x => x.LastName)
                            .ThenBy(x => x.LastName);
        }
        public async Task<PagedResponse<Employee>> GetEmployeesPagedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _employeeRepository.GetAllAsync();
            var employees = await _employeeRepository.GetEmployeesPagedAsync(pageNumber, pageSize);
            employees = employees.OrderBy(x => x.LastName)
                                .ThenBy(x => x.FirstName);
            var response = new PagedResponse<Employee>(employees, totalCount.Count(), pageNumber, pageSize);
            return response;    
         }
        public async Task<PagedResponse<Employee>> SearchEmployeesAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var employees = await _employeeRepository.SearchEmployeesAsync(pageNumber, pageSize, searchTerm);
            var totalCount = employees.Count();
            var response = new PagedResponse<Employee>(employees, totalCount, pageNumber, pageSize);
            return response;
        }
        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);
            await _employeeRepository.SaveChangesAsync();
        }
    }
}