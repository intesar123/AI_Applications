
from openai import OpenAI
from dotenv import load_dotenv
import os

load_dotenv()

api_key = os.getenv("OPENAI_API_KEY")
base_url = os.getenv("OPENAI_BASE_URL")
if not api_key:
    raise RuntimeError("Set OPENAI_API_KEY before running this script.")

if not base_url:
    raise RuntimeError("Set OPENAI_BASE_URL before running this script.")

client = OpenAI(
    api_key=api_key,
    base_url=base_url,
)

response = client.responses.create(
    input="Who is first prime minister of India?",
    model="openai/gpt-oss-20b",
)
print(response.output_text)
