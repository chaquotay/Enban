@ECHO OFF

mkdir build
dotnet pack src\Enban\Enban.csproj --output build --configuration Release