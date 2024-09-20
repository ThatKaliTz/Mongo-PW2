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
    }
}