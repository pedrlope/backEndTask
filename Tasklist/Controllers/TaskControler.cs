using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasklist.Controllers.Models;

namespace Tasklist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksControlers : ControllerBase
    {
        private static List<ModelTask> modelTasks =
            new List<ModelTask>();

        [HttpGet]
        public ActionResult<List<ModelTask>> SearchTasks()
        {
            return Ok(modelTasks);
        }

        [HttpPost("crie uma task")]
        public ActionResult<List<ModelTask>>
           AddTask(ModelTask novo)
        {
            if(novo.Description.Length <10)
                return BadRequest("precisa de mais caracteres");

            if (novo.Id == 0 && modelTasks.Count > 0)
                novo.Id = modelTasks[modelTasks.Count - 1].Id + 1;

            modelTasks.Add(novo);
            return Ok(modelTasks);


        }

        

        [HttpDelete("remova uma task{id}")]
        public ActionResult<List<ModelTask>> DeleteTasks
            (int id)
        {
            var remocao = modelTasks.Find(x => x.Id == id);

            if (remocao is null)
                return NotFound("Task não existe");

            modelTasks.Remove(remocao);
            return Ok();

        }
    }
}