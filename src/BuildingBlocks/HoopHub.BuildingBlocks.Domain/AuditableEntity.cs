namespace HoopHub.BuildingBlocks.Domain
{
    public class AuditableEntity : Entity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
