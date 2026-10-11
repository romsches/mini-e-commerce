# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files first for optimal caching
COPY ["EcommerceBackend.slnx", "./"]
COPY ["Ecommerce.Api/Ecommerce.Api.csproj", "Ecommerce.Api/"]
COPY ["Ecommerce.Application/Ecommerce.Application.csproj", "Ecommerce.Application/"]
COPY ["Ecommerce.Domain/Ecommerce.Domain.csproj", "Ecommerce.Domain/"]
COPY ["Ecommerce.Infrastructure/Ecommerce.Infrastructure.csproj", "Ecommerce.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "Ecommerce.Api/Ecommerce.Api.csproj"

# Copy the remaining source code
COPY . .
WORKDIR /src/Ecommerce.Api

# Publish the application
RUN dotnet publish "Ecommerce.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.dll"]