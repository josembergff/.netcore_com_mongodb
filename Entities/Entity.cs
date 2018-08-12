using System;
using MongoDB.Bson.Serialization.Attributes;
using prmToolkit.NotificationPattern;

namespace MongoApi.Entities {
    public abstract class Entity : Notifiable {
        [BsonId]
        [BsonRepresentation (MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonDateTimeOptions]
        public DateTime DataCadastro { get; set; }

        [BsonDateTimeOptions]
        public DateTime DataEdicao { get; set; }

        internal virtual bool EstaValidoParaInsercao () {
            return this._id != string.Empty;
        }
        internal virtual bool EstaValidoParaAtualizacao () {
            return _id != string.Empty;
        }

        public abstract string NomePlural ();
    }
}