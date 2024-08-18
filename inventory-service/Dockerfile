# Use .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the csproj file and restore as distinct layers
COPY Inventory.Host/Inventory.Host.csproj Inventory.Host/
RUN dotnet restore Inventory.Host/Inventory.Host.csproj

# Copy the rest of the files and build
COPY . ./
WORKDIR /app/Inventory.Host
RUN dotnet publish Inventory.Host.csproj -c Release -o out

# Use .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/Inventory.Host/out .
ENTRYPOINT [ "dotnet", "Inventory.Host.dll" ]
