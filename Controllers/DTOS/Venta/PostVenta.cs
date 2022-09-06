using API.Model;

namespace API.Controllers.DTOS.Venta
{
    public class PostVenta
    {
        public int Id { get; set; }
        public string Comentarios{ get; set; }

        public List<ProductoVendido> ProductosVendidos { get; set; }
    }
}
