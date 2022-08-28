/*using API.Model;
using System.Data;
using System.Data.SqlClient;

namespace API.Repository
{
    public class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=GGPC;Database=SistemaGestion;Trusted_Connection=True";
        // Crear nuevo productoVendido ------------------------------------
        public static bool CrearProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string queryInsert = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES (@stock, @idProducto, @idVenta);";

                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = productoVendido.Stock };
                SqlParameter idProductoParameter = new SqlParameter("idProducto", SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                SqlParameter idVentaParameter = new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductoParameter);
                    sqlCommand.Parameters.Add(idVentaParameter);

                    int rows = sqlCommand.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }
    }
}
*/