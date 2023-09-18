using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FRUTI_Extens.Motor
{
    class DBUpdate
    {
        private static string connectionString;

        public DBUpdate()
        {
            connectionString = "Server=PRIMAVERA_P10;Database=PRIFRUTI2018;User Id=sa;Password=*Pelicano*;";
        }

        public bool ExecutarUpdate(string sqlQuery)
        {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection)) {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}






