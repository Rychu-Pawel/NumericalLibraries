$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
Remove-Item "$dir\*.nupkg"
nuget pack "$dir\LinearEquation.csproj" -IncludeReferencedProjects -Prop Configuration=Release