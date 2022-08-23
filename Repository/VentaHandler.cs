using API.Model;
using System.Data;
using System.Data.SqlClient;

namespace API.Repository
{
    public class VentaHandler
    {
        public const string ConnectionString = "Server=GGPC;Database=SistemaGestion;Trusted_Connection=True";
        // Mostrar todas las ventas -----------------------------
        public static List<Venta> GetVentas()
        {
            List<Venta> resultados = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();

                                resultados.Add(venta);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return resultados;
        }
        // Crear nueva venta ------------------------------------
        public static bool CrearVenta(Venta venta, ProductoVendido productoVendido)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string queryInsert = "INSERT INTO Venta (Comentarios) VALUES (@comentarios);" +
                    "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES (@stock, @idProducto, @idVenta)";

                SqlParameter comentariosParameter = new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios };
                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = productoVendido.Stock };
                SqlParameter idProductoParameter = new SqlParameter("idProducto", SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                SqlParameter idVentaParameter = new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);
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
