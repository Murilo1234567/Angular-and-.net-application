# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /API

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /API
COPY --from=build-env /API/out .
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "api.dll"]