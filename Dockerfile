#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BMS.API/BMS.API.csproj", "BMS.API/"]
RUN dotnet restore "BMS.API/BMS.API.csproj"
COPY . .
WORKDIR "/src/BMS.API"
RUN dotnet build "BMS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BMS.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apt-get update
RUN apt-get install wget libgdiplus -y
RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.dll
RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.dylib
RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.so

ENTRYPOINT ["dotnet", "BMS.API.dll"]