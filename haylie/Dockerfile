FROM mcr.microsoft.com/dotnet/sdk AS build-env
WORKDIR /haylie

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet build haylie/haylie*.csproj -c Debug -o ./publish
RUN dotnet publish haylie/haylie*.csproj -c Debug -o ./publish
#RUN dotnet publish -o app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /
COPY --from=build-env haylie/publish .
ENTRYPOINT ["dotnet", "haylie.dll"]