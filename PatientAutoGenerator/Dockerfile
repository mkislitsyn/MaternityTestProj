FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /

COPY PatientAutoGenerator/PatientAutoGenerator/PatientAutoGenerator.csproj PatientAutoGenerator/
RUN dotnet restore PatientAutoGenerator/PatientAutoGenerator.csproj

COPY PatientAutoGenerator/PatientAutoGenerator/ PatientAutoGenerator/
WORKDIR /PatientAutoGenerator
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:6.0

WORKDIR /app

COPY --from=build /app .

ENTRYPOINT ["dotnet", "PatientAutoGenerator.dll"]