using Fina.Main.Crud.Shared.Data;
using Fina.Main.Crud.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Main.Crud.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly ApplicationDbContext dbContext;
        public ProductController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return await dbContext.Products.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(x => x.ID == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Product product)
        {
            dbContext.Add(product);
            await dbContext.SaveChangesAsync();
            return new CreatedAtRouteResult("GetProduct", new { id = product.ID }, product);

        }
        [HttpPut]
        public async Task<ActionResult> Put(Product product)
        {
            dbContext.Entry(product).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>Delete(int id)
        {
            var product = new Product { ID = id };
            dbContext.Remove(product);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

    }

}
