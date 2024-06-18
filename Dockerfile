# I hate docker
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

WORKDIR /src
COPY ["TLD14/TLD14.csproj", "./TLD14/"]
COPY ["Common/Common.csproj", "./Common/"]
RUN dotnet restore "TLD14/TLD14.csproj"

COPY . .

RUN dotnet publish "TLD14/TLD14.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose port 8080 for the web application
EXPOSE 8080

# Set the entry point for the container
ENTRYPOINT ["dotnet", "./TLD14.dll"]
