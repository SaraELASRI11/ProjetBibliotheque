pipeline {
    agent any

    environment {
        PATH = "/usr/bin/dotnet:$PATH"
        DOCKER_CREDENTIALS = credentials('global')
        DOCKER_IMAGE = 'saraelas/gestionbibliotheque-app'
    }

    stages {
        stage('Clone Repository') {
            steps {
                script {
                    git branch: 'main',
                        url: 'https://github.com/SaraELASRI11/ProjetBibliotheque.git'
                }
            }
        }

        stage('Debug Paths') {
            steps {
                sh 'ls -R'
            }
        }

        stage('Restore & Build') {
            steps {
                script {
                    sh 'dotnet build GestionBibliotheque.sln'
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    sh 'dotnet test $WORKSPACE/LivreService_Test/Bibliotheque_Test.csproj '
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    sh' docker build -t ${DOCKER_IMAGE}:latest -f GestionBibliotheque/Dockerfile .'                }
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
                sshagent(['globalSsh']) {
                    sh '''
                        ssh -o StrictHostKeyChecking=no sara@192.168.146.130 "
                            docker login -u '$DOCKER_CREDENTIALS_USR' -p '$DOCKER_CREDENTIALS_PSW ' &&
                            docker pull $DOCKER_IMAGE &&
                            docker stop app_container || true &&
                            docker rm app_container || true &&
                            docker run -d --name app_container --restart=always $DOCKER_IMAGE"
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
