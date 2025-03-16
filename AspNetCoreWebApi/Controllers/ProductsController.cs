﻿using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;
        public ProductsController(ShopContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task <ActionResult> GetAllProducts()
        {
            return Ok(await _context.Products.ToArrayAsync()); 
        }

        [HttpGet("{id}")]
        public async Task <ActionResult> GetProduct(int id)
        {
            var product =await  _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpGet("available")]
        public async Task <ActionResult<IEnumerable<Product>>> GetAvailableProduct()
        {
            return await _context.Products.Where(p => p.IsAvailable).ToArrayAsync();
        }
    }
}
