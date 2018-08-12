using System;
using MongoDB.Bson.Serialization.Attributes;
using prmToolkit.NotificationPattern;

namespace MongoApi.Entities {
    public class Cliente : Entity {

        public string Nome { get; set; }

        internal override bool EstaValidoParaInsercao () {
            new AddNotifications<Cliente> (this).IfNullOrEmpty (x => x.Nome, "O nome não pode ser vazio");

            new AddNotifications<Cliente> (this).IfLengthLowerThan (x => x.Nome, 4, "O nome têm que ter mais que 3 caracteres");
            return base.EstaValidoParaInsercao () && IsValid ();

            new AddNotifications<Cliente> (this).IfLengthGreaterThan (x => x.Nome, 200, "O nome têm que ter menos que 200 caracteres");
            return base.EstaValidoParaInsercao () && IsValid ();
        }

        public override string NomePlural () {
            return "Clientes";
        }
    }
}