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
    public class Customer{

        private static string BD_SERVER = Properties.Settings.Default.BD_SERVER;
        private static string BD_NAME = Properties.Settings.Default.BD_NAME;
        private static string BD_USER = Properties.Settings.Default.BD_USER;
        private static string BD_PASSWORD = Properties.Settings.Default.BD_PASSWORD;

        private string username;
        private string password;
        //private string name;
        //private string startDate;
        //private string endDate;

        public Customer(string n, string p)
        {
            

            string connection_data = @"server=" + BD_SERVER + ";userid=" + BD_USER 
                + ";password=" + BD_PASSWORD + ";database=" + BD_NAME;

            MySqlConnection co = null;

            try
            {
                co = new MySqlConnection(connection_data);
                co.Open();

                string query = "SELECT * FROM customer;";
                MySqlDataAdapter da = new MySqlDataAdapter(query, co);
                DataSet ds = new DataSet();
                da.Fill(ds, "customer");
                DataTable dt = ds.Tables["customer"];
                bool found = false;

                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        if(col.ToString() == "username" && row[col].ToString() == n)
                        {
                            if(row["password"].ToString() == p)
                            {
                                username = row[col].ToString();
                                password = row["password"].ToString();
                                
                                found = true;
                            }
                        }
                    }
                }
                if(!found)
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
        public string UserName
        {
            get { return username; }
        }
        
        
    }
}
