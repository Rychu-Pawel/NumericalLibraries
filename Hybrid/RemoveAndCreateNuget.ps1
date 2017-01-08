$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
Remove-Item "$dir\*.nupkg"
nuget pack "$dir\Hybrid.csproj" -Prop Configuration=Release