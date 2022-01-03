node {
  stage('SCM') {
    checkout scm
  }
  stage('SonarQube Analysis') {
    def msbuildHome = tool 'Default MSBuild'
    def scannerHome = tool 'SonarScanner for MSBuild'
    withSonarQubeEnv() {
      bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" begin /k:\"Demo\""
      bat "\"${msbuildHome}\\MSBuild.exe\" /t:Rebuild"
      bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" end"
    }
  }
  stage ('Generating Software Bill of Materials') {
    //bat "dotnet tool install --global CycloneDX"
    bat "dotnet CycloneDX Othello.sln -o ."
  }
  stage('dependencyTrackPublisher') {
    try {
        dependencyTrackPublisher artifact: 'bom.xml', projectId: 'a65ea72b-5b77-40c5-8b19-fb83525f40eb', synchronous: true
    } catch (e) {
        echo 'failed'
    }
  }
}
