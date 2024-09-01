namespace EmployeesTransportManagement.Models
{
    public class Project
    {
        public Guid ProjectId { get; set; } = Guid.NewGuid();
        public string ProjectName { get; set; }
        public Guid CoordinatorId { get; set; }
        public Coordinator Coordinator { get; set; } 
        public ICollection<Settlement> Settlements { get; set; } 
    }
}
