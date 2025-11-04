# Use the .NET 8 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies (for better layer caching)
COPY ["OrderService/OrderService.csproj", "OrderService/"]
RUN dotnet restore "OrderService/OrderService.csproj"

# Copy the rest of the source code
COPY . .

# Set working directory to the project folder
WORKDIR /src/OrderService

# Build and publish the application in Release configuration
RUN dotnet publish -c Release -o /app/publish --no-restore

# Use the .NET 8 ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose port 80 (standard for ASP.NET Core in containers)
EXPOSE 80

# Set ASP.NET Core to listen on port 80 (overridable via environment variables)
ENV ASPNETCORE_URLS=http://+:80

# Set the entrypoint to run the application
ENTRYPOINT ["dotnet", "OrderService.dll"]