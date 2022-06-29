FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /repo
COPY global.json Directory.Build.props .editorconfig ./
COPY libs/                                           libs/
COPY apps/worker-app/src/Gracker.WorkerApp/Gracker.WorkerApp.csproj apps/worker-app/src/Gracker.WorkerApp/
RUN dotnet restore apps/worker-app/src/Gracker.WorkerApp/Gracker.WorkerApp.csproj
COPY apps/worker-app/src/Gracker.WorkerApp/ apps/worker-app/src/Gracker.WorkerApp/

FROM build AS publish
WORKDIR /repo/apps/worker-app/src/Gracker.WorkerApp/
RUN dotnet publish "Gracker.WorkerApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gracker.WorkerApp.dll"]
