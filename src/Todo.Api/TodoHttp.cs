using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Todo.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Threading;
using System.Diagnostics;

namespace Todo.Api
{
    public class TodoController
    {
        private readonly ToDoContext _context;
        public TodoController(ToDoContext context)
        {
            _context = context;
        }


        [FunctionName("GetTodos")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous,Constants.HttpActions.Get, Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var todos = await _context.TodoItems.ToListAsync();
                return new OkObjectResult(todos);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
                throw;
            }                      
        }


        [FunctionName("WriteTodo")]
        public async Task<IActionResult> Post(
            [HttpTrigger(AuthorizationLevel.Anonymous, 
            Constants.HttpActions.Post, Route = null)] HttpRequest req,
            ILogger log)
        {
            string description = null;
            
            try
            {

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                description ??= data?.TodoDescription;

                Random random = new Random();

                _context.TodoItems.Add(new TodoItem { Description = description, CreateDate = System.DateTime.Now,Id= random.Next(2, 222222) });

                await _context.SaveChangesAsync();

                string responseMessage =  $"Created todo item, {description}. This HTTP triggered function executed successfully.";

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error creating Todo");
                return new BadRequestObjectResult(ex);
            }
           
        }

        [FunctionName("DeleteTodo")]
        public async Task<IActionResult> Delete(
           [HttpTrigger(AuthorizationLevel.Function, Constants.HttpActions.Delete, Route = "todo/{Id}")] HttpRequest req,
           CancellationToken cts,
           ILogger log)
        {
            int id = 0;
            if (!int.TryParse(req.Query["Id"], out id))
                return new NotFoundResult();
            else
            {
                var item = _context.TodoItems.SingleAsync(x => x.Id == id);

                _context.Remove(item);

                await _context.SaveChangesAsync(cts);

                string responseMessage = "Item deleted successfully";

                return new OkObjectResult(responseMessage);
            }
        }

    }
}
