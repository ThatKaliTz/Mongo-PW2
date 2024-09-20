using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Convierte automáticamente ObjectId a string
        public string Id { get; set; }

        [BsonElement("idusuario")]
        public string IdUsuario { get; set; }

        [BsonElement("idjuego")]

        public string IdJuego { get; set; }

        [BsonElement("calificacion")]

        public int Calificacion { get; set; }

        [BsonElement("descripcion")]

        public string Descripcion { get; set; }

    }
}