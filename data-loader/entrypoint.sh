#!/bin/bash

DB_NAME="Cars"

echo "⏳ Waiting for SQL Server to accept connections..."
for i in {1..30}; do
    /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1
    if [ $? -eq 0 ]; then
        echo "✅ SQL Server is ready to accept connections."
        break
    fi
    echo "❌ SQL Server not ready yet (attempt $i), retrying in 2s..."
    sleep 2
done

if [ $i -eq 30 ]; then
    echo "💥 SQL Server did not become ready in time. Exiting."
    exit 1
fi

echo "🔍 Checking if database '$DB_NAME' already exists..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -Q "SELECT name FROM sys.databases WHERE name = '$DB_NAME';" -h -1 > /tmp/db_check.txt

if grep -q "^$DB_NAME$" /tmp/db_check.txt; then
    echo "✅ Database '$DB_NAME' already exists. Skipping initialization script."
else
    echo "🚀 Database not found. Executing /app/init.sql..."
    /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -i /app/init.sql
    if [ $? -eq 0 ]; then
        echo "✅ init.sql executed successfully."
    else
        echo "❌ Failed to execute init.sql."
        exit 1
    fi
fi

# Keep the container alive for inspection/debugging
tail -f /dev/null


