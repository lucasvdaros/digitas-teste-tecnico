#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated7.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Digitas.QuotesMetrics.Function.csproj", "Digitas.QuotesMetrics.Function/"]
RUN dotnet restore "Digitas.QuotesMetrics.Function/Digitas.QuotesMetrics.Function.csproj"
COPY . "Digitas.QuotesMetrics.Function/"
WORKDIR "/src/Digitas.QuotesMetrics.Function"
RUN dotnet build "Digitas.QuotesMetrics.Function.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Digitas.QuotesMetrics.Function.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true