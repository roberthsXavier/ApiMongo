FROM microsoft/aspnetcore:2.0-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-sac2016 AS build
WORKDIR /src
COPY ApiEnvio/ApiEnvio.csproj ApiEnvio/
RUN dotnet restore ApiEnvio/ApiEnvio.csproj
COPY . .
WORKDIR /src/ApiEnvio
RUN dotnet build ApiEnvio.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ApiEnvio.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApiEnvio.dll"]
