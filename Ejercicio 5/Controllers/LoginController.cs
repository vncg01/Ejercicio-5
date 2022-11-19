﻿using Datos.Interfaces;
using Datos.Repositorios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Security.Claims;

namespace Ejercicio_5.Controllers
{
    public class LoginController : Controller
    {
        private readonly Config _configuracion;
        private ILoginRepositorio _loginRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(Config config)
        {
            _configuracion = config;
            _loginRepositorio = new LoginRepositorio(config.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(Login login)
        {
            string rol = string.Empty;

            try
            {
                bool usuarioValido = await _loginRepositorio.ValidarUsuario(login);
                if (usuarioValido)
                {
                    Usuario user = await _usuarioRepositorio.GetPorCodigo(login.Usuario);
                    if (user.EstaActivo)
                    {
                        rol = user.ROL;
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, user.Codigo),
                            new Claim(ClaimTypes.Role, rol)
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddMinutes(5) });
                    }
                    else
                    {
                        return LocalRedirect("/login/El usuario no esta activo");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
