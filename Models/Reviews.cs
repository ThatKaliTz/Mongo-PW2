using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Models
{
    public class Review
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("usuario")]
        public string Usuario { get; set; }

        [BsonElement("juego")]

        public string Juego { get; set; }

        [BsonElement("titulo")]
        public string Titulo { get; set; }

        [BsonElement("calificacion")]

        public int Calificacion { get; set; }

        [BsonElement("contenido")]

        public string Contenido { get; set; }

    }
}