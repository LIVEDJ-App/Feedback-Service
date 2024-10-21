FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5020

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

COPY ["Feedback.Api/Feedback.Api.csproj", "Feedback.Api/"]
COPY ["Feedback.Application/Feedback.Application.csproj", "Feedback.Application/"]
COPY ["Feedback.Domain/Feedback.Domain.csproj", "Feedback.Domain/"]
COPY ["Feedback.Persistence/Feedback.Persistence.csproj", "Feedback.Persistence/"]

RUN dotnet restore "Feedback.Api/Feedback.Api.csproj"

COPY . .
WORKDIR "/src/Feedback.Api"
RUN dotnet build "Feedback.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Feedback.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Feedback.Api.dll"]
