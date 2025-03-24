dotnet build --configuration Release

Start-Process powershell -ArgumentList "dotnet run --no-build --project OrleansMultiSilo.Silo/OrleansMultiSilo.Silo.csproj --configuration Release"
Start-Process powershell -ArgumentList "dotnet run --no-build --project OrleansMultiSilo.Silo/OrleansMultiSilo.Silo.csproj --configuration Release /Silo:Port=35701"
Start-Process powershell -ArgumentList "dotnet run --no-build --project OrleansMultiSilo.Silo/OrleansMultiSilo.Silo.csproj --configuration Release /Silo:Port=35702"
Start-Process powershell -ArgumentList "dotnet run --no-build --project OrleansMultiSilo.Silo/OrleansMultiSilo.Silo.csproj --configuration Release /Silo:Port=35703"
Start-Process powershell -ArgumentList "dotnet run --no-build --project OrleansMultiSilo.Silo/OrleansMultiSilo.Silo.csproj --configuration Release /Silo:Port=35704"
Start-Process powershell -ArgumentList "dotnet run --no-build --project OrleansMultiSilo.Silo/OrleansMultiSilo.Silo.csproj --configuration Release /Silo:Port=35705"

Start-Process powershell -ArgumentList "dotnet run --no-build --project OrleansMultiSilo.Api/OrleansMultiSilo.Api.csproj --configuration Release"