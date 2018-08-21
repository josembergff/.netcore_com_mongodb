using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoApi.Entities;

namespace MongoApi.Data {
    public interface IRepository {
        Task<string> Acrescentar<TEntidade> (TEntidade entidade) where TEntidade : Entity;

        Task<TEntidade> BuscarId<TEntidade> (string id) where TEntidade : Entity;

        Task<IEnumerable<TEntidade>> Listar<TEntidade> (Expression<Func<TEntidade, bool>> filtro) where TEntidade : Entity;

        Task<bool> Atualizar<TEntidade> (TEntidade entidade) where TEntidade : Entity;

        Task<bool> Remover<TEntidade> (Expression<Func<TEntidade, bool>> filtro) where TEntidade : Entity;

        Task<bool> RemoverId<TEntidade> (string id) where TEntidade : Entity;
    }
}