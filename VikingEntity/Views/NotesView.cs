using VikingCommon;
using VikingEntity.Models;

namespace VikingEntity.Views;

public static class NotesView
{
    private static NotesBase notesBase = new NotesBase();
    public static Enums.ViewMode Display()
    {
        ConsoleKey input = ConsoleKey.NoName;
        while (true)
        {
            Console.WriteLine("Admin Menu");
            MenuItem.Display('A', "Add Notes");
            MenuItem.Display('V', "Read Notes");
            MenuItem.Display('X', "<= Back");
            
            input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.A:
                    AddNote();
                    break;
                case ConsoleKey.V:
                    ViewAll();
                    break;
                case ConsoleKey.X:
                    return Enums.ViewMode.Main;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private static void ViewAll()
    {
        int i = 0;
        foreach (var note in notesBase.Where(p_x => p_x.IsDeleted == false))
        {
            i++;
            Console.WriteLine($"{i + ".", -3} {note.Title}");
        }
    }

    private static void AddNote()
    {
        Note note = new Note();
        note.Oid = notesBase.GetNextOid();
        note.Title = SafeInput.String("Title");
        note.Content = SafeInput.String("Content");
        note.UserOid = Program.Currentuser.Oid;
        notesBase.Add(note);
        notesBase.Commit();
    }
}