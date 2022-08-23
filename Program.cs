using SQL.Handlers;

namespace SQL
{
    public class ProbarObjetos
    {
        static void Main(string[] args)
        {
            ProductoHandler productoHandler = new ProductoHandler();
            productoHandler.GetProducto(1);
           
            UsuarioHandler usuarioHandler = new UsuarioHandler();
            usuarioHandler.GetUsuario("tcasazza");

            ProductoVendidoHandler productoVendidoHandler = new ProductoVendidoHandler();
            productoVendidoHandler.GetProductoVendido(1);

            VentaHandler ventaHandler = new VentaHandler();
            ventaHandler.GetVenta(1);
            
            InicioSesion inicioSesion = new InicioSesion();
            inicioSesion.IniciarSesion(Console.ReadLine(), Console.ReadLine());
        }
    }
}