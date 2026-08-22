# <https://hub.docker.com/_/microsoft-dotnet>
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY global.json .
COPY CodeMaze.slnx .

COPY Domain/Domain.csproj Domain/
COPY MiddlewareExample/MiddlewareExample.csproj MiddlewareExample/
COPY Repository/Repository.csproj Repository/
COPY Webapi/Webapi.csproj Webapi/
COPY Webapi.Tests/Webapi.Tests.csproj Webapi.Tests/

RUN dotnet restore CodeMaze.slnx

# copy everything else and build app
COPY . .
RUN dotnet publish Webapi/Webapi.csproj -c Release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "Webapi.dll"]