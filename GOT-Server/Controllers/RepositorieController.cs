using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOT_Server.Contexts;
using GOT_Server.Entities;
using GOT_Server.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GOT_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositorieController : ControllerBase
    {
        public RepositorieController(AppDb db)
        {
            Db = db;
        }

        // GET api/Commited
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new RepositorieQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/Commited/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new RepositorieQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // GET api/Commited/5
        [HttpGet("{name}")]
        public async Task<IActionResult> GetID(string name)
        {
            await Db.Connection.OpenAsync();
            var query = new RepositorieQuery(Db);
            var result = await query.FindID(name);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Commited
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Repositorie body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/Commited/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Repositorie body)
        {
            await Db.Connection.OpenAsync();
            var query = new RepositorieQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.NameRepositorie = body.NameRepositorie;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/Commited/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new RepositorieQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/Commited
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new RepositorieQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }

        public AppDb Db { get; }
    }
}
