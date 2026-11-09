

using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel.DataAnnotations;


var modelId = Environment.GetEnvironmentVariable("MODEL_ID") ?? "openai/gpt-oss-20b"; // Groq model ID
//var endpoint = "https://api.groq.com/openai/v1";
 Uri groqEndpoint = new Uri(Environment.GetEnvironmentVariable("ENDPOINT") ?? "https://api.groq.com/openai/v1");
var apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? throw new InvalidOperationException("API_KEY environment variable is not set."); // Groq API key

// create the kernel builder and add OpenAI chat completion service (Groq is OpenAI-compatible)
var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion(modelId: modelId, apiKey: apiKey, endpoint: groqEndpoint);

// add enterprise logging
builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

// build the kernel
Kernel? kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();


// Add a plugin (the LightsPlugin class is defined below)
// what is plugin
// A plugin is a class that contains functions (marked with [KernelFunction]) that can be invoked by the AI through the kernel.
kernel.Plugins.AddFromType<LightsPlugin>("Lights");

// Enable planning 
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

//Create a history store the conversation 
var history = new ChatHistory();

string? userInput = null;

do
{
    // Collect user input
    Console.Write("User >");
    userInput = Console.ReadLine();

    // Add user input to chat history
    if (!string.IsNullOrWhiteSpace(userInput))
    {
        history.AddUserMessage(userInput);

        // Get the response from the AI
        var result = await chatCompletionService.GetChatMessageContentAsync(history, executionSettings: openAIPromptExecutionSettings, kernel: kernel);
        Console.WriteLine($"AI > {result}");

        // add the message from the AI to the chat history
        history.AddMessage(result.Role, result.Content?? string.Empty);
    }
}
while (!string.IsNullOrWhiteSpace(userInput));
