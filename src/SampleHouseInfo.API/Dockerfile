#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SampleHouseInfo.API/SampleHouseInfo.API.csproj", "src/SampleHouseInfo.API/"]
COPY ["src/SampleHouseInfo.Application/SampleHouseInfo.Application.csproj", "src/SampleHouseInfo.Application/"]
COPY ["src/SampleHouseInfo.Domain/SampleHouseInfo.Domain.csproj", "src/SampleHouseInfo.Domain/"]
COPY ["src/SampleHouseInfo.Infrastructure.Persistence/SampleHouseInfo.Infrastructure.Persistence.csproj", "src/SampleHouseInfo.Infrastructure.Persistence/"]
RUN dotnet restore "src/SampleHouseInfo.API/SampleHouseInfo.API.csproj"
COPY . .
WORKDIR "/src/src/SampleHouseInfo.API"
RUN dotnet build "SampleHouseInfo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SampleHouseInfo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleHouseInfo.API.dll"]