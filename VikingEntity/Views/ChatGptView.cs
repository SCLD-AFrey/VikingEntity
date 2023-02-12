using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using VikingCommon;

namespace VikingEntity.Views;

public static class ChatGptView
{
    public static async Task<Enums.ViewMode> Display()
    {
        
        
        ConsoleKey input;
        while (true)
        {
            Console.WriteLine("Chat Gpt Menu");
            MenuItem.Display('A', "Ask a question");
            MenuItem.Display('X', "<= Back");
            
            input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.A:
                    await AskQuestion();
                    break;
                case ConsoleKey.X:
                    return Enums.ViewMode.Main;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
        
    }

    private static async Task AskQuestion()
    {
        string prompt = "";
        var openAiService = new OpenAIService(new OpenAiOptions()
        {
            ApiKey =  Program.Settings.OpenAiApiKey
        });
        while (prompt.ToLower() != "exit") {
        
            prompt = SafeInput.String("Q: ")!;
        
            var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = prompt,
                //Model = Models.TextDavinciV3
            });

            if (completionResult.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("A: " + completionResult.Choices.FirstOrDefault()!.Text);
                Console.ResetColor();
            }
            else
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
        }
        
        
        
        
    }
}