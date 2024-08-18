# Use .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the csproj file and restore as distinct layers
COPY Order.Host/Order.Host.csproj Order.Host/
RUN dotnet restore Order.Host/Order.Host.csproj

# Copy the rest of the files and build
COPY . ./
WORKDIR /app/Order.Host
RUN dotnet publish Order.Host.csproj -c Release -o out

# Use .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/Order.Host/out .
ENTRYPOINT [ "dotnet", "Order.Host.dll" ]
