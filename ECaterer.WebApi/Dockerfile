#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ECaterer.WebApi/ECaterer.WebApi.csproj", "ECaterer.WebApi/"]
COPY ["ECaterer.Contracts/ECaterer.Contracts.csproj", "ECaterer.Contracts/"]
COPY ["ECaterer.Web/ECaterer.Web.csproj", "ECaterer.Web/"]
COPY ["ECaterer.Core/ECaterer.Core.csproj", "ECaterer.Core/"]
RUN dotnet restore "ECaterer.WebApi/ECaterer.WebApi.csproj"
COPY . .
WORKDIR "/src/ECaterer.WebApi"
RUN dotnet build "ECaterer.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECaterer.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECaterer.WebApi.dll"]