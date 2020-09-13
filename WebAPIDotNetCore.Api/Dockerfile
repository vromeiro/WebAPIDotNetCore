#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY WebAPIDotNetCore.Api/WebAPIDotNetCore.Api.csproj WebAPIDotNetCore.Api/
COPY WebAPIDotNetCore.Domain/WebAPIDotNetCore.Domain.csproj WebAPIDotNetCore.Domain/
RUN dotnet restore "WebAPIDotNetCore.Api/WebAPIDotNetCore.Api.csproj"
COPY . .
WORKDIR "/src/WebAPIDotNetCore.Api"
RUN dotnet build "WebAPIDotNetCore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPIDotNetCore.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "WebAPIDotNetCore.Api.dll"]
