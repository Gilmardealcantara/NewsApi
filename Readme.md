# News API

## Run 
```
dotnet run -p src/NewsApi.Api/
```

## Setup Local Dev
``` sh
cd setupDevDB
docker-compose down -v --rmi all --remove-orphans
docker-compose build # once time
docker-compose up -d
# After 30s is done !!! 
# Db Info: 
# Data source: localhost,1435;
# User ID: sa
# Password: Senha@123
```

# Tests
## Run Unit Tests
``` sh
dotnet test test/NewsApi.Application.Tests/
```
 

## Run Integration Tests
``` sh
dotnet test test/NewsApi.Application.Tests/
```

## tests with detail
``` sh
dotnet test --logger "console;verbosity=detailed"
```

## generate coverage
``` sh
# unit application tests
cd test/NewsApi.Application.Tests/
generate-coverate.sh 

# api integration tests
cd test/NewsApi.Api.IntegrationTests/
generate-coverate.sh 
```



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


TODO
paginate news list
Remove HTML from content preview
Delete thumb use case
Notification Service

