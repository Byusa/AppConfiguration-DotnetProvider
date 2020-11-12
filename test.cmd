cd /D "%~dp0"

dotnet test tests\Tests.AzureAppConfiguration\Tests.AzureAppConfiguration.csproj --logger trx;LogFileName=%~dp0TestResults/results.trx ||  exit /b 1
dotnet test tests\Tests.AzureAppConfiguration.AspNetCore\Tests.AzureAppConfiguration.AspNetCore.csproj --logger trx;LogFileName=%~dp0TestResults/results.trx ||  exit /b 1
