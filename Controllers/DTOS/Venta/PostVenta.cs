using API.Model;

namespace API.Controllers.DTOS.Venta
{
    public class PostVenta
    {
        public string Comentarios{ get; set; }

        public List<ProductoVendido> ProductosVendidos { get; set; }
    }
}
