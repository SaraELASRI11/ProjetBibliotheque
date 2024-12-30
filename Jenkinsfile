pipeline {
    agent any

    environment {
        DOCKER_CREDENTIALS = credentials('global') // ID des credentials Docker Hub
        DOCKER_IMAGE = 'saraelas/gestionbibliotheque-app'
    }

    stages {
        stage('Clone Repository') {
            steps {
                script {
                    // Cloner le dépôt GitHub sans credentials (car public)
                    git branch: 'main',
                        url: 'https://github.com/SaraELASRI11/ProjetBibliotheque.git'
                }
            }
        }

        stage('Restore & Build') {
            steps {
                script {
                    sh 'dotnet restore GestionBibliotheque.sln'
                    sh 'dotnet build GestionBibliotheque.sln --configuration Release'
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    sh 'dotnet test LivreService_Test/LivreService_Test.csproj --configuration Release --no-build'
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
