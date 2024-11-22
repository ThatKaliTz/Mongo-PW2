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

            return Ok(new { nombre = userInDb.Nombre, apellido = userInDb.Apellido, foto = userInDb.Foto, email = userInDb.Email, user = userInDb.User,
             password = userInDb.password, admin = userInDb.admin });
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

        var directoryPath = Path.Combine("wwwroot", "images");
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

        var imageUrl = $"http://localhost:5119/images/{safeFileName}";
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
        public async Task<IActionResult> UpdateUsuario([FromBody] Usuario updatedUser)
        {
            var filter = Builders<Usuario>.Filter.Eq("Email", updatedUser.Email);
            var update = Builders<Usuario>.Update
                .Set("Foto", updatedUser.Foto)
                .Set("User", updatedUser.User)
                .Set("Nombre", updatedUser.Nombre)
                .Set("Email", updatedUser.Email)
                .Set("password", updatedUser.password);

            var result = await _usuariosCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

          return Ok(new { nombre = updatedUser.Nombre, apellido = updatedUser.Apellido, foto = updatedUser.Foto, email = updatedUser.Email,
           user = updatedUser.User, password = updatedUser.password, admin = updatedUser.admin});
        }
    }



}