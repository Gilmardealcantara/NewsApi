# News API


docker-compose down -v --rmi all --remove-orphans
docker-compose up --build --abort-on-container-exit

dotnet test --logger "console;verbosity=detailed"

cd test/NewsApi.Application.Tests/
generate-coverate.sh 

cd test/NewsApi.Api.IntegrationTests/
generate-coverate.sh 

code coverate



- A Notícia deverá conter:
    - título
    - Conteúdo
    - Imagem de Thumbnail
    - Possibilidade de Curtir
    - Possibilidade de Comentar
    - Lista de Comentários (inicialmente os 10 primeiros, o resto sobre demanda)
    - Número de Curtidas
    - Número de Visualizações
    - Número de comentários
    - Author


- Deve ser possível uma listagem com o preview das notícias que deverá conter em cada item
    - título
    - Preview do Conteúdo com até 100 caracteres
    - Imagem de Thumbnail
    - Número de Comentários
    - Número de Curtidas
    - Número de Visualizações


- Deve ser possível
    - Cadastrar notícia (com título, conteúdo e imagem de thumbnails) 
    - Alterar uma notícia (Título, conteúdo e imagem de thumbnails)
    - Deletar notícia
    - Curtir notícias
    - Adicionar/Editar/Remover comentários 
