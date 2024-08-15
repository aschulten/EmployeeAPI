# Usa la imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usa la imagen de build para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EmployeeAPI.csproj", "."]
RUN dotnet restore "EmployeeAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "EmployeeAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeAPI.csproj" -c Release -o /app/publish

# Finaliza la imagen de la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeAPI.dll"]
