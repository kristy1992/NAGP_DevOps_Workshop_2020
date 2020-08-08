pipeline {
agent any
 
options {
    skipDefaultCheckout()
}

stages {
stage ('Checkout') {
        steps{
            checkout(scm)
            stash includes: '**', name: 'source', useDefaultExcludes: false
        }
    }
	stage('SonarQube Ananlysis Begin'){
	steps{
	bat '"C:\\Program Files\\dotnet\\dotnet.exe" "C:\\sonar-scanner-msbuild-4.10.0.19059-netcoreapp3.0\\SonarScanner.MSBuild.dll" begin /k:"DemoWebApplication" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="83455cb30deee681aa37f5b8d2b66ad930d7e08a"'
	}
	}
stage ('Restore Packages') {     
         steps {
             deleteDir()
             unstash 'source'
             script {
                 bat '"C:\\Program Files\\dotnet\\dotnet.exe" restore "DemoWebApplication\\DemoWebApplication.sln" '
             }             
          }
        }

stage('Build') {
     steps {
            deleteDir()
            unstash 'source'
                script{
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" mbuild "DemoWebApplication\\DemoWebApplication.sln" /t:Rebuild'
                }
			echo "${workspace}"
      }
   }
   
   stage('Run MS Test') {
			steps {
			script {
					
					workspace = getWorkspace(pwd())
				}
				
				bat '"C:\\Program Files\\opencover.4.7.922\\OpenCover.Console.exe" -register -target:"C:\\Program Files\\dotnet\\dotnet.exe" -targetargs:"test DemoWebApplication\\DemoTestProject\\bin\\Debug\\netcoreapp3.1\\DemoTestProject.dll --no-build --logger:trx"  -output:test_result_coverage.xml'
				bat '"C:\\Users\\kristy\\.nuget\\packages\\reportgenerator\\4.5.8\\tools\\netcoreapp3.0\\ReportGenerator.exe\" -reports:test_result_coverage.xml -targetdir:${workspace}\\TestResults'
			}
		}
		
		stage('Publish test results'){
			steps{
				publishHTML([
				  allowMissing: false,
				  alwaysLinkToLastBuild: false,
				  keepAll: true,
				  reportDir: "${workspace}\\TestResults\\",
				  reportFiles: 'index.htm',
				  reportName: 'Open Cover Report',
				  reportTitles: ''
				])
			}
		}
		
		stage('SonarQube Ananlysis End'){
		steps{
	bat '"C:\\Program Files\\dotnet\\dotnet.exe" "C:\\sonar-scanner-msbuild-4.10.0.19059-netcoreapp3.0\\SonarScanner.MSBuild.dll" end /d:sonar.login="83455cb30deee681aa37f5b8d2b66ad930d7e08a"'
	}
	}
 }
}

String getWorkspace(String workspace){
	return workspace.replace("/","\\")
}


