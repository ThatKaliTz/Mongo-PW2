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
        public async Task<ActionResult<IEnumerable<Juego>>> Getjuegos()
        {
            var juegos = await _juegosCollection.Find(_ => true).ToListAsync();
            return Ok(juegos);

            
        }
    }
}