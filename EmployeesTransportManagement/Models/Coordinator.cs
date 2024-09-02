namespace EmployeesTransportManagement.Models
{
    public class Coordinator
    {
        public Guid CoordinatorId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Project> Projects { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
