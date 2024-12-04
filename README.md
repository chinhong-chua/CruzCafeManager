# CruzCafeManager

# For initial round, please ensure you have dotnet CLI and efcore tools installed
# Please restore the database from https://cruzstorages.blob.core.windows.net/db-container/cafe_manager.bacpac
# You should use the Import Data Tier Application task for easy setup

# Backend Running steps
1. cd CafeBackend
2. update DefaultConnection in appsettings.json with your local restored database connection string
3. dotnet ef database update (if database not existed yet)
4. dotnet run

# Frontend Running steps
1. npm install (initial setup)
2. npm run dev