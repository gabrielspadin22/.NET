using API.Controllers.DTOS.Venta;
using API.Model;
using System.Data;
using System.Data.SqlClient;

namespace API.Repository
{
    public class VentaHandler
    {
        
        public const string ConnectionString = "Server=GGPC;Database=SistemaGestion;Trusted_Connection=True";
        // Mostrar todas las ventas de un usuario -----------------------------
        public static List<GetVentas> GetVentasUsuario(GetVentas venta)
        {
            List<GetVentas> resultado = new List<GetVentas>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "SELECT pv.IdVenta, pv.Stock, pv.IdProducto, p.Descripciones, p.IdUsuario, p.PrecioVenta FROM ProductoVendido pv INNER JOIN Producto p ON pv.IdProducto = p.Id WHERE p.IdUsuario = @idUsuario;";

                SqlParameter idParameter = new SqlParameter("idUsuario", SqlDbType.VarChar) { Value = venta.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                GetVentas ventaCompleta = new GetVentas();

                                ventaCompleta.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                ventaCompleta.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                ventaCompleta.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
                                ventaCompleta.Stock = Convert.ToInt32(dataReader["Stock"]);
                                ventaCompleta.Descripciones = dataReader["Descripciones"].ToString();
                                ventaCompleta.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);

                                resultado.Add(ventaCompleta);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }
        // Mostrar TODAS las ventas -----------------------------
        public static List<GetVentas> GetVentas()
        {
            List<GetVentas> resultado = new List<GetVentas>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT pv.IdVenta, pv.Stock, pv.IdProducto, p.Descripciones, p.IdUsuario, p.PrecioVenta FROM ProductoVendido pv INNER JOIN Producto p ON pv.IdProducto = p.Id;", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                GetVentas ventaCompleta = new GetVentas();

                                ventaCompleta.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                ventaCompleta.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                ventaCompleta.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
                                ventaCompleta.Stock = Convert.ToInt32(dataReader["Stock"]);
                                ventaCompleta.Descripciones = dataReader["Descripciones"].ToString();
                                ventaCompleta.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);

                                resultado.Add(ventaCompleta);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return resultado;
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
        // Eliminar venta ------------------------------------
        public static bool EliminarVenta(DeleteVenta venta)
        {
            bool resultado = false;

            List<ProductoVendido> LPV = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string queryInsert = "SELECT Id, Stock FROM ProductoVendido WHERE IdVenta = @idVenta; DELETE FROM ProductoVendido WHERE IdVenta = @idVenta; DELETE FROM Venta WHERE Id = @idVenta";

                SqlParameter comentariosParameter = new SqlParameter("@idVenta", SqlDbType.BigInt) { Value = venta.Id };

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
                                ProductoVendido PV = new ProductoVendido();
                                PV.IdProducto = Convert.ToInt32(dataReader["Id"]);
                                PV.Stock = Convert.ToInt32(dataReader["Stock"]);

                                LPV.Add(PV);
                            }
                        }
                    }
                }
                foreach (var item in LPV)
                {
                    const string queryInsertProductos = "UPDATE Producto SET Stock = Stock + @stockPV;";

                    SqlParameter stockParameter = new SqlParameter("stockPV", SqlDbType.BigInt) { Value = item.Stock };

                    using (SqlCommand sqlCommand2 = new SqlCommand(queryInsertProductos, sqlConnection))
                    {
                        sqlCommand2.Parameters.Add(stockParameter);

                        using (SqlDataReader dataReader = sqlCommand2.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    
                                    sqlCommand2.ExecuteNonQuery();
                                }
                                
                                Console.WriteLine("Se elimino la venta");
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
