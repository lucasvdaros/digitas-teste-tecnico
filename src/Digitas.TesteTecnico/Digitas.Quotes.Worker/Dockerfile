FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Digitas.Quotes.Worker/Digitas.Quotes.Worker.csproj", "Digitas.Quotes.Worker/"]
RUN dotnet restore "Digitas.Quotes.Worker/Digitas.Quotes.Worker.csproj"
COPY . .
WORKDIR "/src/Digitas.Quotes.Worker"
RUN dotnet build "Digitas.Quotes.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Digitas.Quotes.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Digitas.Quotes.Worker.dll"]