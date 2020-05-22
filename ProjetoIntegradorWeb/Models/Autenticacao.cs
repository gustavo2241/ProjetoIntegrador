using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegradorWeb.Models
{
    public class Autenticacao
    {
        public string ConnectionString { get; set; }

        public Autenticacao(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public string RegistrarUsuario(Usuario usuario)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Usuario(login, senha, nome, email) VALUES(@Login, @Senha, @Nome, @Email)";
                cmd.Parameters.AddWithValue("@Login", usuario.Login);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);

                var resultado = cmd.ExecuteNonQuery().ToString();
                conn.Close();
                return resultado;
            }
        }

        public string ValidarLogin(Usuario usuario)
        {
            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario WHERE login = @Login and senha = @Senha";
                cmd.Parameters.AddWithValue("@Login", usuario.Login);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

                conn.Open();
                string resultado = cmd.ExecuteScalar()?.ToString();
                conn.Close();
                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    return null;
                }                
            }
        }
    }
}
