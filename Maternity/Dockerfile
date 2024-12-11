FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY Maternity/Maternity.sln ./
COPY Maternity/src ./src

RUN dotnet restore Maternity.sln

RUN dotnet build Maternity.sln -c Release --no-restore

RUN dotnet publish src/Maternity.API/Maternity.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

RUN apt-get update && apt-get install -y curl

COPY --from=build /app/publish .

EXPOSE 7000

ENTRYPOINT ["dotnet", "Maternity.API.dll"]