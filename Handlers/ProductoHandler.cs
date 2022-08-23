using SQL.Tablas;
using System.Data;
using System.Data.SqlClient;

namespace SQL.Handlers
{
    public class ProductoHandler : DbHandler
    {
        public List<Producto> GetProducto(int idUsuario)
        {
            List<Producto> productoSelected = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string querySelect = "SELECT * FROM Producto WHERE IdUsuario = @idUsuario";

                SqlParameter idProductoParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = idUsuario };
                          
                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idProductoParameter);

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

                                productoSelected.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return productoSelected;
        }



        public List<string> GetTodasLasDescripcionesConDataAdapater()
        {
            List<string> descripciones = new List<string>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string querySelect = "SELECT * FROM Producto";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(querySelect, sqlConnection);

                sqlConnection.Open();

                DataSet resultado = new DataSet();
                sqlDataAdapter.Fill(resultado);

                sqlConnection.Close();
            }
            return descripciones;
        }





        public List<string> GetTodasLasDescripcionesConDataReader()
        {
            List<string> descripciones = new List<string>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string querySelect = "SELECT * FROM Producto";

                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                descripciones.Add(dataReader.GetString(1));
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return descripciones;
        }
    }
}

