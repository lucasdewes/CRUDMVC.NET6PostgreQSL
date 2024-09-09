using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPostgreSQL.Models; // Certifique-se de usar o namespace correto

public class RelatorioController : Controller
{
    private readonly string _connectionString;

    public RelatorioController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // GET: Relatorio/Index
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Pesquisar(DateTime dataInicio, DateTime dataFim)
    {
        // Converte DateTime para Date (remove a parte do tempo)
        var dataInicioFormatada = dataInicio.Date.ToString("yyyy-MM-dd");
        var dataFimFormatada = dataFim.Date.ToString("yyyy-MM-dd");

        // Construa a consulta SQL formatando as datas
        string ssqlconsulta = @$"SELECT * FROM obter_producao_leite('{dataInicioFormatada}', '{dataFimFormatada}')";

        var resultados = new List<ProducoesLeite>();

        // Use ADO.NET para executar a consulta
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new NpgsqlCommand(ssqlconsulta, connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var producao = new ProducoesLeite
                        {
                            IdAnimal = reader.GetInt32(reader.GetOrdinal("Id do Animal")),
                            SISBOV = reader.GetString(reader.GetOrdinal("SISBOV")),
                            VolumeTotalLeite = reader.GetDecimal(reader.GetOrdinal("Volume Total de Leite"))
                        };

                        resultados.Add(producao);
                    }
                }
            }
        }
        ViewData["DataInicio"] = dataInicio.ToString("yyyy-MM-dd");
        ViewData["DataFim"] = dataFim.ToString("yyyy-MM-dd");
        // Certifique-se de passar a lista para a view
        return View("Index", resultados);
    }
}