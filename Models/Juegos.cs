using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Models
{
    public class Juego
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Convierte automáticamente ObjectId a string
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("consola")]

        public string Apellido { get; set; }

        [BsonElement("foto")]

        public string Foto { get; set; }

        [BsonElement("genero")]

        public string Genero { get; set; }

        [BsonElement("anio")]

        public string Anio { get; set; }

        [BsonElement("sinopsis")]

        public string Sinopsis { get; set; }
    }
}