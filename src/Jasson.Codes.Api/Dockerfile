FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_URLS=https://+:5001;http://+:5000

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Jasson.Codes.Api/jasson.codes.api.csproj", "src/Jasson.Codes.Api/"]
RUN dotnet restore "src/Jasson.Codes.Api/jasson.codes.api.csproj"
COPY . .
WORKDIR "/src/src/Jasson.Codes.Api"
RUN dotnet build "jasson.codes.api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "jasson.codes.api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jasson.codes.api.dll"]
