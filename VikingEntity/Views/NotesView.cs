using VikingCommon;
using VikingEntity.Models;

namespace VikingEntity.Views;

public static class NotesView
{
    private static readonly NotesBase NotesBase = new NotesBase();
    public static Enums.ViewMode Display()
    {
        // ReSharper disable once TooWideLocalVariableScope
        ConsoleKey input;
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
        foreach (var note in NotesBase.Where(p_x => p_x.IsDeleted == false))
        {
            i++;
            Console.WriteLine($"{i + ".", -3} {note.Title}");
        }
    }

    private static void AddNote()
    {
        Note note = new Note
        {
            Oid = NotesBase.GetNextOid(),
            Title = SafeInput.String("Title")!,
            Content = SafeInput.String("Content")!,
            UserOid = Program.CurrentUser.Oid
        };
        NotesBase.Add(note);
        NotesBase.Commit();
    }
}