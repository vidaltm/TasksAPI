using Microsoft.EntityFrameworkCore;
using Moq;
using TasksAPI.Data;
using TasksAPI.Models;
using TasksAPI.Repositories;
using TasksAPI.Repositories.Interfaces;
using TasksAPI.Services;

namespace TasksAPI.Test.Services
{
    [TestFixture]
    public class TarefaServiceTests
    {
        private Mock<ITarefaRepository> tarefaRepositoryMock;
        private Mock<IProjetoRepository> projetoRepositoryMock;
        private Mock<IUsuarioRepository> usuarioRepositoryMock;
        private Mock<IHistoricoTarefaRepository> historicoRepositoryMock;
        private void Setup()
        {
            tarefaRepositoryMock = new Mock<ITarefaRepository>();
            projetoRepositoryMock = new Mock<IProjetoRepository>();
            usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            historicoRepositoryMock = new Mock<IHistoricoTarefaRepository>();
        }
        private Tarefa PreencheModel()
        {
            var tarefa = new Tarefa
            {
                Id = 3,
                NomeTarefa = "Tarefa 3",
                DescricaoTarefa = "Descricao Tarefa 3",
                Prioridade = Models.Enums.Prioridades.Baixa,
                Status = Models.Enums.StatusTarefa.Pendente,
                ProjetoId = 1,
                Usuario = "Admin"
            };

            return tarefa;
        }
        [Test]
        public async Task GetTarefasByProject_ReturnListOfTarefasSuccess()
        {
            Setup();
            // Arrange
            int projectId = 1;           
            
            var tarefaService = new TarefaServices(tarefaRepositoryMock.Object, usuarioRepositoryMock.Object, projetoRepositoryMock.Object, historicoRepositoryMock.Object);

            var expectedTarefas = new List<Tarefa>
            {
                new Tarefa 
                { 
                    Id = 1, 
                    NomeTarefa = "Tarefa 1", 
                    DescricaoTarefa = "Descricao Tarefa 1", 
                    Prioridade = Models.Enums.Prioridades.Baixa, 
                    Status = Models.Enums.StatusTarefa.Pendente, 
                    ProjetoId = projectId, 
                    Usuario = "Admin" 
                },
                new Tarefa 
                {
                    Id = 2, 
                    NomeTarefa = "Tarefa 2",
                    DescricaoTarefa = "Descrição tarefa 2",
                    Prioridade = Models.Enums.Prioridades.Media,
                    Status = Models.Enums.StatusTarefa.EmAndamento,
                    ProjetoId = projectId,
                    Usuario = "Admin"
                },
            };

            tarefaRepositoryMock.Setup(repo => repo.GetTarefasByProjectAsync(projectId))
                                .ReturnsAsync(expectedTarefas);

            // Act
            var result = await tarefaService.GetTarefasByProject(projectId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Tarefa>>(result);
            CollectionAssert.AreEqual(expectedTarefas, result);
        }

        [Test]
        public async Task PostAsync_AddTarefaToContextSuccess()
        {
            Setup();

            // Arrange
            var tarefa = PreencheModel();

            usuarioRepositoryMock.Setup(repo => repo.Get(tarefa.Usuario)).Returns(true);

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new AppDbContext(dbContextOptions))
            {
                var tarefaRepository = new TarefaRepository(context);                

                // Adicionar a entidade ao contexto
                context.Tarefas.Add(tarefa);

                // Persistir as alterações no banco de dados
                await context.SaveChangesAsync();

                var tarefaService = new TarefaServices(tarefaRepositoryMock.Object, usuarioRepositoryMock.Object, projetoRepositoryMock.Object, historicoRepositoryMock.Object);
                // Act
                await tarefaService.Post(tarefa);                

                // Assert
                var addedTarefa = await context.Tarefas.FirstOrDefaultAsync();
                Assert.IsNotNull(addedTarefa);
                Assert.AreEqual(tarefa.Id, addedTarefa.Id);
                Assert.AreEqual(tarefa.NomeTarefa, addedTarefa.NomeTarefa);                
            }
        }
        [Test]
        public async Task PutAsync_UpdateTarefaInContextSuccess()
        {
            Setup();

            // Arrange
            var tarefa = PreencheModel();

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(dbContextOptions))
            {
                context.Tarefas.Update(tarefa);
                context.SaveChanges();

                var tarefaService = new TarefaServices(tarefaRepositoryMock.Object, usuarioRepositoryMock.Object, projetoRepositoryMock.Object, historicoRepositoryMock.Object);

                // Atualizar a Tarefa
                tarefa.NomeTarefa = "Tarefa Atualizada";

                // Act
                await tarefaService.Put(tarefa);

                // Assert
                var updatedTarefa = await context.Tarefas.FindAsync(tarefa.Id);
                Assert.IsNotNull(updatedTarefa);
                Assert.AreEqual("Tarefa Atualizada", updatedTarefa.NomeTarefa);
            }
        }
        [Test]
        public async Task DeleteAsync_RemoveTarefaFromContextSuccess()
        {
            Setup();
            // Arrange                        
            var tarefa = PreencheModel();

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(dbContextOptions))
            {
                context.Tarefas.Add(tarefa);
                context.SaveChanges();                

                var tarefaService = new TarefaServices(tarefaRepositoryMock.Object, usuarioRepositoryMock.Object, projetoRepositoryMock.Object, historicoRepositoryMock.Object);

                // Act
                await tarefaService.Delete(tarefa);
                context.Tarefas.Remove(tarefa);
                context.SaveChanges();

                // Assert
                var deletedTarefa = await context.Tarefas.FindAsync(tarefa.Id);
                Assert.IsNull(deletedTarefa);
            }
        }
    }
}
