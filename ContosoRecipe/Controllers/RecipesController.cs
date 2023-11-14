using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace ContosoRecipe.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private List<string> dishes = new List<string> { "oxtail", "Curry Chicken", "Popcorn" };

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetDishes([FromQuery] int count)
        {
            if (count <= 0)
            {
                // Return a bad request if the count is not valid.
                return BadRequest("Invalid count parameter.");
            }

            if (!dishes.Any())
            {
                // Return NotFound if there are no dishes.
                return NotFound("No dishes found.");
            }

            // Return Ok with the dishes taken according to the count.
            return Ok(dishes.Take(count));
        }

        [HttpPost]
        public ActionResult<string> CreateNewRecipes([FromBody] string newDish)
        {
            if (string.IsNullOrEmpty(newDish))
            {
                // Return a bad request if the newDish is empty.
                return BadRequest("The newDish parameter is empty.");
            }

            // Add the newDish to the list and return its value.
            dishes.Add(newDish);
            return Ok(newDish);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            // Check if the dish with the given id exists in the list.
            if (dishes.Contains(id))
            {
                // If it exists, remove it and return NoContent.
                dishes.Remove(id);
                return NoContent();
            }
            else
            {
                // If it doesn't exist, return NotFound.
                return NotFound("Dish not found.");
            }
        }
    }
}
