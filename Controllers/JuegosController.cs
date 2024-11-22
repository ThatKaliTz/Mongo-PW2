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


        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest(new { message = "No file uploaded" });
                }

                var directoryPath = Path.Combine("wwwroot", "juegos");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var fileName = Path.GetFileName(image.FileName);
                var safeFileName = fileName.Replace(" ", "_");
                var filePath = Path.Combine(directoryPath, safeFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var imageUrl = $"http://localhost:5119/juegos/{safeFileName}";
                return Ok(new { imageUrl });
            }
            catch (Exception ex)
            {
                // Log del error (puedes usar un sistema de logging como Serilog)
                Console.WriteLine($"Error al subir la imagen: {ex.Message}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario([FromBody] Juego updatedGame)
        {
            var filter = Builders<Juego>.Filter.Eq("Nombre", updatedGame.Nombre);
            var update = Builders<Juego>.Update
                .Set("Foto", updatedGame.Foto)
                .Set("Nombre", updatedGame.Nombre)
                .Set("consola", updatedGame.Consola)
                .Set("anio", updatedGame.Anio)
                .Set("genero", updatedGame.Genero)
                .Set("sinopsis", updatedGame.Sinopsis)
                .Set("calificacion", updatedGame.Calificacion);


            var result = await _juegosCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            return Ok(new
            {
                nombre = updatedGame.Nombre,
                consola = updatedGame.Consola,
                foto = updatedGame.Foto,
                anio = updatedGame.Anio,
                genero = updatedGame.Genero,
                sinopsis = updatedGame.Sinopsis,
                calificacion = updatedGame.Calificacion
            });
        }



        [HttpGet("{nombre}")]
        public async Task<IActionResult> GetJuegoById(string nombre)
        {
            var filter = Builders<Juego>.Filter.Eq("Nombre", nombre);
            var juego = await _juegosCollection.Find(filter).FirstOrDefaultAsync();
            if (juego == null)
            {
                return NotFound("Juego no encontrado");
            }
            return Ok(juego);
        }
    }


}