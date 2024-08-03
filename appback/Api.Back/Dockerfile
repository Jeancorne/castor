FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Api.Application/Api.Application.csproj", "Api.Application/"]
COPY ["Api.Auth/Api.Auth.csproj", "Api.Auth/"]
COPY ["Api.Common/Api.Common.csproj", "Api.Common/"]
COPY ["Api.Core/Api.Core.csproj", "Api.Core/"]
COPY ["Api.Infraestructure/Api.Infraestructure.csproj", "Api.Infraestructure/"]
COPY ["Api.Presentation/Api.Presentation.csproj", "Api.Presentation/"]

RUN dotnet restore "./Api.Presentation/Api.Presentation.csproj"
COPY . .
WORKDIR "/src/Api.Presentation"
RUN dotnet build "./Api.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ENV ASPNETCORE_ENVIRONMENT=Production
RUN dotnet publish "./Api.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 12050
EXPOSE 12051
ENV ASPNETCORE_URLS=http://+:12050
ENV DOTNET_RUNNING_IN_CONTAINER=true

ENTRYPOINT ["dotnet", "Api.Presentation.dll"]