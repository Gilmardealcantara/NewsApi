#!/bin/bash

DB_NAME='news-dev'

echo -e "\n***DB SETUP INIT '${DB_NAME}' ***"

# Wait to be sure that SQL Server came up
sec=40
echo -e "\n***Waiting init db for apply sql scripts... (${sec}s)***"
while [ $sec -gt 0 ]; do
    echo "Exec scripts in $sec seconds"
    ((sec=sec-2))
    sleep 2
done

# Run the setup script to create the DB and the schema in the DB
# Note: make sure that your password matches what is in the Dockerfile
ls ./scripts/

echo -e "\n***CREATING DB '${DB_NAME}'***"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Senha@123 -d master -i ./scripts/01-Create-Database.sql

echo -e "\n***CREATING SCHEMA from repository scripts***"

for file in $(ls ./scripts/*.sql); do 
    echo "Apply sql: $file"; 
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Senha@123 -d ${DB_NAME} -i $file
done

echo -e "\n***Seed Data***"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Senha@123 -d ${DB_NAME} -i ./scripts/02-Insert-News.sql


echo -e "***DB SETUP DONE '${DB_NAME}' !!!***\n"
