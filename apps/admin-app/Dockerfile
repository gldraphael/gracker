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

FROM node:lts-alpine AS spa-build
ENV NODE_ENV production
ENV BUILD_PATH /build
WORKDIR /repo
COPY .editorconfig apps/admin-app/src/gracker-admin-spa/package.json apps/admin-app/src/gracker-admin-spa/package-lock.json ./
WORKDIR /repo/apps/admin-app/src/gracker-admin-spa
RUN npm install
COPY apps/admin-app/src/gracker-admin-spa /repo
RUN npm run build

FROM build AS publish
WORKDIR /repo/apps/admin-app/src/Gracker.AdminApp/
RUN dotnet publish "Gracker.AdminApp.csproj" -c Release -o /app/publish
COPY --from=spa-build /build /app/publish/wwwroot/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gracker.AdminApp.dll"]
