# Fase 1: API Coding
Para executar o projeto via docker é necessario fazer o download do mesmo ir na raiz do projeto TasksAPI e executar o seguinte comando:
- docker build . -f .\TasksAPI\Dockerfile -t tasksapi
E logo depois da imagem criada executar o comando para executar o projeto:
- docker run -d -p 7200:80 tasksapi
E por fim no navegador colocar essa url para carregar o projeto pelo swagger:
- http://localhost:7200/swagger/index.html
