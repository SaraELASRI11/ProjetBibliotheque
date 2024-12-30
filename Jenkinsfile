pipeline {
    agent any

    environment {
        DOTNET_PATH = '/usr/bin' // Mettez ici le chemin obtenu avec `which dotnet`
        DOCKER_CREDENTIALS = credentials('dockerhub-credentials')
        DOCKER_IMAGE = 'saraelas/gestionbibliotheque-app'
    }

    stages {
        stage('Setup Environment') {
            steps {
                script {
                    sh '''
                    export PATH=$DOTNET_PATH:$PATH
                    '''
                }
            }
        }

        stage('Clone Repository') {
            steps {
                script {
                    git branch: 'main',
                        url: 'https://github.com/SaraELASRI11/ProjetBibliotheque.git'
                }
            }
        }

        stage('Restore & Build') {
            steps {
                script {
                    sh '''
                    export PATH=$DOTNET_PATH:$PATH
                    dotnet restore GestionBibliotheque.sln
                    dotnet build GestionBibliotheque.sln --configuration Release
                    '''
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    sh '''
                    export PATH=$DOTNET_PATH:$PATH
                    dotnet test LivreService_Test/LivreService_Test.csproj --configuration Release --no-build
                    '''
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    sh "docker build -t ${DOCKER_IMAGE}:latest ."
                }
            }
        }

        stage('Push Docker Image') {
            steps {
                script {
                    sh '''
                    echo $DOCKER_CREDENTIALS_PSW | docker login -u $DOCKER_CREDENTIALS_USR --password-stdin
                    docker push ${DOCKER_IMAGE}:latest
                    '''
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    sh '''
                    docker stop gestionbibliotheque || true
                    docker rm gestionbibliotheque || true
                    docker run -d --name gestionbibliotheque -p 8080:80 ${DOCKER_IMAGE}:latest
                    '''
                }
            }
        }
    }

    post {
        always {
            echo 'Pipeline terminé.'
        }
        success {
            echo 'Pipeline exécuté avec succès.'
        }
        failure {
            echo 'Échec du pipeline.'
        }
    }
}
