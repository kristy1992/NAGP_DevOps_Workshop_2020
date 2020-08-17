pipeline {

    agent any

    options {
        buildDiscarder(logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '10', daysToKeepStr: '', numToKeepStr: '5'))

	    disableConcurrentBuilds()
		
	    skipDefaultCheckout()
    }

stages {
    stage ('Checkout') {
        steps{
            checkout(scm)
            stash includes: '**', name: 'source', useDefaultExcludes: false
        }
		//post { cleanup { deleteDir() } }
    }
	
	
    stage ('Restore Packages') {     
        steps {
            unstash 'source'
            script {
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" restore "DemoWebApplication\\DemoWebApplication.sln" '
            }             
        }
    }

    }
}



