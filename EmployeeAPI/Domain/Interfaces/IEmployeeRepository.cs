using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Infrastructure.Repositories;

namespace EmployeeAPI.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}