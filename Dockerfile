# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

# Copy everything and restore
COPY . .
RUN dotnet restore "./QuantityMeasurementAPI/QuantityMeasurementAPI.csproj"

# Publish the API project
RUN dotnet publish "./QuantityMeasurementAPI/QuantityMeasurementAPI.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port Render will use
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "QuantityMeasurementAPI.dll"]