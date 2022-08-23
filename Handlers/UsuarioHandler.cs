using System.Data.SqlClient;
using System.Data;
using SQL.Tablas;

namespace SQL.Handlers
{
    public class UsuarioHandler : DbHandler
    {
        public Usuario GetUsuario(string nombreUsuario)
        {
            Usuario usuarioSelected = new Usuario();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string querySelect = "SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario";

                SqlParameter idUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = nombreUsuario };

                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuarioSelected.Id = Convert.ToInt32(dataReader["Id"]);
                                usuarioSelected.Nombre = dataReader["Nombre"].ToString();
                                usuarioSelected.Apellido = dataReader["Apellido"].ToString();
                                usuarioSelected.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuarioSelected.Contraseña = dataReader["Contraseña"].ToString();
                                usuarioSelected.Mail = dataReader["Mail"].ToString();
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return usuarioSelected;
        }


        public void DeleteUsuario(int idUsuario)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = "DELETE FROM Usuario WHERE Id = @idUsuario";

                    SqlParameter parametro = new SqlParameter();

                    parametro.ParameterName = "idUsuario";
                    parametro.SqlDbType = SqlDbType.BigInt;
                    parametro.Value = idUsuario;

                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(parametro);
                        int FilasAfectadas = sqlCommand.ExecuteNonQuery(); // Se ejecuta la query y devuelve la cantidad de filas afectadas
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void UpdateContraseña(int idUsuario, string nuevaContraseña)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = "UPDATE Usuario SET Contraseña = @nuevaContraseña WHERE Id = @idUsuario";

                    SqlParameter parametroNuevaContraseña = new SqlParameter();

                    parametroNuevaContraseña.ParameterName = "nuevaContraseña";
                    parametroNuevaContraseña.SqlDbType = SqlDbType.VarChar;
                    parametroNuevaContraseña.Value = nuevaContraseña;

                    SqlParameter parametroUsuarioId = new SqlParameter();

                    parametroUsuarioId.ParameterName = "idUsuario";
                    parametroUsuarioId.SqlDbType = SqlDbType.BigInt;
                    parametroUsuarioId.Value = idUsuario;

                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(parametroUsuarioId);
                        sqlCommand.Parameters.Add(parametroNuevaContraseña);
                        int FilasAfectadas = sqlCommand.ExecuteNonQuery(); // Se ejecuta la query y devuelve la cantidad de filas afectadas
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Insert(Usuario usuario)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string queryInsert = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail);";

                SqlParameter nombreParameter = new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailUsuario = new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailUsuario);
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
    }
}
