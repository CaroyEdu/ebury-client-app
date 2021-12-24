using BDLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace ebury_client
{
    public class User
    {

        private static string BD_SERVER = Properties.Settings.Default.BD_SERVER;
        private static string BD_NAME = Properties.Settings.Default.BD_NAME;
        private static string BD_USER = Properties.Settings.Default.BD_USER;
        private static string BD_PASSWORD = Properties.Settings.Default.BD_PASSWORD;

        private string username;
        private string password;
        private string endYear;
        private string startYear;
        private string nif;

        public User(string n, string p)
        {

            string connection_data = @"server=" + BD_SERVER + ";userid=" + BD_USER
                + ";password=" + BD_PASSWORD + ";database=" + BD_NAME;

            MySqlConnection co = null;

            try
            {
                co = new MySqlConnection(connection_data);
                co.Open();

                string query = "SELECT * FROM customer WHERE username = '" + n + "';";
                var command = new MySqlCommand(query, co);
                MySqlDataReader r = command.ExecuteReader();
                r.Read();
                bool found = false;

                if (r.GetString(1) == p)
                {
                    found = true;
                    username = n;
                    password = p;
                    startYear = r.GetString(2);
                    endYear = (r.IsDBNull(3)) ? "noExistente" : r.GetString(3);
                    nif = r.GetString(4);
                    
                }
                if (!found)
                {
                    throw new Error("Incorrect Username or Password");
                }
            }
            catch (Exception e)
            {
                throw new Error("ERROR: " + e);
            }
            finally
            {
                if (co != null)
                {
                    co.Close();
                }
            }


        }
        public string userName
        {
            get { return username; }
        }
        public string Nif
        {
            get { return nif; }
        }

    }
}