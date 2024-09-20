using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Convierte automáticamente ObjectId a string
        public string Id { get; set; }

        [BsonElement("idusuario")]
        public string IdUsuario { get; set; }

        [BsonElement("idjuego")]

        public string IdJuego { get; set; }

        [BsonElement("titulo")]

        public string Titulo { get; set; }

        [BsonElement("info")]

        public string Info { get; set; }

        [BsonElement("foto")]

        public string Foto { get; set; }
    }
}