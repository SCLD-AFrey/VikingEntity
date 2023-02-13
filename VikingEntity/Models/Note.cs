namespace VikingEntity.Models;

public class Note
{
    public int Oid { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateModified { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public int UserOid { get; set; } = 0;
}