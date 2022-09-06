using API.Controllers.DTOS.Usuario;
using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        // Traer usuario ----------------------------
        [HttpPost("~/TraerUsuario")]
        public UsuarioPublico GetUsuario([FromBody] GetUsuario usuario)
        {
            return UsuarioHandler.GetUsuario(new UsuarioPublico
            {
                NombreUsuario = usuario.NombreUsuario
            });
        }
        // Crear usuario -----------------------------------------
        [HttpPost("~/CrearUsuario")]
        public string CrearUsuario([FromBody] PostUsuario usuario)
        {
            return UsuarioHandler.CrearUsuario(new Usuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Mail = usuario.Mail,
                Contraseña = usuario.Contraseña
            });
        }
        // Actualizar usuario ------------------------------------
        [HttpPut("~/ActualizarUsuario")]
        public bool ActualizarUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ActualizarUsuario(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Mail = usuario.Mail,
                Contraseña = usuario.Contraseña
            });
        }
        // BORRAR usuario ========================================
        [HttpDelete("~/EliminarUsuario")]
        public bool EliminarUsuario([FromBody] DeleteUsuario usuario)
        {
            return UsuarioHandler.EliminarUsuario(new Usuario
            {
                Id = usuario.Id,
            });
        }
        // Iniciar sesion -----------------------------------------
        [HttpPost("~/IniciarSesion")]
        public string IniciarSesion([FromBody] IniciarSesion usuario)
        {
            return UsuarioHandler.IniciarSesion(new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña
            });
        }
    }
}
