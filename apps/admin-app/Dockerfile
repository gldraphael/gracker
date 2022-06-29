FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /repo
COPY global.json Directory.Build.props .editorconfig ./
COPY libs/                                           libs/
COPY apps/admin-app/src/Gracker.AdminApp/Gracker.AdminApp.csproj apps/admin-app/src/Gracker.AdminApp/
RUN dotnet restore apps/admin-app/src/Gracker.AdminApp/Gracker.AdminApp.csproj
COPY apps/admin-app/src/Gracker.AdminApp/            apps/admin-app/src/Gracker.AdminApp/

FROM build AS publish
WORKDIR /repo/apps/admin-app/src/Gracker.AdminApp/
RUN dotnet publish "Gracker.AdminApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gracker.AdminApp.dll"]
