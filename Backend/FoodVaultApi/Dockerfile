# Use an official .NET 6 SDK image as a parent image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory to /app
WORKDIR /app

# Copy the .csproj file to the container
COPY *.csproj ./

# Restore the project dependencies
RUN dotnet restore

# Copy the rest of the project files to the container
COPY . .

# Build the project
RUN dotnet publish -c Release -o out

# Use an official .NET 6 runtime image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory to /app
WORKDIR /app

# Copy the published files from the previous build to the container
COPY --from=build /app/out .

# Expose port 5000
EXPOSE 5000

# Start the application
ENTRYPOINT ["dotnet", "FoodVaultApi.dll"]