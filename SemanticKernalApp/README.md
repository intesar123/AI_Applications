# Semantic Kernel App - Lights Control Chat Interface

## Project Description

This is a .NET 10 console application that demonstrates the capabilities of **Microsoft Semantic Kernel** with **Groq AI API** integration. The application provides an interactive chat interface where users can control smart lights through natural language commands. The AI assistant can understand requests and execute plugin functions to manage light states.

### Key Features

- **Interactive Chat Interface**: Real-time conversation with an AI powered by Groq
- **Smart Lights Plugin**: Manage lights with functions like:
  - Get lights status (`get_lights`)
  - Change light state (`change_state`)
- **Semantic Kernel Integration**: Leverages Microsoft Semantic Kernel for AI orchestration
- **Function Calling**: AI automatically detects and calls appropriate plugin functions
- **Environment-based Configuration**: Secure API key and endpoint management
- **Structured Logging**: Enterprise-grade logging for debugging and monitoring

## Project Structure

```
SemanticKernalApp/
├── Program.cs                 # Main application entry point
├── LightsPlugin.cs            # Smart lights plugin with kernel functions
├── LightsModel.cs             # Data model for light objects
├── Properties/
│   ├── launchSettings.json    # Local configuration (git-ignored)
│   └── launchSettings.sample.json  # Sample configuration template
└── README.md                  # This file
```

## Packages & Dependencies

This project uses the following NuGet packages:

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.SemanticKernel` | 1.82.0 | Core Semantic Kernel framework for AI orchestration |
| `Microsoft.Extensions.Logging` | 10.0.14 | Logging infrastructure |
| `Microsoft.Extensions.Logging.Console` | 10.0.14 | Console logging provider |
| `Microsoft.Extensions.DependencyInjection` | *Transitive* | Dependency injection container |
| `System.ComponentModel.DataAnnotations` | *Built-in* | Attribute support for kernel functions |

### Target Framework

- **.NET 10.0** - Latest .NET runtime

## Getting Started

### Prerequisites

1. **.NET 10 SDK** installed
2. **Groq API Key** - Get one from [console.groq.com](https://console.groq.com)
3. **Groq Account** - Free tier available

### Setup & Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/intesar123/AI_Applications.git
   cd AI_Applications/SemanticKernalApp
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Configure environment variables**:
   Copy the sample launch settings and update with your credentials:
   ```bash
   cp Properties/launchSettings.sample.json Properties/launchSettings.json
   ```

4. **Update launchSettings.json** with your Groq credentials:
   ```json
   {
     "profiles": {
       "Development": {
         "commandName": "Project",
         "environmentVariables": {
           "ENDPOINT": "https://api.groq.com/openai/v1",
           "API_KEY": "your_groq_api_key_here",
           "MODEL_ID": "gpt-4o-mini"
         }
       }
     }
   }
   ```

5. **Build the application**:
   ```bash
   dotnet build
   ```

6. **Run the application**:
   ```bash
   dotnet run
   ```

## Usage

Once the application is running, you'll see a chat prompt:

```
User >
```

### Example Interactions

**Get light status**:
```
User > What lights do we have?
AI > [Retrieves lights from the plugin]
```

**Control lights**:
```
User > Turn on the table lamp
AI > [Executes change_state function for Table Lamp]
```

**Natural language queries**:
```
User > Which lights are currently on?
AI > [Processes query and provides response]
```

Type `exit` or leave input blank to quit the application.

## Launch Settings Configuration

### Development Profile

The `launchSettings.json` file contains the following profile configuration:

```json
{
  "profiles": {
    "Development": {
      "commandName": "Project",
      "environmentVariables": {
        "ENDPOINT": "https://api.groq.com/openai/v1",
        "API_KEY": "gsk_***************************************",
        "MODEL_ID": "openai/gpt-oss-20b"
      }
    }
  }
}
```

**Environment Variables**:
- `ENDPOINT`: Groq API endpoint (default: `https://api.groq.com/openai/v1`)
- `API_KEY`: Your Groq API authentication key
- `MODEL_ID`: Model identifier (examples: `gpt-4o-mini`, `openai/gpt-oss-20b`)

### Sample Configuration Template

See `Properties/launchSettings.sample.json` for a template with placeholder values:

```json
{
  "profiles": {
    "Development": {
      "commandName": "Project",
      "environmentVariables": {
        "ENDPOINT": "https://api.groq.com/openai/v1",
        "API_KEY": "<<----Replace me------>>",
        "MODEL_ID": "openai/gpt-oss-20b"
      }
    }
  }
}
```

## Architecture

### Core Components

1. **Semantic Kernel Builder**
   - Initializes the AI kernel with OpenAI-compatible Groq service
   - Registers plugins for function calling

2. **LightsPlugin**
   - `GetLightsAsync()`: Retrieves list of available lights with status
   - `ChangeStateAsync(id, isOn)`: Toggles light state on/off

3. **Chat History Management**
   - Maintains conversation context
   - Enables multi-turn conversations

4. **Function Choice Behavior**
   - Set to `FunctionChoiceBehavior.Auto()` for automatic plugin invocation
   - AI decides when to call plugin functions based on user input

## Available Models

Popular models available on Groq:

- `mixtral-8x7b-32768` - High capacity open model
- `llama2-70b-4096` - Meta's LLaMA 2 large model
- `gpt-4o-mini` - Compact OpenAI model
- `openai/gpt-oss-20b` - 20B parameter model

Check [Groq Console Models](https://console.groq.com/keys) for the latest available models.

## Error Handling

### Common Issues

1. **"Unknown request URL" error**
   - Ensure you're using `AddOpenAIChatCompletion()` not `AddAzureOpenAIChatCompletion()`
   - Verify endpoint is set to Groq API URL

2. **"API_KEY environment variable is not set"**
   - Configure environment variables in `launchSettings.json`
   - Restart the application after configuration changes

3. **"LightsPlugin doesn't implement any [KernelFunction]-attributed methods"**
   - Ensure all plugin methods have `[KernelFunction]` attribute
   - Import `System.ComponentModel.DataAnnotations`

## Logging

The application uses console logging at `LogLevel.Trace` for detailed debugging:

```csharp
builder.Services.AddLogging(services => 
    services.AddConsole().SetMinimumLevel(LogLevel.Trace)
);
```

All Semantic Kernel operations are logged for troubleshooting.

## Security Considerations

⚠️ **Important**: 
- Never commit `launchSettings.json` with real API keys to version control
- The file is already in `.gitignore`
- Always use environment variables or secure secret management for sensitive data
- Use sample configuration template (`launchSettings.sample.json`) for templates

## Contributing

To extend this project:

1. **Add new plugins**: Create new plugin classes with `[KernelFunction]` methods
2. **Custom models**: Implement `LightsModel` variants for other IoT devices
3. **Enhanced logging**: Extend console logging configuration

## Learning Resources

- [Microsoft Semantic Kernel Documentation](https://learn.microsoft.com/en-us/semantic-kernel/overview/)
- [Groq API Documentation](https://console.groq.com/docs)
- [Semantic Kernel .NET Examples](https://github.com/microsoft/semantic-kernel/tree/main/dotnet/samples)

## License

This project is part of the AI_Applications repository.

## Support

For issues, questions, or contributions, please refer to the main repository:
[intesar123/AI_Applications](https://github.com/intesar123/AI_Applications)
