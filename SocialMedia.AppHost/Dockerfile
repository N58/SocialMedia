FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SocialMedia.AppHost/SocialMedia.AppHost.csproj", "SocialMedia.AppHost/"]
COPY ["SocialMedia.API/SocialMedia.API.csproj", "SocialMedia.API/"]
COPY ["SocialMedia.ServiceDefaults/SocialMedia.ServiceDefaults.csproj", "SocialMedia.ServiceDefaults/"]
COPY ["SocialMedia.Domain/SocialMedia.Domain.csproj", "SocialMedia.Domain/"]
COPY ["SocialMedia.Persistence/SocialMedia.Persistence.csproj", "SocialMedia.Persistence/"]
COPY ["SocialMedia.Application/SocialMedia.Application.csproj", "SocialMedia.Application/"]
RUN dotnet restore "SocialMedia.AppHost/SocialMedia.AppHost.csproj"
COPY . .
WORKDIR "/src/SocialMedia.AppHost"
RUN dotnet build "SocialMedia.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SocialMedia.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialMedia.AppHost.dll"]
