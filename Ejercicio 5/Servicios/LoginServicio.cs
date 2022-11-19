using Datos.Interfaces;
using Datos.Repositorios;
using Ejercicio_5.Interfaces;
using Modelos;

namespace Ejercicio_5.Servicios
{
    public class LoginServicio : ILoginServicio
    {
        private readonly Config _configuracion;
        private ILoginRepositorio loginRepositorio;

        public LoginServicio(Config config)
        {
            _configuracion = config;
            loginRepositorio = new LoginRepositorio(config.CadenaConexion);
        }

        public async Task<bool> ValidarUsuario(Login login)
        {
            return await loginRepositorio.ValidarUsuario(login);
        }
    }
}
