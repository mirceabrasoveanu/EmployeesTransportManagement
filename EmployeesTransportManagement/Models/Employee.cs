namespace EmployeesTransportManagement.Models
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Settlement> Settlements { get; set; }
        public Guid UserId { get; set; } // Foreign Key
        public User User { get; set; }
    }

}
