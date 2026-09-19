# ==========================================
# Stage 1: Base Image (Runtime)
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

# ==========================================
# Stage 2: Build Image (SDK)
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["CleanArchitectureBoilerplate.sln", "./"]
COPY ["CA.Api/CA.Api.csproj", "CA.Api/"]
COPY ["CA.Application/CA.Application.csproj", "CA.Application/"]
COPY ["CA.Domain/CA.Domain.csproj", "CA.Domain/"]
COPY ["CA.Infrastructure/CA.Infrastructure.csproj", "CA.Infrastructure/"]

RUN dotnet restore "./CleanArchitectureBoilerplate.sln"

COPY . .

WORKDIR "/src/CA.Api"
RUN dotnet build "CA.Api.csproj" -c Release -o /app/build

# ==========================================
# Stage 3: Publish
# ==========================================
FROM build AS publish
RUN dotnet publish "CA.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ==========================================
# Stage 4: Final Image
# ==========================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.Api.dll"]