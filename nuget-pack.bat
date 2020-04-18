@ECHO OFF

mkdir build
REM Has problems due to https://github.com/NuGet/Home/issues/8713 (NU5128):
REM nuget pack src\Enban\Enban.csproj -exclude "**\Countries\*.*" -outputDirectory build -prop Configuration=Release
nuget pack src\Enban\Enban.nuspec -outputDirectory build -prop Configuration=Release