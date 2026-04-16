# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the solution file
COPY *.slnx ./

# Copy each project file (.csproj) to its respective folder to restore dependencies
# This allows Docker to cache your layers for faster builds
COPY QuantityMeasurementAPI/*.csproj ./QuantityMeasurementAPI/
COPY QuantityMeasurementApp/*.csproj ./QuantityMeasurementApp/
COPY QuantityMeasurementApp.Tests/*.csproj ./QuantityMeasurementApp.Tests/
COPY QuantityMeasurementAppBusinessLayer/*.csproj ./QuantityMeasurementAppBusinessLayer/
COPY QuantityMeasurementAppConsole/*.csproj ./QuantityMeasurementAppConsole/
COPY QuantityMeasurementAppModelLayer/*.csproj ./QuantityMeasurementAppModelLayer/
COPY QuantityMeasurementAppRepoLayer/*.csproj ./QuantityMeasurementAppRepoLayer/

# Restore dependencies
RUN dotnet restore "QuantityMeasurementAPI/QuantityMeasurementAPI.csproj"

# Copy everything else and publish the API
COPY . .
RUN dotnet publish "QuantityMeasurementAPI/QuantityMeasurementAPI.csproj" -c Release -o /out

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /out .

# Expose port 8080 (the default we set in Program.cs)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "QuantityMeasurementAPI.dll"]