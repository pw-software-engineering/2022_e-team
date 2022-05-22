#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ECaterer.Web/ECaterer.Web.csproj", "ECaterer.Web/"]
COPY ["ECaterer.Contracts/ECaterer.Contracts.csproj", "ECaterer.Contracts/"]
COPY ["ECaterer.Core/ECaterer.Core.csproj", "ECaterer.Core/"]
RUN dotnet restore "ECaterer.Web/ECaterer.Web.csproj"
COPY . .
WORKDIR "/src/ECaterer.Web"
RUN dotnet build "ECaterer.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_14.x | bash - && \
apt-get install -y build-essential nodejs
RUN dotnet publish "ECaterer.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECaterer.Web.dll"]