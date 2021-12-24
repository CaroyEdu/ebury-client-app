using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BDLibrary;

namespace ebury_client
{
    class GForms
    {
        private static string BD_SERVER = Properties.Settings.Default.BD_SERVER;
        private static string BD_NAME = Properties.Settings.Default.BD_NAME;
        private static string BD_USER = Properties.Settings.Default.BD_USER;
        private static string BD_PASSWORD = Properties.Settings.Default.BD_PASSWORD;

        private string connection_data = "server=" + BD_SERVER + ";user=" + BD_USER
                + ";database=" + BD_NAME + ";port=3306" + ";password=" + BD_PASSWORD;

        //private MySqlConnection co ;


        private User mainUser;
        private string delimiter = ", ";
        //we are trying to find the desktop location later saving the file there
        private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) ;

        public GForms(User u)
        {
            mainUser = u;
           
        }

        public void initialInform()
        {
            MySqlConnection conn = new MySqlConnection(connection_data);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT accountNumber,DATE_FORMAT(startDate, '%Y-%m-%d'),DATE_FORMAT(endDate, '%Y-%m-%d'),lastName,firstName,street,city,postalCode,country,nif,DATE_FORMAT(birthDate, '%Y-%m-%d') " + 
                    "FROM customer JOIN particular  USING (nif) JOIN  customerXAccount USING(nif) JOIN eburyAccount USING(accountNumber) WHERE EXTRACT(YEAR FROM CURRENT_DATE) - EXTRACT(YEAR FROM startDate) <=5 " + 
                    " AND Country = 'DE'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string aux = "";
                    for (int i = 0; i < rdr.FieldCount; ++i)
                    {
                        string aux2 = rdr[i].ToString();
                        string aux1 = "";
                        string exists = "";
                        if (String.Equals(aux2, aux1))
                        {
                            exists = "noexistente";
                        }
                        else
                        {
                            exists = aux2;
                        }
                        aux += exists;
                        aux += delimiter;
                    }
                    string auxMinusLastChar = aux.Remove(aux.Length - 1, 1);
                    auxMinusLastChar = auxMinusLastChar.Remove(auxMinusLastChar.Length - 1, 1); //these above sentences are used to remove the last two delimiters
                    auxMinusLastChar += Environment.NewLine;
                    csvFileCreator(auxMinusLastChar);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            
            }

        public void weeklyInform()
        {
            string connStr = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT accountNumber,DATE_FORMAT(startDate, '%Y-%m-%d'),DATE_FORMAT(endDate, '%Y-%m-%d'),lastName,firstName,street,city,postalCode,country,nif,DATE_FORMAT(birthDate, '%Y-%m-%d') FROM customer   JOIN particular  USING (nif) JOIN  customerXAccount USING(nif) JOIN eburyAccount USING(accountNumber) WHERE DATEDIFF(CURDATE(), DATE_FORMAT(FROM_UNIXTIME(UNIX_TIMESTAMP(`startDate`)), '%Y-%m-%d')) <=7 AND DATEDIFF(CURDATE(), DATE_FORMAT(FROM_UNIXTIME(UNIX_TIMESTAMP(`startDate`)), '%Y-%m-%d')) >=0 AND accountState = 'Active' AND Country = 'DE'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string aux = "";
                    for (int i = 0; i < rdr.FieldCount; ++i)
                    {
                        string aux2 = rdr[i].ToString();
                        string aux1 = "";
                        string exists = "";
                        if (String.Equals(aux2, aux1))
                        {
                            exists = "noexistente" ;
                        }else{
                            exists = aux2 ;
                        }
                        aux += exists; 
                        aux += delimiter; 
                    }
                    string auxMinusLastChar = aux.Remove(aux.Length - 1, 1);
                    auxMinusLastChar = auxMinusLastChar.Remove(auxMinusLastChar.Length - 1, 1); //these above sentences are used to remove the last two delimiters
                    auxMinusLastChar += Environment.NewLine;
                    csvFileCreator(auxMinusLastChar);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
           
        }
        


        public void csvFileCreator(string inputText)
        {
            String location = fileName();

            if (!File.Exists(location))
            {
                // Create a file to write to
                string createText = "Numero de cuenta" + delimiter + "Fecha de inicio" + delimiter + "Fecha de terminacion" + delimiter + "Apellido" + delimiter + "Nombre" + delimiter +
                                    "Calle" + delimiter + "Ciudad" + delimiter + "Codigo Postal" + delimiter +
                                    "Pais" + delimiter + "Numero de identificacion" + delimiter + "Fecha de nacimiento" + Environment.NewLine;
                File.WriteAllText(location, createText);

            }
            string appendText = inputText;
            File.AppendAllText(location, appendText);

        }
        public String fileName()
        {
            DateTime date = DateTime.Now;
            String fName = @"\Ebury_IBAN_" + date.ToString("ddMMyyyyHHmmss") +@".csv";
            String finalPath = desktopPath + fName;
            return finalPath;
        }
    }
}
