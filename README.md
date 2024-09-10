
# CRUD MVC com PostgreSQL em .NET 8

Este projeto é uma aplicação CRUD desenvolvida em .NET 8, utilizando Entity Framework Core, PostgreSQL, e Razor Views para a interface do usuário.

## Tecnologias Utilizadas

- **.NET 8**: Framework principal para desenvolvimento backend.
- **Entity Framework Core**: Para mapeamento objeto-relacional (ORM) e controle do banco de dados.
- **PostgreSQL**: Banco de dados utilizado para armazenar as informações.
- **ASP.NET Core com Razor Views**: Framework da Microsoft para construção de interfaces web dinâmicas utilizando páginas Razor e a engine de views do ASP.NET.
- **Bootstrap**: Para o design responsivo e estilização da interface.
- **C#**: Linguagem de programação principal do projeto.
- **Visual Studio 2022**: IDE recomendada para desenvolvimento e execução do projeto.

## Pré-requisitos

- **PostgreSQL**: Certifique-se de ter o PostgreSQL instalado e em execução.
- **Visual Studio 2022**: Com as ferramentas de desenvolvimento web e para .NET instaladas.

## Como rodar o projeto

1. Clone este repositório:
   ```bash
   https://github.com/lucasdewes/CRUDMVC.NET6PostgreQSL.git
   ```

2. Abra a solução no Visual Studio 2022 (arquivo `.sln`).

3. Restaure os pacotes NuGet da solução:
   - No Visual Studio, clique com o botão direito na solução e escolha "Restaurar pacotes NuGet".

4. Configure a string de conexão em `Program.cs`, `appsettings.json` e `Consultas.cs` com as informações do seu banco PostgreSQL.

5. Crie e aplique as migrations para atualizar o banco de dados:
   - Abra o **Package Manager Console** e execute:
     ```bash
     Add-Migration NomeMigration
     Update-Database
     ```

6. Execute o projeto:
   - Pressione `F5` ou clique em "Iniciar Debug" no Visual Studio.
   - Para fazer login, crie um usuário manualmente na tabela `Usuarios` ou utilize o fluxo de login já implementado.

## Funcionalidades Adicionais

- **Triggers e Functions**: O projeto implementa triggers e funções SQL avançadas para manipulação de dados e otimização de consultas.
- **Relatório de Produção de Leite**: Funcionalidade que permite gerar relatórios detalhados do volume de leite produzido por cada animal.

## Estrutura do Projeto

- **Models**: Definições de classes que representam as tabelas do banco de dados.
- **Controllers**: Controladores responsáveis pelas operações CRUD e lógica do sistema.
- **Views**: Interface com o usuário construída com Razor Pages.
- **Migrations**: Arquivos gerados pelo Entity Framework para manter o banco de dados atualizado com base nos modelos.

## Contribuições

Sinta-se à vontade para abrir pull requests ou relatar issues.
