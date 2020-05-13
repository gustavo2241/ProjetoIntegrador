using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegradorWeb.Models
{
    public class EstoqueCadastraContext
    {
        public string ConnectionString { get; set; }

        public EstoqueCadastraContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public string cadastraProduto(EstoqueModel produto)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                var newPreco = produto.precoProduto.Replace(',', '.');

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO TB_PRODUTOS(codProduto, nomeProduto, categoriaProduto, qtdProduto, precoProduto) VALUES(@codProduto, @nomeProduto, @categoriaProduto, @qtdProduto, @precoProduto)";
                comm.Parameters.AddWithValue("@codProduto", produto.codProduto);
                comm.Parameters.AddWithValue("@nomeProduto", produto.nomeProduto);
                comm.Parameters.AddWithValue("@categoriaProduto", produto.categoriaProduto);
                comm.Parameters.AddWithValue("@qtdProduto", produto.qtdProduto);
                comm.Parameters.AddWithValue("@precoProduto", newPreco);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            return "OK";
        }
    }
}
