using MongoApi.Entities;

namespace MongoApi.Entities {
    public class Produto : Entity {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public override string NomePlural () {
            return "Pedidos";
        }
    }
}