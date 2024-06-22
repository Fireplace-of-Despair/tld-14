# I hate docker
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app

WORKDIR /src
COPY ["TLD14/TLD14.csproj", "./TLD14/"]
COPY ["Common/Common.csproj", "./Common/"]
RUN dotnet restore "TLD14/TLD14.csproj"

COPY . .

RUN dotnet publish "TLD14/TLD14.csproj" -c Release -o /app/out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

# Enable globalization and time zones:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/enable-globalization.md
ENV \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8

RUN apk add --no-cache \
    icu-data-full \
    icu-libs

WORKDIR /app
COPY --from=build /app/out ./

# Expose port 8080 for the web application
EXPOSE 8080

# Set the entry point for the container
ENTRYPOINT ["dotnet", "./TLD14.dll"]
