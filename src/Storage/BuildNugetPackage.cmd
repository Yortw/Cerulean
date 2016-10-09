del /F /Q /S *.CodeAnalysisLog.xml

"..\..\.nuget\NuGet.exe" pack -sym Cerulean.Storage.nuspec -BasePath .\
pause

copy *.nupkg C:\Nuget.LocalRepository\
pause
