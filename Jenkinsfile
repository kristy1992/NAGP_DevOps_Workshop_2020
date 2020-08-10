pipeline {

    agent any

    options {
        buildDiscarder(logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '10', daysToKeepStr: '', numToKeepStr: '5'))

	    disableConcurrentBuilds()
		
	    //skipDefaultCheckout()
    }

stages {
    stage ('Checkout') {
        steps{
            checkout(scm)
            //stash includes: '**', name: 'source', useDefaultExcludes: false
        }
    }
	stage('SonarQube Ananlysis Begin') {
	    steps{
	        withSonarQubeEnv('sonarqube'){
	            bat '"C:\\Program Files\\dotnet\\dotnet.exe" "C:\\Program Files (x86)\\Jenkins\\tools\\hudson.plugins.sonar.MsBuildSQRunnerInstallation\\sonarscanner\\SonarScanner.MSBuild.dll" begin /k:"DemoWebApplication" /n:"DemoWebApplication" /v:1.0'
	        }
	    }
	}
    stage ('Restore Packages') {     
        steps {
            //unstash 'source'
            script {
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" restore "DemoWebApplication\\DemoWebApplication.sln" '
            }             
        }
    }

    stage('Build') {
        steps {
            //unstash 'source'
            script{
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" build "DemoWebApplication\\DemoWebApplication.sln"'
            }
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
		
		
		
	stage('SonarQube Ananlysis End'){
		steps{
		    withSonarQubeEnv('sonarqube'){
	            bat '"C:\\Program Files\\dotnet\\dotnet.exe" "C:\\Program Files (x86)\\Jenkins\\tools\\hudson.plugins.sonar.MsBuildSQRunnerInstallation\\sonarscanner\\SonarScanner.MSBuild.dll" end'
	        }
	    }
	}

    stage('Docker Create Image'){
        steps{
            bat 'docker build -t DemoWebApplication:${BUILD_NUMBER} --no-cache -f dockerfile .'
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
    }
}

String getWorkspace(String workspace){
	return workspace.replace("/","\\")
}


