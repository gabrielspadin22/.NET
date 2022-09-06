using API.Controllers.DTOS.Producto;
using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductoController : ControllerBase
    {
        // Mostrar todos los productos ----------------------------
        [HttpGet("~/MostrarProductos")]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }
        // Mostrar todos los productos vendidos ----------------------------
        [HttpPost("~/MostrarProductosVendidos")]
        public List<ProductoVendido> GetProductosVendidos([FromBody] int Id)
        {
            return ProductoHandler.GetProductosVendidos(Id);
        }
        // Crear producto -----------------------------------------
        [HttpPost("~/CrearProducto")]
        public bool CrearProducto([FromBody] PostProducto producto)
        {
            return ProductoHandler.CrearProducto(new Producto
            {
                Descripciones = producto.Descripciones,
                Costo = producto.Costo,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                IdUsuario = producto.IdUsuario
            });
        }
        // Actualizar producto ------------------------------------
        [HttpPut("~/ActualizarProducto")]
        public bool ActualizarProducto([FromBody] PutProducto producto)
        {
            return ProductoHandler.ActualizarProducto(new Producto
            {
                Id = producto.Id,
                Descripciones = producto.Descripciones,
                Costo = producto.Costo,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                IdUsuario = producto.IdUsuario
            });
        }
        // BORRAR producto ========================================
        [HttpDelete("~/BorrarProducto")]
        public bool EliminarProducto([FromBody] DeleteProducto producto)
        {
            return ProductoHandler.EliminarProducto(new Producto
            {
                Id = producto.Id,
            });
        }
    }
}
