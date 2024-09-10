#CRUD MVC com PostgreSQL em .NET 8
#Este projeto é uma aplicação CRUD desenvolvida em .NET 8, utilizando Entity Framework Core, PostgreSQL, e tecnologias modernas como Blazor para a interface do usuário.

Tecnologias Utilizadas
.NET 8: Framework principal para desenvolvimento backend.
Entity Framework Core: Para mapeamento objeto-relacional (ORM) e controle do banco de dados.
PostgreSQL: Banco de dados utilizado para armazenar as informações.
Blazor: Framework da Microsoft para a construção de interfaces web dinâmicas.
Bootstrap: Para o design responsivo e estilização da interface.
C#: Linguagem de programação principal do projeto.
Visual Studio 2022: IDE recomendada para desenvolvimento e execução do projeto.
Pré-requisitos
PostgreSQL: Certifique-se de ter o PostgreSQL instalado e em execução.
Visual Studio 2022: Com as ferramentas de desenvolvimento web e para .NET instaladas.
Como rodar o projeto
Clone este repositório:

git clone https://github.com/lucasdewes/CRUDMVC.NET6PostgreQSL.git

Abra a solução no Visual Studio 2022 (arquivo .sln).

Restaure os pacotes NuGet da solução:

No Visual Studio, clique com o botão direito na solução e escolha "Restaurar pacotes NuGet".
Configure a string de conexão em Program.cs e Consultas.cs com as informações do seu banco PostgreSQL.

Crie e aplique as migrations para atualizar o banco de dados:

Abra o Package Manager Console e execute:
bash
Copy code
Add-Migration NomeMigration
Update-Database
Execute o projeto:

Pressione F5 ou clique em "Iniciar Debug" no Visual Studio.
Para fazer login, crie um usuário manualmente na tabela Usuarios ou utilize o fluxo de login já implementado.
Funcionalidades Adicionais
Triggers e Functions: O projeto implementa triggers e funções SQL avançadas para manipulação de dados e otimização de consultas.
Relatório de Produção de Leite: Funcionalidade que permite gerar relatórios detalhados do volume de leite produzido por cada animal.
Estrutura do Projeto
Models: Definições de classes que representam as tabelas do banco de dados.
Controllers: Controladores responsáveis pelas operações CRUD e lógica do sistema.
Views: Interface com o usuário construída com Blazor e Razor Pages.
Migrations: Arquivos gerados pelo Entity Framework para manter o banco de dados atualizado com base nos modelos.
Contribuições
Sinta-se à vontade para abrir pull requests ou relatar issues.
