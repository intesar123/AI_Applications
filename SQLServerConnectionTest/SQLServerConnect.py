# This script tests the connection to a SQL Server database.
import pyodbc

# Define the connection parameters
server = 'INTESAR'
database = 'ShoppingVillaDB1'
username = 'sa'
password = 'Hello@123'
connection_string = f'DRIVER={{ODBC Driver 17 for SQL Server}};SERVER={server};DATABASE={database};UID={username};PWD={password}'

try:
    # Establish the connection
    connection = pyodbc.connect(connection_string)
    print("Connection successful!")
except pyodbc.Error as e:
    print("Error connecting to SQL Server:", e)
finally:
    if 'connection' in locals() and connection:
        connection.close()