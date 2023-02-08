using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System.Speech.Synthesis;
using System.Speech.Recognition;  

namespace GptConsole;

static class Program
{
    internal static bool IsListening = false;
    private static SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
    static readonly SpeechSynthesizer Synthesizer = new SpeechSynthesizer();

    private static OpenAIService _openAiService = new OpenAIService(new OpenAiOptions()
    {
        ApiKey = "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"
    });
    

    static async Task Main(string[] p_args)
    {
        string? prompt = "";
        while (prompt != null && !prompt.ToLower().Trim().Equals("exit"))
        {
            Console.WriteLine("Ask Anything: ");
            prompt = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                var response = await _openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
                {
                    Prompt = prompt,
                    Model = Models.TextDavinciV3
                });
        
                Synthesizer.SetOutputToDefaultAudioDevice();
                var textToSpeak = response.Choices.FirstOrDefault()?.Text;
                if (textToSpeak != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(textToSpeak);
                    Console.ResetColor();
                    Synthesizer.Speak(textToSpeak);
                }
            }
        }
    } 
}