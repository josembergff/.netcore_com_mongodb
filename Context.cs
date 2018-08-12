using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoApi.Entities;
using MongoDB.Driver;

namespace MongoApi {
    public class Context {
        public readonly IMongoDatabase database;

        public Context (IConfiguration config, IOptions<Settings> options) {
            database = new MongoClient (options.Value.ConnectionString).GetDatabase (options.Value.Database);
        }

        public IMongoCollection<TEntidade> Dados<TEntidade> (TEntidade entidade) where TEntidade : Entity {
            return database.GetCollection<TEntidade> (entidade.NomePlural ());
        }
    }
}