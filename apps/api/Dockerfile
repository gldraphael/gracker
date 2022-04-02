FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY global.json Directory.Build.props .editorconfig ./
COPY apps/api/src/Gracker.Api/Gracker.Api.csproj Gracker.Api/
RUN dotnet restore Gracker.Api/Gracker.Api.csproj
COPY apps/api/src/Gracker.Api/ ./Gracker.Api/
WORKDIR /src/Gracker.Api
RUN dotnet build "Gracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gracker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gracker.Api.dll"]
