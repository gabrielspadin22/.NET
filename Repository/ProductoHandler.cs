using API.Model;
using System.Data;
using System.Data.SqlClient;

namespace API.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=GGPC;Database=SistemaGestion;Trusted_Connection=True";
        // Mostrar todos los productos -----------------------------
        public static List<Producto> GetProductos()
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

                                resultados.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return resultados;
        }
        // Crear nuevo producto ------------------------------------
        public static bool CrearProducto(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string queryInsert = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario);";

                SqlParameter descripcionesParameter = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costo", SqlDbType.Float) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVenta", SqlDbType.Float) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

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
        // Actualizar producto -------------------------------------
        public static bool ActualizarProducto(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "UPDATE Producto SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario WHERE Id = @id";

                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = producto.Id };
                SqlParameter descripcionesParameter = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costo", SqlDbType.Float) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVenta", SqlDbType.Float) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

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
        // ELIMINAR PRODUCTO =======================================
        public static bool EliminarProducto(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "DELETE from Producto WHERE Id = @id";

                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = producto.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);

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
