using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.DTO.Response;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/foods")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public FoodController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodResponse>>> GetFoods() {
            var dataFoods = await _dbContext.Foods.ToListAsync();
            var foodDTOS = dataFoods.Select(food => new FoodResponse
            {
                food_id = food.food_id,
                food_name = food.food_name,
                description = food.description,
                price = food.price,
                category = food.category
            }).ToList();

            return foodDTOS;
        
        }

        [HttpPost]
        public async Task<IActionResult> CreateFood([FromBody] Food newFood) {

            if (newFood == null) {
                return BadRequest();
            }

            try
            {

                _dbContext.Foods.Add(newFood);
                await _dbContext.SaveChangesAsync();
                return Ok(newFood);
            }
            catch (DbUpdateException ex) {
                return BadRequest("Gagal Menyimpan data : " + ex.Message);
            }
        }

        private bool FoodIsExists(int id) {
            return _dbContext.Foods.Any(food => food.food_id == id);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {

            var food = await _dbContext.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound("Food data is not found");

            }

            _dbContext.Foods.Remove(food);

            await _dbContext.SaveChangesAsync();

            return Ok("Food data have been deleted");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditFoodData(int id, Food foodEdit)
        {
            if (id != foodEdit.food_id)
            {
                return BadRequest();
            }

            _dbContext.Entry(foodEdit).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodIsExists(id))
                {
                    return NotFound("Food data is not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetDetailFood(int id)
        {
           
                var food = await _dbContext.Foods.FindAsync(id);
                if (food == null)
                {
                    return NotFound("Food data not found");
                }
                

                    return food;
                        

                
           
        }








    }
}
