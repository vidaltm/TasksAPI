using Microsoft.EntityFrameworkCore;
using TasksAPI.Data;
using TasksAPI.Models;
using TasksAPI.Repositories.Interfaces;

namespace TasksAPI.Repositories
{
    public class HistoricoTarefaRepository : IHistoricoTarefaRepository
    {
        AppDbContext _context;
        public HistoricoTarefaRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<List<HistoricoTarefa>> GetHistoricoByUsuarioAsync(string usuario)
        {
            var tarefas = await _context.HistoricoTarefas.AsNoTracking().ToListAsync();
            return tarefas;
        }
        public async Task PostAsync(HistoricoTarefa historico)
        {
            await _context.HistoricoTarefas.AddAsync(historico);
            await _context.SaveChangesAsync();
        }
    }
}
