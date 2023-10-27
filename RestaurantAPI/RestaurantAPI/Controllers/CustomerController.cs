using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RestaurantAPI.Models;
using Newtonsoft.Json;
using RestaurantAPI.DTO.Response;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public CustomerController(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers() {
            var data = await _dbContext.Customers.Include(c => c.Transactions).ToListAsync();
            return data;

        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer newCustomer) {
            if (newCustomer == null) {
                return BadRequest("Data tidak valid");
            }

            try
            {
                _dbContext.Customers.Add(newCustomer);
                await _dbContext.SaveChangesAsync();
                return Ok(newCustomer);
            }
            catch (DbUpdateException ex) {
                return BadRequest("Gagal menyimpan data : " + ex.Message);
            }
        }

        private bool CustomerExists(int id) {
            return _dbContext.Customers.Any(customer => customer.customer_id == id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomerData(int id, Customer customerDataEdit) {
            if (id != customerDataEdit.customer_id) {
                return BadRequest();
            }

            _dbContext.Entry(customerDataEdit).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException) {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id) {
            var customer = await _dbContext.Customers.FindAsync(id);

            if (customer == null) {
                return NotFound();
            }

            _dbContext.Customers.Remove(customer);

            await _dbContext.SaveChangesAsync();

            return Ok("Data customer berhasil dihapus");
        }

        [HttpGet("id")]
        public async Task<ActionResult<Customer>> GetDetailCustomer(int id) {
            var customer = await _dbContext.Customers.FindAsync(id);

            if (customer == null) {

                return NotFound("Data customer tidak ditemukan");
            }

            return customer;
        }


        
      

     
    }
}
