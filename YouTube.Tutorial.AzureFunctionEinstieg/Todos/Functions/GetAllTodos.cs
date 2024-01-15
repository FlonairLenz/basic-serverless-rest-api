using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using YouTube.Tutorial.AzureFunctionEinstieg.Todos.Models;

namespace YouTube.Tutorial.AzureFunctionEinstieg.Todos.Functions;

public static class GetAllTodos
{
    [FunctionName("GetAllTodos")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todos")]
        HttpRequest req,
        [Sql(commandText: "SELECT * FROM dbo.Todo",
            commandType: CommandType.Text,
            connectionStringSetting: "SqlConnectionString")]
        IEnumerable<TodoItem> todos,
        ILogger log)
    {
        return new OkObjectResult(todos);
    }
}