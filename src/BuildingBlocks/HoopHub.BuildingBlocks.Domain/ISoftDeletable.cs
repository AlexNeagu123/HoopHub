namespace HoopHub.BuildingBlocks.Domain
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get;  set; }
        DateTime? DeletedOnUtc { get; set; }
    }
}
