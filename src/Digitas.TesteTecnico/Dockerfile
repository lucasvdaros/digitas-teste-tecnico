FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Digitas.Quotes.Api/Digitas.Quotes.Api.csproj", "Digitas.Quotes.Api/"]
COPY ["Digitas.Quotes.Application/Digitas.Quotes.Application.csproj", "Digitas.Quotes.Application/"]
COPY ["Digitas.Quotes.Domain/Digitas.Quotes.Domain.csproj", "Digitas.Quotes.Domain/"]
COPY ["Digitas.Quotes.Infra/Digitas.Quotes.Infra.csproj", "Digitas.Quotes.Infra/"]
RUN dotnet restore "Digitas.Quotes.Api/Digitas.Quotes.Api.csproj"
COPY "Digitas.Quotes.Api/" "Digitas.Quotes.Api/"
COPY "Digitas.Quotes.Application/" "Digitas.Quotes.Application/"
COPY "Digitas.Quotes.Domain/" "Digitas.Quotes.Domain/"
COPY "Digitas.Quotes.Infra/" "Digitas.Quotes.Infra/"
WORKDIR "/src/Digitas.Quotes.Api"
RUN dotnet build "Digitas.Quotes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Digitas.Quotes.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Digitas.Quotes.Api.dll"]