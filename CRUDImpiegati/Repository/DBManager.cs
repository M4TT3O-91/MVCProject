using CRUDImpiegati.Models;
using System.Data.SqlClient;

namespace CRUDImpiegati.Repository
{
    public class DBManager
    {
        private static string connectionString = @"Server = ACADEMYNETUD07\SQLEXPRESS; Database = Impiegati; Trusted_Connection = True;";

        public List<ImpiegatoViewModel> GetAllImpiegati()
        {
            List<ImpiegatoViewModel> impiegatiList = new List<ImpiegatoViewModel>();
            string sql = @"Select * from Impiegato";

            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var impiegato = new ImpiegatoViewModel
                {
                    ID = Convert.ToInt32(reader["ID"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    Città = reader["Citta"].ToString(),
                    Salario = Convert.ToDecimal(reader["Salario"].ToString()),
                };
                impiegatiList.Add(impiegato);
            }
            return impiegatiList;
        }

        public int EditImpiegato(ImpiegatoViewModel impiegato)
        {
            string sql = @"UPDATE Impiegato
                       SET [Nome] = @Nome
                          ,[Cognome] = @Cognome
                          ,[Salario] = @Salario
                          ,[Citta] = @Citta
                     WHERE ID =@ID";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Nome", impiegato.Nome);
            command.Parameters.AddWithValue("@Cognome", impiegato.Cognome);
            command.Parameters.AddWithValue("@Citta", impiegato.Città);
            command.Parameters.AddWithValue("@Salario", impiegato.Salario);
            command.Parameters.AddWithValue("@ID", impiegato.ID);

            return command.ExecuteNonQuery();
        }
    }
}
