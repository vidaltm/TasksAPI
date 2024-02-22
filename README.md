# Fase 1: API Coding
Para executar o projeto via docker é necessario fazer o download do mesmo ir na raiz do projeto TasksAPI e executar o seguinte comando:
- docker build . -f .\TasksAPI\Dockerfile -t tasksapi

</br>E logo depois da imagem criada executar o comando para executar o projeto:
- docker run -d -p 7200:80 tasksapi

</br>E por fim no navegador colocar essa url para carregar o projeto pelo swagger:
- http://localhost:7200/swagger/index.html </br>

Foi gerado um banco de dados do tipo SQLite que vai subir com a aplicação, é um banco que esta local no projeto, chamado project.db. </br>
Na subida do projeto ja vai levantar alguns dados. </br>
Na questão de usuarios eu somente criar um metodo que preeche alguns usuarios em um model, nesse model contem o usuario "Thiago" com permissões de "usuario" e o usuario "Admin" com permissões de "gerente", usei os 2 para os testes iniciais. </br>

# Fase 2: Refinamento  
- Qual funcionalidade é considerada mais crítica ou prioritária para os usuários?
- Há planos para incluir funcionalidades adicionais no futuro? Se sim, quais são elas?
- Existe uma visão de longo prazo para a API? Algumas funcionalidades que poderiam ser planejadas para versões futuras?
- Quão escalavel precisa ser essa API?
- Ela vai ser usada por um microserviço?
- Futuramente essa API vai ter novas funções?
- A API vai ser usada somente internamente ou também externamente?

# Fase 3: Final
- Incluiria validação e login para os usuarios usando um JWT inicialmente, pois da maneira que está não tem segurança nenhuma. </br>
- Poderia usar um design pattern Facade para simplificar as interfaces usadas
- Decidia com a empresa qual ferramenta em nuvem usariamos, AWS, Azure, GCP
- Transformaria o mesmo em microserviços para ter mais escalabilidade dependendo da quantidade de acessos a essa API
- Incluiria deviado ao microserviços um RabbitMQ para gerenciar as mensagens
- Poderia mudar a forma que esta construida a api para usar uma api via gRPC
