#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ETT-Backend.csproj", "ETT-Backend/"]
RUN dotnet restore "ETT-Backend/ETT-Backend.csproj"
COPY [".", "ETT-Backend/"]
WORKDIR "/src/ETT-Backend"
RUN dotnet build "ETT-Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETT-Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt update && \
    apt install unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg
ENTRYPOINT ["dotnet", "ETT-Backend.dll"]