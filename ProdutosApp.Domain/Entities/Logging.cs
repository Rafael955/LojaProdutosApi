using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Domain.Entities
{
    public class Logging_CadastroProduto
    {
        #region Propriedades

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        [BsonElement("_id")]
        public Guid? Id { get; set; }

        [BsonElement("produto")]
        public string? Produto { get; set; }

        [BsonElement("fornecedor")]
        public string? Fornecedor { get; set; }

        [BsonElement("descricao")]
        public string? Descricao { get; set; }

        [BsonElement("datahoragravacao")]
        public DateTime? DataHoraGravacao { get; set; }

        //[BsonElement("pessoa_id")]
        //[BsonRepresentation(MongoDB.Bson.BsonType.String)]
        //public Guid? PessoaId { get; set; }

        #endregion
    }
}
