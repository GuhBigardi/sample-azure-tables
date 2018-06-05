using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace tables.auditoria.poc
{
    public class Auditoria : TableEntity
    {
        public Auditoria()
        {
        }

        public Auditoria(string nomeServico, string login, string mensagem)
        {
            PartitionKey = nomeServico;
            RowKey = login;
            Mensagem = mensagem;
        }

        public string Mensagem { get; set; }
        public DateTime DataOperacao { get => DateTime.Now; }
    }
}
