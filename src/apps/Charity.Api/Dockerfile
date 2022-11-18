# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY src/apps/Charity.Api/Charity.Api.csproj src/apps/Charity.Api/
COPY src/common/Charity.Migrations/Charity.Migrations.csproj src/common/Charity.Migrations/
COPY src/common/Charity.Infrastructure/Charity.Infrastructure.csproj src/common/Charity.Infrastructure/
COPY src/common/Charity.Application/Charity.Application.csproj src/common/Charity.Application/
COPY src/common/Charity.Domain/Charity.Domain.csproj src/common/Charity.Domain/
RUN dotnet restore src/apps/Charity.Api/Charity.Api.csproj --use-current-runtime

# copy everything else and build app
COPY . .
RUN dotnet publish src/apps/Charity.Api/Charity.Api.csproj -c Release -o /app --use-current-runtime --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Charity.Api.dll"]