#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

COPY /cert/rootzms.crt /usr/local/share/ca-certificates/rootzms.crt
RUN chmod 644 /usr/local/share/ca-certificates/rootzms.crt && update-ca-certificates

ENV TZ=Asia/Chita
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY . .
WORKDIR "/src/zms.Root.Api.SmsService"
RUN dotnet build "zms.Root.Api.SmsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "zms.Root.Api.SmsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "zms.Root.Api.SmsService.dll"]