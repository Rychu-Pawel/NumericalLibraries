$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
Remove-Item "$dir\*.nupkg"
nuget pack "$dir\FourierTransform.csproj" -Prop Configuration=Release