using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoApi.Entities;
using MongoDB.Driver;

namespace MongoApi.Data
{
    public class RepositoryMongo : IRepository
    {

        public RepositoryMongo(Context context)
        {
            Context = context;
        }

        public Context Context { get; }

        public async Task<string> Acrescentar<TEntidade>(TEntidade entidade) where TEntidade : Entity
        {
            entidade.DataCadastro = DateTime.Now;
            await Context.Dados<TEntidade>(entidade).InsertOneAsync(entidade);
            return entidade._id;
        }

        public async Task<bool> Atualizar<TEntidade>(TEntidade entidade) where TEntidade : Entity
        {
            entidade.DataEdicao = DateTime.Now;
            await Context.Dados<TEntidade>(entidade).UpdateOneAsync(Builders<TEntidade>.Filter.Eq(s => s._id, entidade._id), Builders<TEntidade>.Update.Set(s => s, entidade));

            return true;
        }

        public async Task<TEntidade> BuscarId<TEntidade>(string id) where TEntidade : Entity
        {
            var novo = Activator.CreateInstance<TEntidade>();
            var lista = await Context.Dados<TEntidade>(novo).FindAsync(f => f._id == id);

            return lista.FirstOrDefault();
        }

        public async Task<IEnumerable<TEntidade>> Listar<TEntidade>(Expression<Func<TEntidade, bool>> filtro) where TEntidade : Entity
        {
            var novo = Activator.CreateInstance<TEntidade>();

            var lista = await Context.Dados<TEntidade>(novo).FindAsync(filtro);

            return lista.ToList();
        }

        public async Task<bool> Remover<TEntidade>(Expression<Func<TEntidade, bool>> filtro) where TEntidade : Entity
        {
            var novo = Activator.CreateInstance<TEntidade>();

            var lista = await Context.Dados<TEntidade>(novo).DeleteOneAsync(filtro);

            return true;
        }

        public async Task<bool> RemoverId<TEntidade>(string id) where TEntidade : Entity
        {
            var novo = Activator.CreateInstance<TEntidade>();

            var lista = await Context.Dados<TEntidade>(novo).DeleteOneAsync(f => f._id == id);

            return true;

        }
    }
}