FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HealthManagerServer/HealthManagerServer.csproj", "HealthManagerServer/"]
RUN dotnet restore "HealthManagerServer/HealthManagerServer.csproj"
COPY . .
WORKDIR "/src/HealthManagerServer"
RUN dotnet build "HealthManagerServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HealthManagerServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthManagerServer.dll"]
