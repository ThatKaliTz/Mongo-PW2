using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Models
{
    public class Juego
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("consola")]

        public string Consola { get; set; }

        [BsonElement("foto")]

        public string Foto { get; set; }

        [BsonElement("genero")]

        public string Genero { get; set; }

        [BsonElement("anio")]

        public string Anio { get; set; }

        [BsonElement("sinopsis")]

        public string Sinopsis { get; set; }
    
        [BsonElement("calificacion")]
        public string Calificacion { get; set; }

        [BsonElement("dmain")]
        public string Dmain { get; set; }
        [BsonElement("dcomp")]
        public string Dcomp { get; set; }
        [BsonElement("developer")]
        public string Developer { get; set; }
        [BsonElement("publisher")]
        public string Publisher { get; set; }
    }
}