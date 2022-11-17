using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using WebApiNorthwind.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace WebApiNorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CustomerController(NorthwindContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var customer = (from p in _context.Customers select p).ToList();
            return customer;

        }
        [HttpGet("{id}")]
        public Customer Get(string id) {
            var customer = (from p in _context.Customers where p.CustomerId == id select p).SingleOrDefault();
            return customer;
        
        }
        [HttpGet("{companyName}/{contactName}")]
        public dynamic Get(string companyName, string contactName) {
            dynamic customer = (from p in _context.Customers where p.CompanyName == companyName && p.ContactName == contactName select new { p.CompanyName, p.ContactName });
            return customer;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult Put([FromBody]Customer customer, string id)
        {
            if (id != customer.CustomerId)
            {
                BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(string id)
        {
            var resultado = _context.Customers.FirstOrDefault(x => x.CustomerId ==id);
            if (resultado == null)
            { return NotFound(); }
            _context.Customers.Remove(resultado);
            _context.SaveChanges();
            return resultado;
        }



    }
}
