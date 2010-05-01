..\lib\specflow.exe generateall Specs\Specs.csproj /verbose

..\lib\nunit-console.exe Specs\bin\debug\Rescue.Specs.dll /xml=results.xml

..\lib\specflow.exe nunitexecutionreport Specs\Specs.csproj /xmlTestResult:results.xml /out:specs.html