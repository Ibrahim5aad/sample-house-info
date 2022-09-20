FROM nginx:1 AS base
WORKDIR /usr/share/nginx/html
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Clients/SPAClient/SPAClient.csproj", "src/Clients/SPAClient/"]
RUN dotnet restore "src/Clients/SPAClient/SPAClient.csproj"
COPY . .
WORKDIR "/src/src/Clients/SPAClient"
RUN dotnet build "SPAClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SPAClient.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .