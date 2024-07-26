#nullable enable
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LegacyApp
{
	public interface IClientRepository {
		Client GetById(int id);
	}

	public class ClientRepository : IClientRepository
    {
        public Client GetById(int id)
        {
            Client client = null;
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspGetClientById"
                };

                var parameter = new SqlParameter("@ClientId", SqlDbType.Int) { Value = id };
                command.Parameters.Add(parameter);

                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read()) {
	                Enum? cat = Enum.GetValues<SpecialClientCategory>().FirstOrDefault(v => v.ToString() == reader["Name"].ToString());
	                if (cat == null) {
		                cat = SpecialClientCategory.Unknown;
	                }

	                client = new Client
                                      {
                                          Id = int.Parse(reader["ClientId"].ToString()),
                                          SpecialClientCategory = (SpecialClientCategory)cat,
                                          ClientStatus = (ClientStatus)int.Parse(reader["ClientStatusId"].ToString())
                                      };
                }
            }

            return client;
        }
    }
}
