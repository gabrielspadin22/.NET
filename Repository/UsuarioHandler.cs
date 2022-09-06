using API.Model;
using System.Data;
using System.Data.SqlClient;

namespace API.Repository
{
    public static class UsuarioHandler
    {
        public const string ConnectionString = "Server=GGPC;Database=SistemaGestion;Trusted_Connection=True";
        // Mostrar todos los usuarios -----------------------------
        public static UsuarioPublico GetUsuario(UsuarioPublico usuario)
        {
            UsuarioPublico resultado = new UsuarioPublico();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario";

                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                resultado.Nombre = dataReader["Nombre"].ToString();
                                resultado.Apellido = dataReader["Apellido"].ToString();
                                resultado.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                resultado.Mail = dataReader["Mail"].ToString();
                                resultado.Contraseña = dataReader["Contraseña"].ToString();
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }
        // Crear nuevo usuario ------------------------------------
        public static string CrearUsuario(Usuario usuario)
        {
            string resultado = "Error al crear usuario";

            UsuarioPublico vof = GetUsuario(new UsuarioPublico
            {
                NombreUsuario = usuario.NombreUsuario
            });

            if (vof.NombreUsuario != usuario.NombreUsuario)
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

                        int rows = sqlCommand.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            resultado = "Nuevo usuario creado con exito";
                        }
                        resultado = resultado;
                    }
                    sqlConnection.Close();
                }
            }
            else
            {
                resultado = "El nombre de usuario ya se encuentra en uso";
            }
            return resultado;
        }
        // Actualizar usuario -------------------------------------
        public static bool ActualizarUsuario(Usuario usuario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail WHERE Id = @id";

                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Id };
                SqlParameter nombreParameter = new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParameter = new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Mail };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);

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
        // ELIMINAR USUARIO =======================================
        public static bool EliminarUsuario(Usuario usuario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "DELETE from Usuario WHERE Id = @id";

                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Id };

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
        // Iniciar sesion -----------------------------
        public static string IniciarSesion(Usuario usuario)
        {
            string resultado = "Nombre de usuario o contraseña incorrectos";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contraseña";

                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                resultado = $"Bienvenido {usuario.Nombre} {usuario.Apellido}";
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }
    }
}
