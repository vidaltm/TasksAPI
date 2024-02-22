using Microsoft.EntityFrameworkCore;
using TasksAPI.Models;

namespace TasksAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<HistoricoTarefa> HistoricoTarefas { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }        
    }
}
