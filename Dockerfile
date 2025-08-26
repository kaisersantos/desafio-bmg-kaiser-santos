# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia os csproj e restaura dependências
COPY *.sln .
COPY MyApp.API/*.csproj ./MyApp.API/
COPY MyApp.Application/*.csproj ./MyApp.Application/
COPY MyApp.Domain/*.csproj ./MyApp.Domain/
COPY MyApp.Infrastructure/*.csproj ./MyApp.Infrastructure/
RUN dotnet restore

# Copia o código restante e publica
COPY . .
WORKDIR /src/MyApp.API
RUN dotnet publish -c Release -o /app

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copia os artefatos da build
COPY --from=build /app .

# Porta exposta
EXPOSE 5000
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_URLS=http://+:5000

# Entry point
ENTRYPOINT ["dotnet", "MyApp.API.dll"]