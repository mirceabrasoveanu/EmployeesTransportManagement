namespace EmployeesTransportManagement.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid(); 
        public string Email { get; set; }
        public string Role { get; set; } 
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Coordinator> Coordinators { get; set; }
    }
}
