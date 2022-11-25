using Modelos;

namespace Ejercicio_5.Interfaces
{
    public interface IUsuarioServicio
    {
        Task<Usuario> GetPorCodigo(string codigo);
        Task<bool> Nuevo(Usuario usuario);
        Task<bool> Actualizar(Usuario usuario);
        Task<bool> Eliminar(string codigo);
        Task<IEnumerable<Usuario>> GetLista();
    }
}
