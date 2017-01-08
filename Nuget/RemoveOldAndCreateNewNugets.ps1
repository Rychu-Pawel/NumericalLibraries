Remove-Item "*.nupkg"

get-childitem -path "$PSScriptRoot\..\" -recurse -filter "RemoveAndCreateNuget.ps1" | foreach-object {
  echo "Running " $_.FullName
  Invoke-Expression $_.FullName
}