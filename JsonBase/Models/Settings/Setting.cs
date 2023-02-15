namespace JsonBase.Models.Settings;
public class Setting : JsonItem
{ 
    public string? Name { get; init; }
    public string? Value { get; set; }
    public string? Type { get; init; } = typeof(string).ToString();
    public override string ToString()
    {
        return $"{Oid, -5} {Name, -25} {Value, -25} {Type}";
    }
}