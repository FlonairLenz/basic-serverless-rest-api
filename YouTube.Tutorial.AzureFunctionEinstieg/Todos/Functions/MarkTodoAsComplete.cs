using System;
using System.Collections.Generic;
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

public static class MarkTodoAsComplete
{
    [FunctionName("MarkTodoAsComplete")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "todos/complete")]
        HttpRequest req,
        [Sql(commandText: "UPDATE dbo.Todo SET IsComplete = 1 WHERE Id = @Id",
            commandType: System.Data.CommandType.Text,
            parameters: "@Id={Query.id}",
            connectionStringSetting: "SqlConnectionString")]
        IEnumerable<TodoItem> todos,
        ILogger log)
    {
        return new OkObjectResult(req.Query["id"].ToString());
    }
}