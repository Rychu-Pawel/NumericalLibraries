$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
Remove-Item "$dir\*.nupkg"
nuget pack "$dir\Calculator.csproj" -IncludeReferencedProjects -Prop Configuration=Release