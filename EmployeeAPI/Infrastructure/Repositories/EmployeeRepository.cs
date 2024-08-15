using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Interfaces;
using EmployeeAPI.Infrastructure.Data;

namespace EmployeeAPI.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeContext context) : base(context)
        {
        }
    }
}