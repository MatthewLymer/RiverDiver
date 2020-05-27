FROM mcr.microsoft.com/dotnet/core/sdk:2.1.700-stretch AS build

WORKDIR /app

COPY ./src/ ./src/

RUN dotnet publish ./src/RiverDiver.Web.App --configuration Release --output /app/artifacts

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1.11-stretch-slim

WORKDIR /app

RUN apt-get update && apt-get install -y libc6-dev libgdiplus libx11-dev

COPY --from=build /app/artifacts ./

ENTRYPOINT ["dotnet", "RiverDiver.Web.App.dll"]