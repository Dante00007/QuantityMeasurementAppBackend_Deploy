# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the solution file
COPY *.slnx ./

# Copy the actual project files based on your screenshot
COPY QuantityMeasurementAPI/*.csproj ./QuantityMeasurementAPI/
COPY QuantityMeasurementAppBusinessLayer/*.csproj ./QuantityMeasurementAppBusinessLayer/
COPY QuantityMeasurementAppModelLayer/*.csproj ./QuantityMeasurementAppModelLayer/
COPY QuantityMeasurementAppRepoLayer/*.csproj ./QuantityMeasurementAppRepoLayer/

# Restore dependencies
RUN dotnet restore "QuantityMeasurementAPI/QuantityMeasurementAPI.csproj"

# Copy the rest of the source code
COPY . .

# Publish the API
RUN dotnet publish "QuantityMeasurementAPI/QuantityMeasurementAPI.csproj" -c Release -o /out

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /out .

# Render dynamic port handling
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "QuantityMeasurementAPI.dll"]