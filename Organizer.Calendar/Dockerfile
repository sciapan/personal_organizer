#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Src/Calendar.Api/Calendar.Api.csproj", "Src/Calendar.Api/"]
COPY ["Src/Calendar.Application/Calendar.Application.csproj", "Src/Calendar.Application/"]
COPY ["Src/Calendar.Domain/Calendar.Domain.csproj", "Src/Calendar.Domain/"]
COPY ["Src/Calendar.Migrations/Calendar.Migrations.csproj", "Src/Calendar.Migrations/"]
COPY ["Src/Calendar.Persistence/Calendar.Persistence.csproj", "Src/Calendar.Persistence/"]
RUN dotnet restore "Src/Calendar.Api/Calendar.Api.csproj"
COPY . .
WORKDIR "/src/Src/Calendar.Api"
RUN dotnet build "Calendar.Api.csproj" -c Release -o /app/build

# Install dotnet debug tools
RUN dotnet tool install --tool-path /tools dotnet-trace \
 && dotnet tool install --tool-path /tools dotnet-counters \
 && dotnet tool install --tool-path /tools dotnet-dump \
 && dotnet tool install --tool-path /tools dotnet-gcdump

FROM build AS publish
RUN dotnet publish "Calendar.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy dotnet-tools
COPY --from=build /tools /tools

ENTRYPOINT ["dotnet", "Calendar.Api.dll"]