namespace EmployeesTransportManagement.Models
{
    public class Settlement
    {
        public Guid SettlementId { get; set; } = Guid.NewGuid();
        public float Amount { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool IsApproved { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } 
        public Guid ProjectId { get; set; } 
        public Project Project { get; set; } 
        public bool IsRejected { get; set; } //soft delete
    }
}
