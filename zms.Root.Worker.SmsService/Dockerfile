#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base

ENV TZ=Asia/Chita
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["zms.Root.Worker.SmsService/zms.Root.Worker.SmsService.csproj", "zms.Root.Worker.SmsService/"]
COPY ["zms.Infrastructure.External.SmsService.Beeline/zms.Infrastructure.External.SmsService.Beeline.csproj", "zms.Infrastructure.External.SmsService.Beeline/"]
COPY ["zms.Generic.SmsService.Application/zms.Generic.SmsService.Application.csproj", "zms.Generic.SmsService.Application/"]
COPY ["zms.Common.Application/zms.Common.Application.csproj", "zms.Common.Application/"]
COPY ["zms.Common.SharedKernel/zms.Common.SharedKernel.csproj", "zms.Common.SharedKernel/"]
COPY ["zms.Generic.SmsService.Domain/zms.Generic.SmsService.Domain.csproj", "zms.Generic.SmsService.Domain/"]
COPY ["zms.Infrastructure.Logging.Serilog/zms.Infrastructure.Logging.Serilog.csproj", "zms.Infrastructure.Logging.Serilog/"]
COPY ["zms.Root.Module.Application/zms.Root.Module.Application.csproj", "zms.Root.Module.Application/"]
COPY ["zms.Infrastructure.Caching.ZmsMemoryCache/zms.Infrastructure.Caching.ZmsMemoryCache.csproj", "zms.Infrastructure.Caching.ZmsMemoryCache/"]
COPY ["zms.Lib.Extensions/zms.Lib.Extensions.csproj", "zms.Lib.Extensions/"]
COPY ["zms.Infrastructure.Common.DateTimeProvider/zms.Infrastructure.Common.DateTimeProvider.csproj", "zms.Infrastructure.Common.DateTimeProvider/"]
COPY ["zms.Persistence.SqlServer.EF.FileStorage/zms.Persistence.SqlServer.EF.FileStorage.csproj", "zms.Persistence.SqlServer.EF.FileStorage/"]
COPY ["zms.Infrastructure.Utility.FileStorage/zms.Infrastructure.Utility.FileStorage.csproj", "zms.Infrastructure.Utility.FileStorage/"]
COPY ["zms.Root.Module.SmsService.Beeline/zms.Root.Module.SmsService.Beeline.csproj", "zms.Root.Module.SmsService.Beeline/"]
COPY ["zms.Root.Module.SmsService/zms.Root.Module.SmsService.csproj", "zms.Root.Module.SmsService/"]
COPY ["zms.Infrastructure.Report.SmsService/zms.Infrastructure.Report.SmsService.csproj", "zms.Infrastructure.Report.SmsService/"]
COPY ["zms.Infrastructure.Utility.Word/zms.Infrastructure.Utility.Word.csproj", "zms.Infrastructure.Utility.Word/"]
COPY ["zms.Persistence.FileStorage.SmsService/zms.Persistence.FileStorage.SmsService.csproj", "zms.Persistence.FileStorage.SmsService/"]
COPY ["zms.Persistence.SqlServer.EF.SmsService/zms.Persistence.SqlServer.EF.SmsService.csproj", "zms.Persistence.SqlServer.EF.SmsService/"]
COPY ["zms.Persistence.SqlServer.EF.Common/zms.Persistence.SqlServer.EF.Common.csproj", "zms.Persistence.SqlServer.EF.Common/"]
COPY ["zms.Root.Module.SmsService.Rostelecom/zms.Root.Module.SmsService.Rostelecom.csproj", "zms.Root.Module.SmsService.Rostelecom/"]
COPY ["zms.Infrastructure.External.SmsService.Rostelecom/zms.Infrastructure.External.SmsService.Rostelecom.csproj", "zms.Infrastructure.External.SmsService.Rostelecom/"]
RUN dotnet restore "./zms.Root.Worker.SmsService/./zms.Root.Worker.SmsService.csproj"
COPY . .
WORKDIR "/src/zms.Root.Worker.SmsService"
RUN dotnet build "./zms.Root.Worker.SmsService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./zms.Root.Worker.SmsService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --chown=app --from=publish /app/publish .
ENTRYPOINT ["dotnet", "zms.Root.Worker.SmsService.dll"]