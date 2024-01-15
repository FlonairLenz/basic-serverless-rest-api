using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using YouTube.Tutorial.AzureFunctionEinstieg.Todos.Models;

namespace YouTube.Tutorial.AzureFunctionEinstieg.Todos.Functions;

public static class CreateTodo
{
    [FunctionName("CreateTodo")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "todos")]
        HttpRequest req,
        [Sql(commandText: "dbo.Todo", connectionStringSetting: "SqlConnectionString")]
        IAsyncCollector<TodoItem> todoTable,
        ILogger log)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var name = JsonConvert.DeserializeObject<TodoItem>(requestBody)?.Name;
        
        var todo = new TodoItem
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsComplete = false
        };
        
        await todoTable.AddAsync(todo);
        
        return new OkObjectResult(todo);
    }
}