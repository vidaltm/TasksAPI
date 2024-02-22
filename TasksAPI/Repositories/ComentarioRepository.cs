using TasksAPI.Data;
using TasksAPI.Models;
using TasksAPI.Repositories.Interfaces;

namespace TasksAPI.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        AppDbContext _context;
        public ComentarioRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task PostAsync(Comentarios comentarios)
        {
            await _context.Comentarios.AddAsync(comentarios);
            await _context.SaveChangesAsync();
        }
    }
}
