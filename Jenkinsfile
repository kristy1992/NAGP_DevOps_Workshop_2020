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
	bat '"dotnet-sonarscanner" begin /k:"jenkins-demo-project"'
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
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" build "DemoWebApplication\\DemoWebApplication.sln"'
                }
			echo "${workspace}"
      }
   }
   
   stage('Run MS Test') {
			steps {
			script {
					
					workspace = getWorkspace(pwd())
				}
				
				bat '"C:\\Program Files\\opencover.4.7.922\\OpenCover.Console.exe" -register -target:"C:\\Program Files\\dotnet\\dotnet.exe" -targetargs:"test --no-build --logger:trx"  -output:test_result_coverage.xml'
				bat '"C:\\Users\\kristy\\.nuget\\packages\\reportgenerator\\4.5.8\\tools\\netcoreapp3.0\\ReportGenerator.exe\" -reports:test_result_coverage.xml -targetdir:${workspace}\\TestResults'
			}
		}
		
		stage('Publish test results'){
			steps{
				publishHTML([
				  allowMissing: false,
				  alwaysLinkToLastBuild: false,
				  keepAll: true,
				  reportDir: "TestResults\\",
				  reportFiles: 'index.htm',
				  reportName: 'Open Cover Report',
				  reportTitles: ''
				])
			}
		}
		
		stage('SonarQube Ananlysis End'){
		steps{
	bat '"dotnet-sonarscanner" end /k:"jenkins-demo-project"'
	}
	}
 }
}

String getWorkspace(String workspace){
	return workspace.replace("/","\\")
}


