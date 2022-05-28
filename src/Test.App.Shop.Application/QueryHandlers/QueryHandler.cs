using System.Data;
using Microsoft.Data.SqlClient;
using Test.App.Shop.Infra.CrossCutting.Environments.Configurations;

namespace Test.App.Shop.Application.QueryHandlers;

public abstract class QueryHandler
{
    protected readonly IDbConnection Database;

    protected QueryHandler(ApplicationConfiguration applicationConfiguration)
    {
        Database = new SqlConnection(applicationConfiguration.ConnectionString);
    }
}
