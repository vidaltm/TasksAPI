using Microsoft.EntityFrameworkCore;
using TasksAPI.Data;
using TasksAPI.Models;
using TasksAPI.Repositories.Interfaces;

namespace TasksAPI.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        AppDbContext _context;
        private readonly ITarefaRepository _tarefaRepository;
        public ProjetoRepository(AppDbContext context, ITarefaRepository tarefaRepository)
        {
            this._context = context;
            _tarefaRepository = tarefaRepository;

        }
        public async Task<List<Projeto>> GetAllAsync()
        {
            var projetos = await _context.Projetos.Include(x => x.Tarefas).AsNoTracking().ToListAsync();            
            return projetos;
        }

        public async Task<Projeto> GetByIdAsync(int id)
        {
            var projeto = await _context.Projetos.Include(x => x.Tarefas).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return projeto;
        }

        public async Task PostAsync(Projeto projeto)
        {
            await _context.Projetos.AddAsync(projeto);
            await _context.SaveChangesAsync();
        }

        public async Task PutAsync(Projeto projeto)
        {
            _context.Projetos.Update(projeto);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Projeto projeto)
        {
            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();
        }
    }
}
