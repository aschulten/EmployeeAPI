using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Interfaces;
using System;

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
            try
            {
                var employees = await _employeeRepository.GetAllAsync();
                return employees.OrderBy(x => x.LastName)
                                .ThenBy(x => x.FirstName); // Corregido: Ordenar por LastName y FirstName
            }
            catch (Exception ex)
            {
                // Manejo de excepción (por ejemplo, loggear el error)
                throw new Exception("Error while getting all employees", ex);
            }
        }

        public async Task<PagedResponse<Employee>> GetEmployeesPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalCount = await _employeeRepository.GetAllAsync();
                var employees = await _employeeRepository.GetEmployeesPagedAsync(pageNumber, pageSize);
                employees = employees.OrderBy(x => x.LastName)
                                     .ThenBy(x => x.FirstName);

                return new PagedResponse<Employee>(employees, totalCount.Count(), pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                // Manejo de excepción
                throw new Exception("Error while getting paginated list of employees", ex);
            }
        }

        public async Task<PagedResponse<Employee>> SearchEmployeesAsync(int pageNumber, int pageSize, string searchTerm)
        {
            try
            {
                var employees = await _employeeRepository.SearchEmployeesAsync(pageNumber, pageSize, searchTerm);
                var totalCount = employees.Count();

                return new PagedResponse<Employee>(employees, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                // Manejo de excepción
                throw new Exception("Error while searching employees", ex);
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    throw new KeyNotFoundException($"Epmployee with ID {id} not found");
                }
                return employee;
            }
            catch (Exception ex)
            {
                // Manejo de excepción
                throw new Exception($"Error while getting employee with ID {id}", ex);
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await _employeeRepository.AddAsync(employee);
                await _employeeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepción
                throw new Exception("Error while adding employee", ex);
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                await _employeeRepository.UpdateAsync(employee);
                await _employeeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepción
                throw new Exception("Error while updating employee", ex);
            }
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    throw new KeyNotFoundException($"Employee with ID {id} not found");
                }

                await _employeeRepository.DeleteAsync(id);
                await _employeeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepción
                throw new Exception($"Error while deleting employee with ID {id}", ex);
            }
        }
    }
}
