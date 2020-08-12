using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOT_Server.Contexts;
using GOT_Server.Entities;
using GOT_Server.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GOT_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitedController : Controller
    {
        public CommitedController(AppDb db)
        {
            Db = db;
        }

        // GET api/Commited
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new CommitQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/Commited/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            await Db.Connection.OpenAsync();
            var query = new CommitQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Commited
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Commited body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/Commited/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(string id, [FromBody] Commited body)
        {
            await Db.Connection.OpenAsync();
            var query = new CommitQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.MessageCommit = body.MessageCommit;
            result.DateCommit = body.DateCommit;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/Commited/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(string id)
        {
            await Db.Connection.OpenAsync();
            var query = new CommitQuery(Db);
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
            var query = new CommitQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }

        public AppDb Db { get; }
    }
}
