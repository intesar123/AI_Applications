# SQL Server Connection Test

This folder contains a small Python script that attempts to connect to a SQL
Server database and reports whether the connection succeeds.

## Requirements

- Python 3
- Microsoft ODBC Driver 17 for SQL Server
- The `pyodbc` Python package
- Network access to the SQL Server, with valid database credentials

Install `pyodbc` if it is not already available in your Python environment:

```powershell
pip install pyodbc
```

## Configure

Open [`SQLServerConnect.py`](./SQLServerConnect.py) and set the server,
database, and SQL Server login details for your environment. The connection
string must use the installed ODBC driver name and include the appropriate
authentication fields.

Do not commit real passwords or other credentials to source control. For
ongoing use, load credentials from environment variables or another secure
configuration source instead of storing them in the script.

## Run

From this folder, run:

```powershell
py .\SQLServerConnect.py
```

On success, the script prints `Connection successful!`. If the connection
fails, it prints the `pyodbc` error. The connection is closed after the
attempt.
