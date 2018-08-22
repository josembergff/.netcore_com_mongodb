using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoApi.Data;
using MongoApi.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoApi.Controllers {
    public abstract class CrudController<TEntidade> : ControllerBase where TEntidade : Entity, new () {
        private IRepository Repository { get; }

        public CrudController (IRepository repository) {
            Repository = repository;
        }
        // GET api/values
        [HttpGet]
        public virtual async Task<IEnumerable<TEntidade>> Get () {
            Expression<Func<TEntidade, bool>> filter1 = c => true;
            var clientes = await Repository.Listar<TEntidade> (filter1);
            return clientes;
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public async Task<TEntidade> Get (string id) {
            var cliente = await Repository.BuscarId<TEntidade> (id);
            return cliente;
        }

        // POST api/values
        [HttpPost]
        public virtual async Task<ActionResult<string>> Post ([FromBody] TEntidade value) {
            value.DataCadastro = DateTime.Now;
            if (!value.EstaValidoParaInsercao ()) {
                return BadRequest (value.Notifications);
            }
            var retorno = await Repository.Acrescentar<TEntidade> (value);

            return retorno;
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public async Task<ActionResult<bool>> Put (string id, [FromBody] TEntidade value) {
            value._id = id;
            if (!value.EstaValidoParaAtualizacao ()) {
                return BadRequest (value.Notifications);
            }
            await Repository.Atualizar<TEntidade> (value);
            return true;
        }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public async Task<ActionResult<bool>> Delete (string id) {
            await Repository.RemoverId<TEntidade> (id);
            return true;
        }
    }
}