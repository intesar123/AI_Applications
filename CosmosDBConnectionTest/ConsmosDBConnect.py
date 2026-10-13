import os
from dotenv import load_dotenv
from azure.cosmos import CosmosClient, PartitionKey, exceptions

# 1. Define local emulator credentials (universal fixed key)
# The default emulator key only works via localhost. For a remote IP, set COSMOS_ENDPOINT and
# COSMOS_KEY to the endpoint and key the emulator was started with (/AllowNetworkAccess /Key=...).
load_dotenv()
ENDPOINT = os.getenv("COSMOS_ENDPOINT")
print(f"COSMOS_ENDPOINT: {ENDPOINT}")
PRIMARY_KEY = os.getenv("COSMOS_KEY")
print(f"COSMOS_KEY: {PRIMARY_KEY}")

def main():
    try:
        # 2. Initialize the client
        # connection_verify=False is required for the local emulator's self-signed SSL cert
        client = CosmosClient(ENDPOINT, credential=PRIMARY_KEY, connection_verify=False)
        print("Successfully connected to the local Cosmos DB Emulator.")

        # 3. Create a Database
        database_name = "LocalDatabase"
        database = client.create_database_if_not_exists(id=database_name)
        print(f"Database '{database_name}' created or already exists.")

        # 4. Create a Container (Table)
        container_name = "UsersContainer"
        # /id or /userId is a standard partition key choice for a user table
        container = database.create_container_if_not_exists(
            id=container_name, 
            partition_key=PartitionKey(path="/id")
        )
        print(f"Container '{container_name}' created or already exists.")

        # 5. Insert an Item (Optional Test)
        test_item = {
            "id": "user_002",
            "name": "Intesar",
            "email": "intesar@example.com"
        }
        container.create_item(body=test_item)
        print("Successfully inserted a test item!")

    except exceptions.CosmosHttpResponseError as e:
        print(f"An Azure Cosmos DB error occurred: {e.message}")
    except Exception as e:
        print(f"An unexpected error occurred: {e}")

if __name__ == "__main__":
    main()
