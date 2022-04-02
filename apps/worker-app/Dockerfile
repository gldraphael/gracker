FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY global.json Directory.Build.props .editorconfig ./
COPY apps/worker-app/src/Gracker.WorkerApp/Gracker.WorkerApp.csproj Gracker.WorkerApp/
RUN dotnet restore Gracker.WorkerApp/Gracker.WorkerApp.csproj
COPY apps/worker-app/src/Gracker.WorkerApp/ ./Gracker.WorkerApp/
WORKDIR /src/Gracker.WorkerApp
RUN dotnet build "Gracker.WorkerApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gracker.WorkerApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gracker.WorkerApp.dll"]
