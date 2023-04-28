# Recipe Vault

To run the app:
  - In Backend/FoodVaultApi, run `dotnet watch run`
  - In FoodVault_Web, run `npm run dev`

Database Migrations
- If you change a model, create a migration
  - `dotnet ef migrations add <MigrationName>`
  - You don't need the <> symbols