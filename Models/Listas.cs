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
        public string Nombre { get; set; }

        [BsonElement("titulo")]

        public string Titulo { get; set; }

        [BsonElement("cantidad")]

        public int cantidad { get; set; }

        [BsonElement("date")]

        public string Date { get; set; }

        [BsonElement("juegos")]

        public string Juegos { get; set; }
    }
}