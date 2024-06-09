FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NoisePresentation/NoisePresentation.csproj", "NoisePresentation/"]
RUN dotnet restore "NoisePresentation/NoisePresentation.csproj"
COPY . .
WORKDIR "/src/NoisePresentation"
RUN dotnet build "NoisePresentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NoisePresentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NoisePresentation.dll"]
