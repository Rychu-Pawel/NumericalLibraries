$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
Remove-Item "$dir\*.nupkg"
nuget pack "$dir\Derivative.csproj" -Prop Configuration=Release