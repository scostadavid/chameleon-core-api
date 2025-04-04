FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["chameleon-core-api.csproj", "./"]
RUN dotnet restore "chameleon-core-api.csproj"
COPY . .
RUN dotnet publish "chameleon-core-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "chameleon-core-api.dll"]
