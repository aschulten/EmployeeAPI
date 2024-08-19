using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Interfaces;
using EmployeeAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Employees
                .AsQueryable()
                .OrderBy (e => e.LastName)
                .ThenBy (e => e.FirstName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<Employee>> SearchEmployeesAsync(int pageNumber, int pageSize, string searchTerm)
        {
             var query = _context.Employees.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(e => e.FirstName.Contains(searchTerm) || e.LastName.Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();
            query = query.OrderBy(e => e.LastName)
                    .ThenBy(e => e.FirstName);
        var employees = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return employees;
    
        }
    }
}