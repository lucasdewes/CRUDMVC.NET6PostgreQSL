using Microsoft.Data.SqlClient.Server;
using Npgsql;

namespace WebPostgreSQL.Models
{
    public class Consultas
    {
        public static async Task<List<Dictionary<string, object>>> RodarComandoSQL(string sSQL)
        {
            #region Abre connection

            // Configurar a string de conexão com o banco de dados
            string connectionString = "Host=localhost;Port=5432;Pooling=true;Database=SISTEMALEITE;User Id=postgres;Password=admin;";

            // Criar a conexão
            using var connection = new NpgsqlConnection(connectionString);
            //connection.Open();
            await connection.OpenAsync();

            #endregion Abre connection

            #region Cria a string sql e faz a consulta
            // Criar o comando SQL
            using var command = connection.CreateCommand();
            //monta a string sql
            command.CommandText = sSQL;

            // Executar o comando e le os resultados
            using var reader = command.ExecuteReader();

            #endregion Cria a string sql e faz a consulta

            #region formata e retorna os dados

            // Lista de dicionários para armazenar os resultados
            var resultList = new List<Dictionary<string, object>>();

            // Ler os dados e adicionar ao dicionário
            while (reader.Read())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object value = reader.GetValue(i);

                    row[columnName] = value;
                }

                resultList.Add(row);
            }
            return resultList;

            #endregion formata e retorna os dados
        }

        public static async Task<List<Dictionary<string, object>>> GetConsultaLoginAsync(string sSenha, string sEmail)
        {
            #region Abre connection

            // Configurar a string de conexão com o banco de dados
            string connectionString = "Host=localhost;Port=5432;Pooling=true;Database=SISTEMALEITE;User Id=postgres;Password=admin;";

            // Criar a conexão
            using var connection = new NpgsqlConnection(connectionString);
            //connection.Open();
            await connection.OpenAsync();

            #endregion Abre connection

            #region Cria a string sql e faz a consulta
            // Criar o comando SQL
            using var command = connection.CreateCommand();
            //monta a string sql
            command.CommandText = $@"SELECT * FROM usuarios where ""PassWord"" = '{sSenha}' and ""Email"" = '{sEmail}'";

            // Executar o comando e le os resultados
            using var reader = command.ExecuteReader();

            #endregion Cria a string sql e faz a consulta

            #region formata e retorna os dados

            // Lista de dicionários para armazenar os resultados
            var resultList = new List<Dictionary<string, object>>();

            // Ler os dados e adicionar ao dicionário
            while (reader.Read())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object value = reader.GetValue(i);

                    row[columnName] = value;
                }

                resultList.Add(row);
            }
            return resultList;

            #endregion formata e retorna os dados
        }
    }
}