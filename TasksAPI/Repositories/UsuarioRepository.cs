using TasksAPI.Models;
using TasksAPI.Repositories.Interfaces;

namespace TasksAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public bool Get(string userName)
        {
            var usuarios = CarregaUsuarios();

            var usuarioExiste = usuarios.Where(x => x.Nome.ToLower() == userName.ToLower()).FirstOrDefault();
            return usuarioExiste != null ? true : false;
        }
        public bool IsGerente(string userName)
        {
            var usuarios = CarregaUsuarios();

            var usuarioGerente = usuarios.Where(x => x.Nome.ToLower() == userName.ToLower() && x.Role == "gerente").FirstOrDefault();
            return usuarioGerente != null ? true : false;
        } 

        private List<Usuarios> CarregaUsuarios()
        {
            var usuarios = new List<Usuarios>();

            usuarios.Add(new Usuarios { Id = 1, Nome = "Thiago", Role = "usuario" });
            usuarios.Add(new Usuarios { Id = 2, Nome = "Admin", Role = "gerente" });

            return usuarios;
        }
    }
}
