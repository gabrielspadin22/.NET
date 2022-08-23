using System.Data.SqlClient;
using System.Data;
using SQL.Tablas;
namespace SQL.Handlers
{
    public class ProductoVendidoHandler : DbHandler
    {
        public List<Producto> GetProductoVendido(int idUsuario)
        {
            List<Producto> productosVendidoSelected = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string querySelect = "SELECT Producto.Descripciones, Producto.PrecioVenta FROM Producto INNER JOIN ProductoVendido ON ProductoVendido.IdProducto = Producto.Id INNER JOIN Usuario ON Producto.IdUsuario = Usuario.Id WHERE Usuario.Id = @idUsuario";

                SqlParameter idVentaParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = idUsuario };

                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idVentaParameter);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto productoSelected = new Producto();

                                productoSelected.Descripciones = dataReader["Descripciones"].ToString();
                                productoSelected.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                
                                productosVendidoSelected.Add(productoSelected);
                            }
                        }
                        sqlConnection.Close();
                    }
                }
                return productosVendidoSelected;
            }
        }
    }
}