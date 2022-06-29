FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /repo
COPY global.json Directory.Build.props .editorconfig ./
COPY libs/                                           libs/
COPY apps/api/src/Gracker.Api/Gracker.Api.csproj apps/api/src/Gracker.Api/
RUN dotnet restore                                   apps/api/src/Gracker.Api/Gracker.Api.csproj
COPY apps/api/src/Gracker.Api/                       apps/api/src/Gracker.Api/

FROM build AS publish
WORKDIR /repo/apps/api/src/Gracker.Api/
RUN dotnet publish Gracker.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gracker.Api.dll"]
