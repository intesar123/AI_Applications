# Groq API Connection Test

A small Python script that sends a prompt to Groq's OpenAI-compatible API and prints the response. It uses the OpenAI Python client configured with a Groq base URL.

## Requirements

- Python
- A Groq API key

## Setup

From this project directory, install the dependencies:

```powershell
python -m pip install openai python-dotenv
```

Create a `.env` file by copying `.env.sample`, then set your credentials and Groq API base URL:

```dotenv
GROQ_API_KEY=your-groq-api-key
GROQ_BASE_URL=your-groq-openai-compatible-base-url
```

The script also accepts `OPENAI_API_KEY` as a fallback for `GROQ_API_KEY`. Keep API keys private and do not commit `.env`.

## Run

```powershell
python groqAITest.py
```

The script sends the question "Who is first prime minister of India?" using the `openai/gpt-oss-20b` model and prints the generated response. Update the prompt or model in `groqAITest.py` to try a different request.