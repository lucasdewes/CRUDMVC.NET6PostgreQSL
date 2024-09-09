using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebPostgreSQL.Models;

var sqlConnectionStringBuilder = WebApplication.CreateBuilder(args);

// Adicionar serviço no container
sqlConnectionStringBuilder.Services.AddControllersWithViews();

// Adicionar parte de Login
sqlConnectionStringBuilder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Access/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});

// Acessar a string de conexão do appsettings.json
var _stringDeConnexao = sqlConnectionStringBuilder.Configuration.GetConnectionString("DefaultConnection");

sqlConnectionStringBuilder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<DbContextAplicacao>(option => option.UseNpgsql(_stringDeConnexao));

var app = sqlConnectionStringBuilder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Criar ou atualizar as triggers, functions e a view no banco de dados
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DbContextAplicacao>();

    var triggerSql = @"
        CREATE OR REPLACE FUNCTION atualizar_produtos_ordenha()
        RETURNS trigger AS $$
        BEGIN
            IF EXISTS (SELECT 1 FROM ""Produto"" WHERE ""Nome"" = 'Papel Toalha') AND
                      EXISTS (SELECT 1 FROM ""Produto"" WHERE ""Nome"" = 'Sabão Líquido Neutro') THEN
                UPDATE ""Produto"" SET ""Quantidade"" = ""Quantidade"" - 4 WHERE ""Nome"" = 'Papel Toalha';
                UPDATE ""Produto"" SET ""Quantidade"" = ""Quantidade"" - 10 WHERE ""Nome"" = 'Sabão Líquido Neutro';
            END IF;
            RETURN NEW;
        END;
        $$ LANGUAGE plpgsql;

        DROP TRIGGER IF EXISTS trigger_atualizar_produtos_ordenha ON ""OrdenhaAnimais"";

        CREATE TRIGGER trigger_atualizar_produtos_ordenha
        AFTER INSERT ON ""OrdenhaAnimais""
        FOR EACH ROW
        EXECUTE FUNCTION atualizar_produtos_ordenha();

------------------------------

        CREATE OR REPLACE FUNCTION atualizar_produtos_registro_ordenha()
        RETURNS trigger AS $$
        BEGIN
            IF EXISTS (SELECT 1 FROM ""Produto"" WHERE ""Nome"" = 'Leite Cru') THEN
                UPDATE ""Produto""
                SET ""Quantidade"" = ""Quantidade"" + NEW.""VolumeLeite"" WHERE ""Nome"" = 'Leite Cru';
            END IF;
            RETURN NEW;
        END;
        $$ LANGUAGE plpgsql;

        DROP TRIGGER IF EXISTS trigger_atualizar_produtos_registro_ordenha ON ""RegistroOrdenha"";

        CREATE TRIGGER trigger_atualizar_produtos_registro_ordenha
        AFTER INSERT ON ""RegistroOrdenha""
        FOR EACH ROW
        EXECUTE FUNCTION atualizar_produtos_registro_ordenha();

------------------------------

CREATE OR REPLACE FUNCTION obter_producao_leite(data_inicio DATE, data_fim DATE)
RETURNS TABLE (
    ""Id do Animal"" INT,
    ""SISBOV"" TEXT,
    ""Volume Total de Leite"" NUMERIC
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        a.""Id"" as ""Id do Animal"",
        a.""SISBOV"",
        SUM(r.""VolumeLeite"" / total_animais.total)/ 1000 AS ""Volume Total de Leite""
    FROM
        ""Animal"" a
    JOIN
        ""OrdenhaAnimais"" oa ON a.""Id"" = oa.""AnimalId""
    JOIN
        ""RegistroOrdenha"" r ON oa.""OrdenhaId"" = r.""Id""
    JOIN (
        SELECT
            oa.""OrdenhaId"",
            COUNT(*) AS total
        FROM
            ""OrdenhaAnimais"" oa
        GROUP BY
            oa.""OrdenhaId""
    ) total_animais ON r.""Id"" = total_animais.""OrdenhaId""
    WHERE
        r.""DataOrdenha"" BETWEEN data_inicio AND data_fim
    GROUP BY
        a.""Id"", a.""SISBOV"";
END;
$$ LANGUAGE plpgsql;

    ";

    context.Database.ExecuteSqlRaw(triggerSql);
}

// Configurar o pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo),
    SupportedCultures = new List<CultureInfo> { cultureInfo },
    SupportedUICultures = new List<CultureInfo> { cultureInfo }
});

app.Run();