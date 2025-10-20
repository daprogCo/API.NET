#!/bin/bash

echo "🚀 Exécution de /app/init.sql..."

# Lancement du script SQL
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -i /app/init.sql

# Vérification du statut de l'exécution
if [ $? -eq 0 ]; then
    echo "✅ Script SQL exécuté avec succès."
else
    echo "❌ Échec lors de l'exécution du script SQL."
    exit 1
fi

# Garde le conteneur actif (pour debug ou actions manuelles)
tail -f /dev/null
