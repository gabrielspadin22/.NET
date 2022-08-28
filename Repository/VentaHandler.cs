using API.Controllers.DTOS.Venta;
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
        public static bool CrearVenta(PostVenta venta)
        {
            bool resultado = false;
            int Id = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string queryInsert = "INSERT INTO Venta (Comentarios) VALUES (@comentarios); SELECT Id FROM Venta WHERE Comentarios = @comentarios";
               
                SqlParameter comentariosParameter = new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Id = Convert.ToInt32(dataReader["Id"]);
                            }
                        }
                    }
                }
                foreach (var item in venta.ProductosVendidos)
                {
                    const string queryInsertProductos = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES (@stock, @idProducto, @idVenta); UPDATE Producto SET Stock = Stock - @stock WHERE Id = @idProducto; SELECT Stock FROM Producto WHERE Id = @idProducto";

                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = item.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("idProducto", SqlDbType.BigInt) { Value = item.IdProducto };
                    SqlParameter idVentaParameter = new SqlParameter("idVenta", SqlDbType.BigInt) { Value = Id };

                    using (SqlCommand sqlCommand2 = new SqlCommand(queryInsertProductos, sqlConnection))
                    {
                        sqlCommand2.Parameters.Add(stockParameter);
                        sqlCommand2.Parameters.Add(idProductoParameter);
                        sqlCommand2.Parameters.Add(idVentaParameter);

                        using (SqlDataReader dataReader = sqlCommand2.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int stock = Convert.ToInt32(dataReader["Stock"]);
                                    if (item.Stock > stock)
                                    {
                                        Console.WriteLine("Stock Superado");
                                    }
                                }
                            }
                        }
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }
    }
}
