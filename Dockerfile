FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY UsersApi/*.csproj ./UsersApi/
COPY UsersApi.Tests/*.csproj ./UsersApi.Tests/
RUN dotnet restore

# copy everything else and build the app
COPY UsersApi/. ./UsersApi/
COPY UsersApi.Tests/. ./UsersApi.Tests/
WORKDIR /app/UsersApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/UsersApi/out ./

EXPOSE 51000
ENTRYPOINT ["dotnet", "UsersApi.dll"]