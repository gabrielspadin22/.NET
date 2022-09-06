
namespace API.Controllers.DTOS.Venta
{
    public class GetVentas
    {
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public int Stock { get; set; }
        public string Descripciones { get; set; }
        public double PrecioVenta { get; set; }

    }
}
