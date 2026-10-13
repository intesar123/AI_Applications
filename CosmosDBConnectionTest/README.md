# Cosmos DB Connection Test

A small Python script ([ConsmosDBConnect.py](./ConsmosDBConnect.py)) that connects to the Azure Cosmos DB Emulator, creates a database and container, and inserts a test item.

## What it does

1. Connects to the Cosmos DB endpoint (SSL verification disabled for the emulator's self-signed certificate).
2. Creates the database `LocalDatabase` if it doesn't exist.
3. Creates the container `UsersContainer` (partition key `/id`) if it doesn't exist.
4. Inserts a sample item (`user_001`).

## Prerequisites

- Python 3.8+
- Azure Cosmos DB Emulator running locally (default `https://localhost:8081/`)

## Install packages

```powershell
python -m venv .venv
.\.venv\Scripts\Activate.ps1
pip install azure-cosmos
pip install python-dotenv 
```

## Configuration

The script reads these environment variables (defaults target the local emulator with its well-known key):

| Variable          | Default                  | Description                  |
|-------------------|--------------------------|------------------------------|
| `COSMOS_ENDPOINT` | `https://localhost:8081/` | Cosmos DB endpoint URL       |
| `COSMOS_KEY`      | Emulator default key     | Primary key for the account  |

Example for a remote emulator or a real account:

```powershell
$env:COSMOS_ENDPOINT = "https://<host>:8081/"
$env:COSMOS_KEY = "<your-key>"
```

> The default emulator key only works with `localhost`. A remote emulator must be started with `/AllowNetworkAccess /Key=<your key>`.

## Run

```powershell
python ConsmosDBConnect.py
```

## Troubleshooting

- **Unauthorized / wrong key**: use `localhost` with the default key, or supply the matching `COSMOS_KEY`.
- **Partition key paths cannot be empty**: use `PartitionKey(path="/id")` (already done in the script).
- **Item already exists (409)**: re-running inserts the same `id` again; change the id or delete the item.
- **Don't commit real keys**: use environment variables for non-emulator accounts.
