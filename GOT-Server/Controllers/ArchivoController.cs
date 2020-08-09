using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOT_Server.Contexts;
using GOT_Server.Entities;
using GOT_Server.Queries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GOT_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        public ArchivoController(AppDb db)
        {
            Db = db;
        }

        // GET api/Archivo
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new ArchivoQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/Archivo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            await Db.Connection.OpenAsync();
            var query = new ArchivoQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Archivo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Archivo body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/Archivo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(string id, [FromBody] Archivo body)
        {
            await Db.Connection.OpenAsync();
            var query = new ArchivoQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.DireccionArchivo = body.DireccionArchivo;
            result.DataArchivo = body.DataArchivo;
            result.NombreArchivo = body.NombreArchivo;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/Archivo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(string id)
        {
            await Db.Connection.OpenAsync();
            var query = new ArchivoQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/Archivo
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new ArchivoQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }

        public AppDb Db { get; }
    }
}
