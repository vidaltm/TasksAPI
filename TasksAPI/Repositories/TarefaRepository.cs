using Microsoft.EntityFrameworkCore;
using TasksAPI.Data;
using TasksAPI.Models;
using TasksAPI.Repositories.Interfaces;

namespace TasksAPI.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        AppDbContext _context;
        public TarefaRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<List<Tarefa>> GetTarefasByProjectAsync(int projectId)
        {
            var tarefas = await _context.Tarefas.Include(x => x.Comentario).AsNoTracking().Where(x => x.ProjetoId == projectId).ToListAsync();
            return tarefas;
        }
        public async Task PostAsync(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
        }
        public async Task PutAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Tarefa tarefa)
        {
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
