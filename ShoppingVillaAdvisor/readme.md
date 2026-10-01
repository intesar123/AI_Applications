# Project Setup Commands

Run these commands from the `ShoppingVillaAdvisor` project directory:

```powershell
python -m venv .venv
pip install "fastapi[standard]"
pip install uvicorn
pip install sqlalchemy
pip install psycopg2-binary
pip install pydantic-settings
pip install python-jose
pip install passlib[bcrypt]
pip install alembic
pip install pytest
pip install httpx
pip freeze > requirements.txt
```

| Package or command | Description |
|---|---|
| `python -m venv .venv` | Creates a project-local virtual environment for isolated Python dependencies. |
| `fastapi[standard]` | Installs FastAPI and its standard optional dependencies for serving and developing the API. |
| `uvicorn` | Runs the FastAPI application as an ASGI server. |
| `sqlalchemy` | Provides SQL tools and object-relational mapping for database access. |
| `psycopg2-binary` | PostgreSQL database adapter used by Python applications. |
| `pydantic-settings` | Loads and validates application configuration from environment variables and files. |
| `python-jose` | Creates and verifies JSON Web Tokens and related JOSE formats. |
| `passlib[bcrypt]` | Provides password hashing and verification with bcrypt support. |
| `alembic` | Creates and applies database schema migrations for SQLAlchemy. |
| `pytest` | Runs automated Python tests. |
| `httpx` | HTTP client for making synchronous or asynchronous requests, including API tests. |
| `pip freeze > requirements.txt` | Writes installed package versions to `requirements.txt` for environment reproduction. |

## Run the Application

From the `ShoppingVillaAdvisor` project directory, start the development server with:

```powershell
uvicorn app.main:app --reload
```

## Docker Compose Configuration

The root `.env` file supplies the Compose variables below. Keep its credentials local and do not commit it.

| Variable | Purpose |
|---|---|
| `POSTGRES_USER` | PostgreSQL user created for local development. |
| `POSTGRES_PASSWORD` | Password for the local PostgreSQL user. |
| `POSTGRES_DB` | PostgreSQL database created for the application. |
| `DATABASE_URL` | SQLAlchemy connection URL; inside Compose, the database host is `postgres`. |

Validate the Compose configuration from the repository root with:

```powershell
docker compose config --quiet
```

## Database Migrations

Start PostgreSQL before generating or applying migrations:

```powershell
docker compose up -d postgres
```

Run the following Alembic commands from the project root. Initialize Alembic only once; this repository already has an `alembic/` directory and `alembic.ini`, so skip the initialization command here.

| Command | Description |
|---|---|
| `alembic init alembic` | Creates Alembic's configuration and migration directory for a new project. Run once only. |
| `alembic revision --autogenerate -m "create initial tables"` | Compares SQLAlchemy model metadata with the database and generates a migration script. Review the generated script before applying it. |
| `alembic upgrade head` | Applies all pending migrations up to the latest revision. |

The database must be reachable from the environment running Alembic. The Compose hostname `postgres` resolves inside Docker; for host-side commands, configure `DATABASE_URL` with a host-reachable name such as `localhost`.

## Current Progress

- `app/main.py` creates the FastAPI application and provides a root health endpoint.
- The user API obtains a SQLAlchemy session through FastAPI dependencies and uses it to construct the repository and service.
- The user repository and service support creating users and retrieving them by ID, email, or username.
- Update and delete routes are declared, but their service and repository operations are not implemented yet.
- Docker Compose defines the application and PostgreSQL services, and its configuration validates with the root `.env` values.
- The container build still needs path alignment: the Dockerfile expects `requirements.txt` and `app/` at the repository root, while the FastAPI project files are under `ShoppingVillaAdvisor/`.

# ShoppingVillaAdvisor Packages

The package versions below reflect the current `ShoppingVillaAdvisor/requirements.txt` snapshot generated with `pip freeze`. The list includes direct dependencies as well as transitive packages and supporting tools; `requirements.txt` is the source of truth for exact versions.

