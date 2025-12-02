FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY TestApi.slnx .
COPY TestApi/TestApi.csproj TestApi/
COPY TestApi.Tests/TestApi.Tests.csproj TestApi.Tests/

RUN dotnet restore TestApi/TestApi.csproj

COPY . .

WORKDIR /src/TestApi
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

COPY TestApi/TestJson/MainTest/test.json TestJson/MainTest/
COPY TestApi/TestJson/UserTestsResults/testResults.json TestJson/UserTestResults/

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "TestApi.dll"]
