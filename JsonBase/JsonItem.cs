namespace JsonBase;

public abstract class JsonItem
{
    public int Oid { get; init; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateModified { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}