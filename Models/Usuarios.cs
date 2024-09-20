using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Models
{
    public class Usuario
    {
        [BsonId] // Convierte automáticamente ObjectId a string
        public ObjectId Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("apellido")]

        public string Apellido { get; set; }

        [BsonElement("foto")]

        public string Foto { get; set; }

        [BsonElement("email")]

        public string Email { get; set; }

        [BsonElement("user")]

        public string User { get; set; }

        [BsonElement("password")]

        public string password { get; set; }
    }
}