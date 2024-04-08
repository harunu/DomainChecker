# Use the SDK image to build the app and run tests
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
# Clear NuGet package cache to ensure fresh restore
RUN dotnet nuget locals all --clear

# Restore and build the main application
COPY ["DomainChecker/DomainChecker.csproj", "./DomainChecker/"]
RUN dotnet restore "DomainChecker/DomainChecker.csproj"
COPY ["DomainChecker/", "./DomainChecker/"]
RUN dotnet build "DomainChecker/DomainChecker.csproj" -c Release -o /app/build

# Restore and build UnitTests
COPY ["UnitTests/UnitTests.csproj", "./UnitTests/"]
RUN dotnet restore "./UnitTests/UnitTests.csproj"
COPY ["UnitTests/", "./UnitTests/"]
# Ensure build is successful before running tests
RUN dotnet build "./UnitTests/UnitTests.csproj" -c Release
RUN dotnet test "./UnitTests/UnitTests.csproj" --no-build --verbosity normal || true

# Publish the application
FROM build AS publish
RUN dotnet publish "./DomainChecker/DomainChecker.csproj" -c Release -o /app/publish

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "DomainChecker.dll"]

