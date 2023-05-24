using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebPostgreSQL.Models
{
    public class Consultas
    {
        public List<Dictionary<string, object>> GetUsuariosParaLogin()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();

            sqlConnectionStringBuilder.ConnectionString = "Host=localhost;Port=5432;Pooling=true;Database=SISTEMALEITE;User Id=postgres;Password=admin;";
            //sqlConnectionStringBuilder.DataSource = "<your_server.database.windows.net>";
            //sqlConnectionStringBuilder.UserID = "<admin>";
            //sqlConnectionStringBuilder.Password = "<admin>";
            //sqlConnectionStringBuilder.InitialCatalog = "SISTEMALEITE";

            using (SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
               
                connection.Open();

                String sSql = "SELECT * FROM Usuarios";

                using (SqlCommand command = new SqlCommand(sSql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                        }
                    }
                }
            }

            return new List<Dictionary<string, object>>();
        }
    }
}
