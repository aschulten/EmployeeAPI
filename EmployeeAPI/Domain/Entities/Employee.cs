namespace EmployeeAPI.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfJoining { get; set; }
        public decimal LastSalary { get; set; }
        public int EmployeeId { get; set; }
    }
}