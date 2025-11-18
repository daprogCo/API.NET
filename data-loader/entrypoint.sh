#!/bin/bash

DB_NAME="Cars"
TABLE_NAME="Car"
CSV_NAME="Automobile.csv"
CSV_PATH="/tmp/$CSV_NAME"
KAGGLE_DATASET="tawfikelmetwally/automobile-dataset"

SQLCMD=(
  /opt/mssql-tools/bin/sqlcmd
  -S sqlserver
  -U sa
  -P "$SA_PASSWORD"
)

main() {
    if ! wait_for_sqlserver; then
        return 1
    fi

    printf "🔍 Checking if database '%s' exists...\n" "$DB_NAME"
    if ! check_database_exists; then
        if ! run_init_sql; then
            return 1
        fi
    else
        printf "✅ Database '%s' already exists.\n" "$DB_NAME"
    fi

    printf "🔎 Checking if table '%s' is empty...\n" "$TABLE_NAME"
    if check_table_empty; then
        printf "📭 Table '%s' is empty. Proceeding with data download only.\n" "$TABLE_NAME"
        if ! download_and_extract_csv; then
            return 1
        fi
        printf "✅ CSV file is now available at: %s\n" "$CSV_PATH"
        if ! import_csv_to_sql; then
            return 1
        fi
    else
        printf "✅ Table '%s' already has data. Skipping download.\n" "$TABLE_NAME"
    fi

    tail -f /dev/null
}

wait_for_sqlserver() {
    printf "⏳ Waiting for SQL Server to accept connections...\n"
    for i in {1..30}; do
        if "${SQLCMD[@]}" -Q "SELECT 1" > /dev/null; then
            printf "✅ SQL Server is ready to accept connections.\n"
            return 0
        fi
        printf "❌ SQL Server not ready yet (attempt %d), retrying in 2s...\n" "$i"
        sleep 2
    done
    printf "💥 SQL Server did not become ready in time.\n" >&2
    return 1
}

check_database_exists() {
    local result;
    result=$("${SQLCMD[@]}" -h -1 -W -Q "SET NOCOUNT ON; SELECT name FROM sys.databases WHERE name = '$DB_NAME';" 2>/dev/null)
    result=$(printf "%s" "$result" | sed 's/\r//g')
    if [[ "$result" == "$DB_NAME" ]]; then
        return 0
    fi
    return 1
}

run_init_sql() {
    printf "🚀 Executing init.sql to initialize schema...\n"
    if "${SQLCMD[@]}" -i /app/init.sql > /dev/null; then
        printf "✅ init.sql executed successfully.\n"
    else
        printf "❌ Failed to execute init.sql.\n" >&2
        return 1
    fi
}

check_table_empty() {
    local count;
    count=$("${SQLCMD[@]}" -d "$DB_NAME" -h -1 -W -Q "SET NOCOUNT ON; SELECT COUNT(*) FROM $TABLE_NAME;" 2>/dev/null)
    count=$(printf "%s" "$count" | tr -d '\r\n[:space:]')
    if [[ "$count" =~ ^[0-9]+$ && "$count" -eq 0 ]]; then
        return 0
    fi
    return 1
}

download_and_extract_csv() {
    printf "📦 Downloading CSV directly from Kaggle...\n"

    # Download ONLY the CSV file
    if ! kaggle datasets files "$KAGGLE_DATASET" \
        -f "$CSV_NAME" \
        -p /tmp \
        --quiet; then
        printf "❌ Failed to download CSV from Kaggle.\n" >&2
        return 1
    fi

    # Verify presence
    if [[ ! -f "$CSV_PATH" ]]; then
        printf "❌ CSV file '%s' not found after download.\n" "$CSV_PATH" >&2
        return 1
    fi

    printf "✅ CSV downloaded successfully: %s\n" "$CSV_PATH"
    return 0
}


import_csv_to_sql() {
    printf "📥 Importing CSV data into SQL Server...\n"

    if [ ! -f "$CSV_PATH" ]; then
        printf "❌ CSV file '%s' not found. Cannot import.\n" "$CSV_PATH" >&2
        return 1
    fi

    if "${SQLCMD[@]}" -d "$DB_NAME" -i /app/import_data.sql > /dev/null; then
        printf "✅ CSV data imported successfully.\n"
        return 0
    else
        printf "❌ Failed to import CSV data.\n" >&2
        return 1
    fi
}

main
