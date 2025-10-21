#!/bin/bash

DB_NAME="Cars"
SQLCMD=(
  /opt/mssql-tools/bin/sqlcmd
  -S sqlserver
  -U sa
  -P "$SA_PASSWORD"
)

wait_for_sqlserver() {
    printf "⏳ Waiting for SQL Server to accept connections...\n"
    for i in {1..30}; do
        if "${SQLCMD[@]}" -Q "SELECT 1" > /dev/null 2>&1; then
            printf "✅ SQL Server is ready to accept connections.\n"
            return 0
        fi
        printf "❌ SQL Server not ready yet (attempt %d), retrying in 2s...\n" "$i"
        sleep 2
    done
    printf "💥 SQL Server did not become ready in time. Exiting.\n" >&2
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
    printf "🚀 Database not found. Executing /app/init.sql...\n"
    if "${SQLCMD[@]}" -i /app/init.sql; then
        printf "✅ init.sql executed successfully.\n"
    else
        printf "❌ Failed to execute init.sql.\n" >&2
        return 1
    fi
}

main() {
    if ! wait_for_sqlserver; then
        return 1
    fi

    printf "🔍 Checking if database '%s' already exists...\n" "$DB_NAME"
    if check_database_exists; then
        printf "✅ Database '%s' already exists. Skipping initialization script.\n" "$DB_NAME"
    else
        if ! run_init_sql; then
            return 1
        fi
    fi

    tail -f /dev/null
}

main



