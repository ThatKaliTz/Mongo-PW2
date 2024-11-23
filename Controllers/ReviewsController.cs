using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IMongoCollection<Review> _reviewCollection;

        public ReviewController()
        {
            var connectionString = "mongodb://localhost:27017"; // URI de conexión
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("pagina"); // Nombre de la base de datos
            _reviewCollection = database.GetCollection<Review>("reviews"); // Nombre de la colección
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await _reviewCollection.Find(_ => true).ToListAsync();
            return Ok(reviews);


        }

        [HttpPost]
        public async Task<ActionResult<Juego>> CreateJuego(Review nuevoReview)
        {
            await _reviewCollection.InsertOneAsync(nuevoReview); // Inserta el nuevo usuario en la colección
            return CreatedAtAction(nameof(GetReviews), new { id = nuevoReview.Id }, nuevoReview);
        }


    }


}