using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiNorthwind.Models;

namespace WebApiNorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IEnumerable<Categorie> Get()
        {
            var categories = (from p in _context.Categories select p).ToList();
            return categories;

        }
        [HttpGet("{id}")]
        public Categorie Get(int id)
        {
            var categories = (from p in _context.Categories where p.CategoryId == id select p).SingleOrDefault();
            return categories;

        }
       
    }
}
