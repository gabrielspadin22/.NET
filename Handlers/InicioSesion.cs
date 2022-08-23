using System.Data.SqlClient;
using System.Data;
using SQL.Tablas;

namespace SQL.Handlers
{
    internal class InicioSesion : DbHandler
    {
        public Usuario IniciarSesion(string nombreUsuario, string contraseña)
        {
            
            Usuario usuarioSelected = new Usuario();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                const string querySelect = "SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contraseña";

                SqlParameter nombreUsuarioParameter = new SqlParameter();

                nombreUsuarioParameter.ParameterName = "nombreUsuario";
                nombreUsuarioParameter.SqlDbType = SqlDbType.VarChar;
                nombreUsuarioParameter.Value = nombreUsuario;

                SqlParameter contraseñaParameter = new SqlParameter();

                contraseñaParameter.ParameterName = "contraseña";
                contraseñaParameter.SqlDbType = SqlDbType.VarChar;
                contraseñaParameter.Value = contraseña;

                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuarioSelected.Nombre = dataReader["Nombre"].ToString();
                                usuarioSelected.Apellido = dataReader["Apellido"].ToString();
                                usuarioSelected.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuarioSelected.Contraseña = dataReader["Contraseña"].ToString();
                                usuarioSelected.Mail = dataReader["Mail"].ToString();

                                Console.WriteLine("Bienvenido {0} {1}", usuarioSelected.Nombre, usuarioSelected.Apellido);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Datos incorrectos, vuelva a intentar");
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return usuarioSelected;
        }
    }
}
