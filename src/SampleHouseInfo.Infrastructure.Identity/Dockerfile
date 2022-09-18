#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SampleHouseInfo.Infrastructure.Identity/SampleHouseInfo.Infrastructure.Identity.csproj", "src/SampleHouseInfo.Infrastructure.Identity/"]
RUN dotnet restore "src/SampleHouseInfo.Infrastructure.Identity/SampleHouseInfo.Infrastructure.Identity.csproj"
COPY . .
WORKDIR "/src/src/SampleHouseInfo.Infrastructure.Identity"
RUN dotnet build "SampleHouseInfo.Infrastructure.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SampleHouseInfo.Infrastructure.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleHouseInfo.Infrastructure.Identity.dll"]