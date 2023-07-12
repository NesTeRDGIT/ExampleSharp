#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base

RUN apt-get update && apt-get install -y locales

ENV TZ=Asia/Chita
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN dotnet restore "zms.Root.Worker.SmsService/zms.Root.Worker.SmsService.csproj"
COPY . .
WORKDIR "/src/zms.Root.Worker.SmsService"
RUN dotnet build "zms.Root.Worker.SmsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "zms.Root.Worker.SmsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "zms.Root.Worker.SmsService.dll"]