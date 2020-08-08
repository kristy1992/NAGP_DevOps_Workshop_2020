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
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" publish -c release -o /app --no-restore' 
                }
            }
			echo "${workspace}"
      }
   }
 }
}


