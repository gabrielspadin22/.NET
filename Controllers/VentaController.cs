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
        // Mostrar todas las ventas ----------------------------
        [HttpGet(Name = "GetVentas")]
        public List<Venta> GetVentas()
        {
            return VentaHandler.GetVentas();
        }
        // Crear venta -----------------------------------------
        [HttpPost(Name = "PostVenta")]
        public bool CrearVenta([FromBody] PostVenta venta)
        {
            return VentaHandler.CrearVenta(new PostVenta
            {
                Comentarios = venta.Comentarios,
                ProductosVendidos = venta.ProductosVendidos,
            });
        }
    }
}
