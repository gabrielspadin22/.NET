using API.Controllers.DTOS.Venta;
using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class VentaController
    {
        // Mostrar todas las ventas de UN usuario ----------------------------
        [HttpPost("~/MostrarVentasUsuario")]
        public List<GetVentas> GetVentasUsuario([FromBody] GetVentas venta)
        {
            return VentaHandler.GetVentasUsuario(new GetVentas
            {
                IdUsuario = venta.IdUsuario
            });
        }
        // Mostrar TODAS las ventas ----------------------------
        [HttpGet("~/MostrarTODASLasVentas")]
        public List<GetVentas> GetVentas()
        {
            return VentaHandler.GetVentas();
        }
        // Crear venta -----------------------------------------
        [HttpPost("~/CrearVenta")]
        public bool CrearVenta([FromBody] PostVenta venta)
        {
            return VentaHandler.CrearVenta(new PostVenta
            {
                Id = venta.Id,
                Comentarios = venta.Comentarios,
                ProductosVendidos = venta.ProductosVendidos,
            });
        }
        // Eliminar venta -----------------------------------------
        [HttpDelete("~/EliminarVenta")]
        public bool EliminarVenta([FromBody] DeleteVenta venta)
        {
            return VentaHandler.EliminarVenta(new DeleteVenta
            {
                Id = venta.Id
            });
        }
    }
}
