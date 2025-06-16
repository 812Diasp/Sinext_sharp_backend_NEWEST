FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sinext_sharp_backend/Sinext_sharp_backend.csproj", "Sinext_sharp_backend/"]
RUN dotnet restore "Sinext_sharp_backend/Sinext_sharp_backend.csproj"
COPY . .
WORKDIR "/src/Sinext_sharp_backend"
RUN dotnet build "Sinext_sharp_backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sinext_sharp_backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sinext_sharp_backend.dll"]
