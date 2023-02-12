using System;

namespace VikingCommon.Models;

public class Reminder : IReminder
{
    public int Oid { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? RemindDateTime { get; set; }
    public bool IsDone { get; set; }
    
    public void Save()
    {
        ReminderBase reminders = new ReminderBase();
        RemindDateTime = DateTime.Parse(RemindDateTime.ToString() ?? string.Empty).ToUniversalTime();

        if (Oid == 0)
        {
            Oid = reminders.GetNextOid();
            reminders.Add(this);
            reminders.Commit();
        }
        else
        {
            var reminder = reminders.Get(Oid);
            if (reminder != null)
            {
                reminder.Title = Title;
                reminder.Description = Description;
                reminder.RemindDateTime = RemindDateTime;
                reminder.IsDone = IsDone;
                reminders.Commit();
            }
        }
    }

}