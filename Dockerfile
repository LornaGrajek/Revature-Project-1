FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 

WORKDIR /app

COPY *.sln ./
COPY StoreBL/*.csproj ./StoreBL/
COPY StoreDL/*.csproj ./StoreDL/
COPY StoreModels/*.csproj ./StoreModels/
COPY Tests/*.csproj ./Tests/
COPY WebApplication1/*.csproj ./WebApplication1/

RUN dotnet restore

COPY . ./

RUN dotnet publish WebApplication1 -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS RUN

WORKDIR /app

COPY --from=build /app/publish ./ 

CMD [ "dotnet", "WebApplication1.dll" ]