reportgenerator -h >/dev/null 2>&1 dotnet tool install -g dotnet-reportgenerator-globaltool 

rm -fr ./TestResults
dotnet test --logger "console;verbosity=detailed" --collect:"XPlat Code Coverage"
reportgenerator "-reports:./**/coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
/mnt/c/Program\ Files\ \(x86\)/Google/Chrome/Application/chrome.exe coveragereport/index.html 
