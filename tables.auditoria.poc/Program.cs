using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.IO;

namespace tables.auditoria.poc
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var connectionString = Configuration.GetSection("ConnectionStrings:auditoriapoc_AzureStorageConnectionString").Value;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                           connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CreateTable(connectionString, tableClient);
            InsertRegister(tableClient);
            GetAuditoria("CadastroUsuario", "gustavo.bigardi@lambda3.com.br", tableClient);
        }

        private static void InsertRegister(CloudTableClient tableClient)
        {
            var auditoria = new Auditoria("CadastroUsuario", "gustavo.bigardi@hotmail.com.br", "logando na aplicação");

            CloudTable table = tableClient.GetTableReference("auditoria");
            TableOperation insertOperation = TableOperation.Insert(auditoria);

            table.ExecuteAsync(insertOperation);
        }

        private static void CreateTable(string connectionString, CloudTableClient tableClient)
        {

            CloudTable table = tableClient.GetTableReference("auditoria");
            table.CreateIfNotExistsAsync();
        }

        public static void GetAuditoria(string pKey, string rKey, CloudTableClient tableClient)
        {
            CloudTable table = tableClient.GetTableReference("auditoria");
            TableOperation tableOperation = TableOperation.Retrieve<Auditoria>(pKey, rKey);
            var entity = table.ExecuteAsync(tableOperation).Result;
        }
    }
}