| Package | Version | Description |
|---|---:|---|
| `agent-detector` | 2.0.0 | Detects agent-related runtime context for supporting tools. |
| `alembic` | 1.20.0 | Manages SQLAlchemy database schema migrations. |
| `annotated-doc` | 0.0.5 | Supplies documentation metadata helpers used by framework tooling. |
| `annotated-types` | 0.8.0 | Provides metadata types used with Python typing annotations. |
| `anyio` | 4.15.1 | Provides asynchronous concurrency primitives across supported async backends. |
| `bcrypt` | 5.0.0 | Implements the bcrypt password-hashing algorithm. |
| `certifi` | 2026.7.22 | Provides a curated CA certificate bundle for TLS verification. |
| `click` | 8.5.0 | Builds command-line interfaces. |
| `colorama` | 0.4.6 | Enables portable colored terminal output on Windows. |
| `detect-installer` | 0.2.1 | Detects which Python package installer installed a distribution. |
| `dnspython` | 2.8.0 | Provides DNS lookups and DNS protocol support. |
| `ecdsa` | 0.19.2 | Implements elliptic-curve digital signature operations. |
| `email-validator` | 2.3.0 | Validates email addresses, including domain syntax. |
| `fastapi` | 0.142.2 | Web framework for defining APIs with Python type hints. |
| `fastapi-cli` | 0.0.32 | Provides the `fastapi` command-line development tools. |
| `fastapi-cloud-cli` | 0.26.0 | Provides command-line tooling for FastAPI Cloud workflows. |
| `fastar` | 0.12.0 | Accelerates file discovery for FastAPI tooling. |
| `googleapis-common-protos` | 1.75.5 | Shared Protocol Buffer definitions for Google APIs. |
| `h11` | 0.16.0 | Implements the HTTP/1.1 protocol used by Python web servers and clients. |
| `httpcore` | 1.0.9 | Low-level synchronous and asynchronous HTTP transport. |
| `httptools` | 0.8.0 | Provides fast HTTP parsing support for ASGI servers. |
| `httpx` | 0.28.1 | HTTP client supporting synchronous and asynchronous requests. |
| `idna` | 3.20 | Encodes and decodes internationalized domain names. |
| `iniconfig` | 2.3.0 | Parses INI-style configuration, including pytest configuration. |
| `Jinja2` | 3.1.6 | Template engine used by command-line and documentation tooling. |
| `Mako` | 1.4.3 | Template engine used by Alembic migration generation. |
| `markdown-it-py` | 4.2.0 | Parses Markdown into tokens and rendered output. |
| `MarkupSafe` | 3.0.3 | Escapes markup safely, including for Jinja2 templates. |
| `mdurl` | 0.1.2 | Parses and formats URLs for Markdown processing. |
| `opentelemetry-api` | 1.45.0 | Defines the OpenTelemetry tracing and metrics APIs. |
| `opentelemetry-exporter-http-transport` | 0.66b0 | Supplies HTTP transport used by OpenTelemetry exporters. |
| `opentelemetry-exporter-otlp-common` | 0.66b0 | Shared components for OpenTelemetry Protocol exporters. |
| `opentelemetry-exporter-otlp-proto-common` | 1.45.0 | Common Protocol Buffer support for OTLP exporters. |
| `opentelemetry-exporter-otlp-proto-http` | 1.45.0 | Exports OpenTelemetry data over OTLP using HTTP and Protocol Buffers. |
| `opentelemetry-proto` | 1.45.0 | OpenTelemetry Protocol Buffer message definitions. |
| `opentelemetry-sdk` | 1.45.0 | SDK for recording and exporting OpenTelemetry telemetry. |
| `opentelemetry-semantic-conventions` | 0.66b0 | Standard attribute names for OpenTelemetry instrumentation. |
| `packaging` | 26.3 | Parses and compares Python package versions and requirement specifiers. |
| `passlib` | 1.7.4 | Password hashing and verification utilities. |
| `pluggy` | 1.6.0 | Plugin and hook system used by pytest and other tools. |
| `protobuf` | 7.36.2 | Google Protocol Buffers serialization runtime. |
| `psycopg2-binary` | 2.9.13 | PostgreSQL adapter for Python, distributed with binary dependencies. |
| `pyasn1` | 0.6.4 | ASN.1 data types and encoding/decoding support. |
| `pydantic` | 2.13.5 | Validates and serializes data using Python type annotations. |
| `pydantic-extra-types` | 2.11.1 | Additional specialized data types for Pydantic models. |
| `pydantic-settings` | 2.15.0 | Loads and validates application settings from environment variables and files. |
| `pydantic_core` | 2.46.5 | Core validation and serialization engine used by Pydantic. |
| `Pygments` | 2.21.0 | Syntax highlighting for terminal and documentation output. |
| `pytest` | 9.1.1 | Test runner and assertion framework. |
| `python-dotenv` | 1.2.3 | Loads environment variables from `.env` files. |
| `python-jose` | 3.5.0 | Creates and verifies JSON Web Tokens and related JOSE formats. |
| `python-multipart` | 0.0.32 | Parses multipart form data and file uploads. |
| `PyYAML` | 6.0.3 | Reads and writes YAML data. |
| `rich` | 15.0.0 | Produces formatted and colored terminal output. |
| `rich-toolkit` | 0.20.5 | Shared terminal UI components built on Rich. |
| `rignore` | 0.8.1 | Matches file paths against ignore rules, such as `.gitignore` patterns. |
| `rsa` | 4.9.1 | Implements RSA public-key cryptography. |
| `sentry-sdk` | 2.71.0 | Captures and reports application errors and performance data to Sentry. |
| `shellingham` | 1.5.4 | Detects the active command-line shell. |
| `six` | 1.17.0 | Compatibility utilities for Python code. |
| `SQLAlchemy` | 2.1.1 | SQL toolkit and object-relational mapper for database access. |
| `starlette` | 1.7.0 | ASGI toolkit underlying FastAPI, including routing and middleware. |
| `typer` | 0.27.2 | Builds typed command-line interfaces using Python annotations. |
| `typing-inspection` | 0.4.4 | Runtime inspection utilities for Python typing annotations. |
| `typing_extensions` | 4.16.0 | Backports newer typing features to supported Python versions. |
| `urllib3` | 2.8.0 | HTTP client library used by Python networking dependencies. |
| `uvicorn` | 0.54.0 | ASGI server for running the FastAPI application. |
| `watchfiles` | 1.3.0 | Monitors files for changes, supporting development reloads. |
| `websockets` | 17.1 | Implements the WebSocket protocol for asynchronous applications. |
