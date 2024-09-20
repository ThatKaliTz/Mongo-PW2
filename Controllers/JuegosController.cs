using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JuegosController : ControllerBase
    {
        private readonly IMongoCollection<Juego> _juegosCollection;

        public JuegosController()
        {
            var connectionString = "mongodb://localhost:27017"; // URI de conexión
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("pagina"); // Nombre de la base de datos
            _juegosCollection = database.GetCollection<Juego>("juegos"); // Nombre de la colección
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Juego>>> GetJuegos()
        {
            var juegos = await _juegosCollection.Find(_ => true).ToListAsync();
            return Ok(juegos);


        }

        [HttpPost]
        public async Task<ActionResult<Juego>> CreateJuego(Juego nuevoJuego)
        {
            await _juegosCollection.InsertOneAsync(nuevoJuego); // Inserta el nuevo usuario en la colección
            return CreatedAtAction(nameof(GetJuegos), new { id = nuevoJuego.Id }, nuevoJuego);
        }

        [HttpDelete("{nombre}")]

        public async Task<IActionResult> DeleteJuego(string nombre)
        {
            var filter = Builders<Juego>.Filter.Eq("Nombre", nombre);
            var result = await _juegosCollection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                return NotFound(new { message = "Juego no encontrado" });
            }

            return Ok(new { message = "Juego eliminado correctamente" });
        }
    }
}