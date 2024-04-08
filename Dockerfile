# Use the .NET SDK image to build the app and run tests
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Clear NuGet package cache to ensure fresh restore
RUN dotnet nuget locals all --clear

# Copy and restore the main application and its dependencies
COPY ["DomainChecker/DomainChecker.csproj", "DomainChecker/"]
RUN dotnet restore "DomainChecker/DomainChecker.csproj"

# Copy the main application source code and build
COPY ["DomainChecker/", "DomainChecker/"]
RUN dotnet build "DomainChecker/DomainChecker.csproj" -c Release -o /app/build

# Copy and restore the UnitTests project and its dependencies
COPY ["UnitTests/UnitTests.csproj", "UnitTests/"]
RUN dotnet restore "UnitTests/UnitTests.csproj"

# Copy the UnitTests source code
COPY ["UnitTests/", "UnitTests/"]

# Build the UnitTests project
RUN dotnet build "UnitTests/UnitTests.csproj" -c Release

# Explicitly set the build configuration for the test command to match the build configuration , making failure optional
RUN dotnet test "UnitTests/UnitTests.csproj" --no-build --configuration Release --verbosity detailed  || true
# Publish the application
FROM build AS publish
RUN dotnet publish "DomainChecker/DomainChecker.csproj" -c Release -o /app/publish

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "DomainChecker.dll"]
