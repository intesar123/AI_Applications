# sample code for testing Azure Blob Storage
from pathlib import Path

from azure.storage.blob import BlobServiceClient

# Initialize the BlobServiceClient with a connection string
connection_string = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://192.168.29.239:10000/devstoreaccount1"
blob_service_client = BlobServiceClient.from_connection_string(
    connection_string, api_version="2025-11-05"
)

# Create a container
container_name = "test-container"
container_client = blob_service_client.create_container(container_name)

# Upload a blob to the container
blob_name = "test-blob.txt"
blob_client = blob_service_client.get_blob_client(container=container_name, blob=blob_name)
sample_dir = Path(__file__).resolve().parent
with (sample_dir / "test-blob.txt").open("rb") as data:
    blob_client.upload_blob(data)

# List blobs in the container
print("Blobs in container:")
for blob in container_client.list_blobs():
    print(blob.name)

# Download the blob
with (sample_dir / "downloaded-test-blob.txt").open("wb") as download_file:
    download_file.write(blob_client.download_blob().readall())

print("Blob downloaded successfully.")