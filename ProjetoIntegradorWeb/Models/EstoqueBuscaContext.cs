using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegradorWeb.Models
{
    public class EstoqueBuscaContext
    {
        public string ConnectionString { get; set; }

        public EstoqueBuscaContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<EstoqueModel> buscaProduto()
        {
            List<EstoqueModel> list = new List<EstoqueModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM TB_PRODUTOS", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new EstoqueModel()
                        {
                            codProduto =  reader.GetInt64("codProduto"),
                            nomeProduto = reader["nomeProduto"].ToString(),
                            categoriaProduto = reader["categoriaProduto"].ToString(),
                            qtdProduto = reader.GetInt64("qtdProduto"),
                            precoProduto = reader["precoProduto"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
