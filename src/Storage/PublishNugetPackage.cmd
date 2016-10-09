@echo off
echo Press any key to publish
pause
"..\..\.nuget\NuGet.exe" push Cerulean.Storage.1.0.0.0.nupkg -Source https://api.nuget.org/v3/index.json
pause