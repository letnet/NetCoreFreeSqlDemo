#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NetCoreFreeSqlDemo.Web/NetCoreFreeSqlDemo.Web.csproj", "NetCoreFreeSqlDemo.Web/"]
COPY ["NetCoreFreeSqlDemo.Domain/NetCoreFreeSqlDemo.Domain.csproj", "NetCoreFreeSqlDemo.Domain/"]
COPY ["NetCoreFreeSqlDemo.Infrastructure/NetCoreFreeSqlDemo.Infrastructure.csproj", "NetCoreFreeSqlDemo.Infrastructure/"]
COPY ["NetCoreFreeSqlDemo.Application/NetCoreFreeSqlDemo.Application.csproj", "NetCoreFreeSqlDemo.Application/"]
COPY ["NetCoreFreeSqlDemo.Utility/NetCoreFreeSqlDemo.Utility.csproj", "NetCoreFreeSqlDemo.Utility/"]
RUN dotnet restore "NetCoreFreeSqlDemo.Web/NetCoreFreeSqlDemo.Web.csproj"
COPY . .
WORKDIR "/src/NetCoreFreeSqlDemo.Web"
RUN dotnet build "NetCoreFreeSqlDemo.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreFreeSqlDemo.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreFreeSqlDemo.Web.dll"]

