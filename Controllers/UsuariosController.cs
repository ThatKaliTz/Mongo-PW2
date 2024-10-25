using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IMongoCollection<Usuario> _usuariosCollection;

        public UsuariosController()
        {
            var connectionString = "mongodb://localhost:27017"; // URI de conexión
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("pagina"); // Nombre de la base de datos
            _usuariosCollection = database.GetCollection<Usuario>("usuarios"); // Nombre de la colección
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuariosCollection.Find(_ => true).ToListAsync();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario(Usuario nuevoUsuario)
        {
            await _usuariosCollection.InsertOneAsync(nuevoUsuario); // Inserta el nuevo usuario en la colección
            return CreatedAtAction(nameof(GetUsuarios), new { id = nuevoUsuario.Id }, nuevoUsuario);
        }
        [HttpDelete("{user}")]
        public async Task<IActionResult> DeleteUsuario(string user)
        {
            var filter = Builders<Usuario>.Filter.Eq("User", user);
            var result = await _usuariosCollection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            return Ok(new { message = "Usuario eliminado correctamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var filter = Builders<Usuario>.Filter.Eq("Email", usuario.Email);
            var userInDb = await _usuariosCollection.Find(filter).FirstOrDefaultAsync();

            if (userInDb == null || userInDb.password != usuario.password)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(new { message = "Login successful", user = userInDb.User });
        }
    }
}