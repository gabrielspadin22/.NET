using System.Data.SqlClient;
using System.Data;
using SQL.Tablas;

namespace SQL.Handlers
{
    public class VentaHandler : DbHandler
    {
        public Venta GetVenta(int idUsuario)
        {
            Venta ventaSelected = new Venta();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string querySelect = "SELECT distinct Venta.Id, Venta.Comentarios FROM Venta INNER JOIN ProductoVendido ON Venta.Id = ProductoVendido.IdVenta INNER JOIN Producto ON ProductoVendido.IdProducto = Producto.Id INNER JOIN Usuario ON Producto.IdUsuario = Usuario.Id WHERE IdUsuario = @IdUsuario";

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
                                ventaSelected.Id = Convert.ToInt32(dataReader["Id"]);
                                ventaSelected.Comentarios = dataReader["Comentarios"].ToString();
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            Console.WriteLine(ventaSelected.Id);
            return ventaSelected;
        }
    }
}
