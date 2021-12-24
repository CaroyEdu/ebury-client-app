using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using BDLibrary;

namespace ebury_client
{
    public partial class EditUser : Form
    {

        private User user;

        public EditUser(User u)
        {
            InitializeComponent();
            this.user = u;
            mostrarDatos();
        }

        private void EditUser_Load(object sender, EventArgs e)
        {
            mostrarDatos();
        }

        private void mostrarDatos()
        {
            string connection_data = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";
            MySqlConnection co = null;

            try
            {
                co = new MySqlConnection(connection_data);
                co.Open();
                string sql = "SELECT nif, firstName, lastName, birthDate, city, street, number, postalCode, country FROM particular";
                var command = new MySqlCommand(sql, co);
                MySqlDataReader r = command.ExecuteReader();
                while(r.Read())
                {
                    if(r.GetString(0) == user.Nif)
                    {
                        lNIF.Text = user.Nif;
                        tNombre.Text = r.GetString(1);
                        tApellidos.Text = r.GetString(2);
                        monthCalendar.SetDate(DateTime.Parse((String)r.GetString(3)));
                        tCiudad.Text = r.GetString(4);
                        tCalle.Text = r.GetString(5);
                        tNumero.Text = r.GetString(6);
                        tCodigoPostal.Text = r.GetString(7);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            finally
            {
                co.Close();
            }
        }
    }
}
