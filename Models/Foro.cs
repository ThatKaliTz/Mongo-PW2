using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Models
{
    public class Foro
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("idjuego")]
        public string IdJuego { get; set; }

        [BsonElement("titulo")]

        public string Titulo { get; set; }



        [BsonElement("descripcion")]

        public string Descripcion { get; set; }

        [BsonElement("comentarios")]

        public string Comentarios { get; set; }

        

    }
}