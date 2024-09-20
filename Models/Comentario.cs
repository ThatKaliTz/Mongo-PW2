using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Models
{
    public class Comentario
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("idforo")]
        public string IdForo { get; set; }

        [BsonElement("idusuario")]
        public string IdUsuario { get; set; }

        [BsonElement("comentario")]

        public string comentario { get; set; }


        [BsonElement("fecha")]

        public string Fecha { get; set; }


    }
}