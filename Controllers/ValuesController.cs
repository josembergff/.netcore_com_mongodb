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
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        public Context Context { get; }
        public IRepository Repository { get; }

        public ValuesController (IRepository repository) {
            Repository = repository;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Cliente>> Get () {
            Expression<Func<Cliente, bool>> filter1 = c => true;
            var clientes = await Repository.Listar<Cliente> (filter1);
            return clientes;
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public async Task<Cliente> Get (string id) {
            var cliente = await Repository.BuscarId<Cliente> (id);
            return cliente;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<string>> Post ([FromBody] Cliente value) {
            if (!value.EstaValidoParaInsercao ()) {
                return BadRequest (value.Notifications);
            }
            var retorno = await Repository.Acrescentar<Cliente> (value);

            return retorno;
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public async Task<ActionResult<bool>> Put (string id, [FromBody] Cliente value) {
            value._id = id;
            if (!value.EstaValidoParaAtualizacao ()) {
                return BadRequest (value.Notifications);
            }
            await Repository.Atualizar<Cliente> (value);
            return true;
        }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public async Task<ActionResult<bool>> Delete (string id) {
            var cliente = new Cliente ();
            cliente._id = id;
            if (!cliente.EstaValidoParaAtualizacao ()) {
                return BadRequest (cliente.Notifications);
            }
            await Repository.Remover<Cliente> (id);
            return true;
        }
    }
}