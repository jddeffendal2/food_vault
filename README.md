# Recipe Vault

To run the app:
  - In Backend/FoodVaultApi, run `dotnet watch run`
  - In FoodVault_Web, run `npm run dev`

Database Migrations
- If you change a model, create a migration
  - `dotnet ef migrations add <MigrationName>`
  - You don't need the <> symbols

### Feature Ideas
- On home page after signing in, we show your favorite recipes (idk how to find that yet)
  - There could be a drop down containing every group you belong to, which would filter the recipes by group. Drop down could have an option like "Mine" which would show the recipes you created but haven't shared with a group