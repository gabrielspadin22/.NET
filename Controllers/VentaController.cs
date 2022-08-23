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
        public bool CrearVenta([FromBody] PostVenta venta, ProductoVendido productosVendidos)
        {
            return VentaHandler.CrearVenta(new Venta
            {
                Comentarios = venta.Comentarios
            },
            new ProductoVendido
            {
                Stock = productosVendidos.Stock,
                IdProducto = productosVendidos.IdProducto,
                IdVenta = productosVendidos.IdVenta
            });
        }
    }
}
