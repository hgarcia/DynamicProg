..\lib\specflow\specflow.exe generateall Specs\Specs.csproj /verbose

..\lib\NUnit\nunit-console.exe Specs\bin\debug\Rescue.Specs.dll /xml=results.xml

..\lib\specflow\specflow.exe nunitexecutionreport Specs\Specs.csproj /xmlTestResult:results.xml /out:specs.html

..\lib\specflow\specflow.exe stepdefinitionreport Specs\Specs.csproj 
/out:specsSteps.html