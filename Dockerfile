# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore BPCalculator/BPCalculator.csproj

# Build and publish the app
RUN dotnet publish BPCalculator/BPCalculator.csproj -c Release -o /out

# Use the ASP.NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Set environment variable for Render (listens on port 8080)
ENV ASPNETCORE_URLS=http://+:8080

# Copy published app from build stage
COPY --from=build /out .

# Start the app
CMD ["dotnet", "BPCalculator.dll"]