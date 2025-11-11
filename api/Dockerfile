# ===== BUILD STAGE =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the dotnet-api folder into the container
COPY dotnet-api/ ./dotnet-api/

# Change directory into the project folder and restore
WORKDIR /src/dotnet-api
RUN dotnet restore

# Publish the app (Release mode)
RUN dotnet publish -c Release -o /app/publish

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Start the .NET app
ENTRYPOINT ["dotnet", "dotnet-api.dll"]


