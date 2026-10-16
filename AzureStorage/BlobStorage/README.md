# Azure Blob Storage sample

This sample uploads `test-blob.txt` to an Azurite Blob Storage emulator,
lists the blobs in the container, then downloads the uploaded blob to
`downloaded-test-blob.txt`.

## Requirements

- Python 3
- Azurite running and reachable at the blob endpoint configured in
  `blobStorageTest.py` (currently `http://192.168.29.239:10000`)

The sample pins the Blob Storage API version to `2025-11-05` for compatibility
with Azurite instances that do not support the SDK's latest API version.

The connection string in the sample uses Azurite's standard development
account and key. If your Azurite endpoint differs, update the connection
string in `blobStorageTest.py` to use the address reachable from your machine.
This is for local development only; use a securely stored Azure Storage
connection string for a real Azure Storage account.

## Install

From this directory, install the dependency:

```powershell
python -m pip install -r requirements.txt
```

## Run

Run from this directory:

```powershell
python blobStorageTest.py
```

The script creates the `test-container` container only if it does not already
exist, and overwrites the blob on reruns. The input and output files are
resolved relative to the script, so the command can also be run from the
`AzureStorage` directory with `python BlobStorage\blobStorageTest.py`.
