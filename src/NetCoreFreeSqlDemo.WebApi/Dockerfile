#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NetCoreFreeSqlDemo.WebApi/NetCoreFreeSqlDemo.WebApi.csproj", "NetCoreFreeSqlDemo.WebApi/"]
COPY ["NetCoreFreeSqlDemo.Application/NetCoreFreeSqlDemo.Application.csproj", "NetCoreFreeSqlDemo.Application/"]
COPY ["NetCoreFreeSqlDemo.Utility/NetCoreFreeSqlDemo.Utility.csproj", "NetCoreFreeSqlDemo.Utility/"]
COPY ["NetCoreFreeSqlDemo.Infrastructure/NetCoreFreeSqlDemo.Infrastructure.csproj", "NetCoreFreeSqlDemo.Infrastructure/"]
RUN dotnet restore "NetCoreFreeSqlDemo.WebApi/NetCoreFreeSqlDemo.WebApi.csproj"
COPY . .
WORKDIR "/src/NetCoreFreeSqlDemo.WebApi"
RUN dotnet build "NetCoreFreeSqlDemo.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreFreeSqlDemo.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreFreeSqlDemo.WebApi.dll"]