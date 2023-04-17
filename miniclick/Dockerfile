#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Настройка имени хоста
ARG VAR_HOSTNAME=127.0.0.1:5000
ENV MY_HOSTNAME=${VAR_HOSTNAME}


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["miniclick/miniclick.csproj", "miniclick/"]
RUN dotnet restore "miniclick/miniclick.csproj"
COPY . .
WORKDIR "/src/miniclick"
RUN dotnet build "miniclick.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "miniclick.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN mkdir -p /app/Data

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "miniclick.dll"]