pipeline {
    agent any

    environment {
        DOCKERHUB_CREDENTIALS = credentials('global') // ID des credentials Docker Hub dans Jenkins
        DOCKER_IMAGE = 'saraelas/gestionbibliotheque-app'
    }

    stages {
        stage('Setup SSH Known Hosts') {
            steps {
                script {
                    // Ajouter les clés hôtes de GitHub au fichier known_hosts
                    sh '''
                    mkdir -p ~/.ssh
                    ssh-keyscan -t rsa,ed25519 github.com >> ~/.ssh/known_hosts
                    chmod 644 ~/.ssh/known_hosts
                    '''
                }
            }
        }

        stage('Clone Repository') {
            steps {
                script {
                    // Cloner le dépôt GitHub en SSH en utilisant les credentials
                    git branch: 'main', credentialsId: 'git-ssh', url: 'git@github.com:SaraELASRI11/ProjetBibliotheque.git'
                }
            }
        }

        stage('Restore & Build') {
            steps {
                script {
                    // Restaurer les dépendances et construire le projet
                    sh 'dotnet restore GestionBibliotheque.sln'
                    sh 'dotnet build GestionBibliotheque.sln --configuration Release'
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    // Exécuter les tests unitaires depuis LivreService_Test
                    sh 'dotnet test LivreService_Test/LivreService_Test.csproj --configuration Release --no-build'
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    // Construire une image Docker à partir du Dockerfile
                    sh "docker build -t ${DOCKER_IMAGE}:latest ."
                }
            }
        }

        stage('Push Docker Image') {
            steps {
                script {
                    // Pousser l'image Docker vers Docker Hub
                    sh '''
                    echo $DOCKERHUB_CREDENTIALS_PSW | docker login -u $DOCKERHUB_CREDENTIALS_USR --password-stdin
                    docker push ${DOCKER_IMAGE}:latest
                    '''
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    // Déployer l'image Docker en exécutant un conteneur
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
