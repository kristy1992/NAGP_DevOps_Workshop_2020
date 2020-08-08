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
            dir('DemoWebApplication\\DemoWebApplication'){
                script{
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" publish -c release -o /app' 
                }
            }
			echo "${workspace}"
      }
   }
   
   stage('Run MS Test') {
			steps {
				
				bat '"C:\\Program Files\\opencover.4.7.922\\OpenCover.Console.exe" -register -target:"C:\\Program Files\\dotnet\\dotnet.exe" -targetargs:"test --no-build --logger:trx  -output:test_result_coverage.xml'
			}
		}
 }
}


