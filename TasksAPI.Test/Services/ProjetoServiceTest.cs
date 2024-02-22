using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAPI.Models;
using TasksAPI.Repositories.Interfaces;
using TasksAPI.Services;

namespace TasksAPI.Test.Services
{
    [TestFixture]
    public class ProjetoServiceTest
    {
        private Mock<ITarefaRepository> tarefaRepositoryMock;
        private Mock<IProjetoRepository> projetoRepositoryMock;
        private Mock<IUsuarioRepository> usuarioRepositoryMock;
        public void Setup()
        {
            tarefaRepositoryMock = new Mock<ITarefaRepository>();
            projetoRepositoryMock = new Mock<IProjetoRepository>();
            usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        }
        [Test]
        public async Task GetAll_ReturnListOfProjetos()
        {
            Setup();
            // Arrange
            var projetosMock = new List<Projeto>
            {
                new Projeto { Id = 1, NomeProjeto = "Projeto 1", Usuario = "Admin" },
                new Projeto { Id = 2, NomeProjeto = "Projeto 2", Usuario = "Admin" },
            };

            projetoRepositoryMock.Setup(repo => repo.GetAllAsync())
                                .ReturnsAsync(projetosMock);

            var projetoService = new ProjetoService(projetoRepositoryMock.Object, usuarioRepositoryMock.Object, tarefaRepositoryMock.Object);

            // Act
            var result = await projetoService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Projeto>>(result);
            CollectionAssert.AreEqual(projetosMock, result);
        }
        [Test]
        public async Task GetById_ReturnProjetoById()
        {
            Setup();
            // Arrange
            int projetoId = 1;
            var projetoMock = new Projeto { Id = projetoId, NomeProjeto = "Projeto de Exemplo", Usuario = "Admin" };

            projetoRepositoryMock.Setup(repo => repo.GetByIdAsync(projetoId))
                                .ReturnsAsync(projetoMock);

            var projetoService = new ProjetoService(projetoRepositoryMock.Object, usuarioRepositoryMock.Object, tarefaRepositoryMock.Object);

            // Act
            var result = await projetoService.GetById(projetoId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Projeto>(result);
            Assert.AreEqual(projetoMock.Id, result.Id);
            Assert.AreEqual(projetoMock.NomeProjeto, result.NomeProjeto);
        }
    }
}
