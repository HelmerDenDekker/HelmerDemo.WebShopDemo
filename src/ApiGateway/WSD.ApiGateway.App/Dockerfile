#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateway/WSD.ApiGateway.App/WSD.ApiGateway.App.csproj", "ApiGateway/WSD.ApiGateway.App/"]
RUN dotnet restore "ApiGateway/WSD.ApiGateway.App/WSD.ApiGateway.App.csproj"
COPY . .
WORKDIR "/src/ApiGateway/WSD.ApiGateway.App"
RUN dotnet build "WSD.ApiGateway.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WSD.ApiGateway.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WSD.ApiGateway.App.dll"]