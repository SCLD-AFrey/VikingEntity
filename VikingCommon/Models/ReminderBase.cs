using System.Text.Json;
using System.Text.Json.Serialization;

namespace VikingCommon.Models;

public class ReminderBase : List<Reminder>
{

    public ReminderBase()
    {
        Load();
    }    
    public void Load()
    {
        List<Reminder>? reminders = new List<Reminder>();
        if (File.Exists(AppFiles._appFileReminders))
        {
            var jsonString = File.ReadAllText(AppFiles._appFileReminders);
            reminders =  JsonSerializer.Deserialize<List<Reminder>>(jsonString);
        }
        AddRange(reminders!);
    }

    public void Commit()
    {
        JsonSerializerOptions options = 
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText(AppFiles._appFileReminders, jsonString);
    }
    
    public int GetNextOid()
    {
        try{
            ReminderBase reminders = new ReminderBase();
            reminders.Load();
            return reminders.Max(p_u => p_u.Oid) + 1;
        } catch (Exception) {
            return 1;
        }
    }
    
    public List<Reminder>? Get()
    {
        return this;
    }
    
    public Reminder? Get(int p_input)
    {
        return this.FirstOrDefault(u => u.Oid == p_input);
    }
    
    public Reminder? Get(string p_input)
    {
        return this.FirstOrDefault(u => u.Title == p_input);
    }
}