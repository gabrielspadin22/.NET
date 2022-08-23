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
        // Mostrar todos los usuarios ----------------------------
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();
        }
        // Crear usuario -----------------------------------------
        [HttpPost(Name = "PostUsuario")]
        public bool CrearUsuario([FromBody] PostUsuario usuario)
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
        [HttpPut(Name = "PutUsuario")]
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
        [HttpDelete(Name = "DeleteUsuario")]
        public bool EliminarUsuario([FromBody] DeleteUsuario usuario)
        {
            return UsuarioHandler.EliminarUsuario(new Usuario
            {
                Id = usuario.Id,
            });
        }

    }
}
