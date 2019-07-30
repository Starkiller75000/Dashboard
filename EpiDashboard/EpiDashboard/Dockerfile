FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8080

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY EpiDashboard/EpiDashboard.csproj EpiDashboard/
RUN dotnet restore EpiDashboard/EpiDashboard.csproj
COPY . .
WORKDIR /src/EpiDashboard
RUN dotnet build EpiDashboard.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EpiDashboard.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EpiDashboard.dll"]
