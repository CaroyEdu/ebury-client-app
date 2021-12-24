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
    public partial class Index : Form
    {

        private User user = null;
        private string[] colCuentasBancarias = { "Número de Cuenta", "Saldo", "Divisa" };

        public Index()
        {
            InitializeComponent();
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            try
            {
                user = new User(tUsername.Text, tPassword.Text);
                connected();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void connected()
        {
            //Muestro
            lBienvenido.Visible = true;
            lBienvenido.Text = "Bienvenido, " + user.userName;
            bAlemania.Visible = true;
            bHolanda.Visible = true;
            pictureDefault.Visible = true;
            panelLeft.Visible = true;
            bDisconnect.Visible = true;
            panelHome1.Visible = true;
            panelHome2.Visible = true;
            panelHome3.Visible = true;

            // Oculto
            tUsername.Text = "";
            tPassword.Text = "";
            labelPassword.Visible = false;
            labelUsername.Visible = false;
            panelRight.Visible = false;
            bLogin.Visible = false;
            pictureLeft.Visible = false;
            pictureRight.Visible = false;
            tUsername.Visible = false;
            tPassword.Visible = false;

            //Cargo las cuentas del Usuario
            cargarCuentas();
        }

        private void disconnected()
        {
            //Oculto
            lBienvenido.Visible = false;
            bAlemania.Visible = false;
            bHolanda.Visible = false;
            pictureDefault.Visible = false;
            panelLeft.Visible = false;
            bHome.Visible = false;
            bDisconnect.Visible = false;
            panelHome1.Visible = false;
            panelHome2.Visible = false;
            panelHome3.Visible = false;

            //Muestro
            labelPassword.Visible = true;
            labelUsername.Visible = true;
            panelRight.Visible = true;
            bLogin.Visible = true;
            pictureLeft.Visible = true;
            pictureRight.Visible = true;
            tUsername.Visible = true;
            tPassword.Visible = true;

            //Limpio las cuentas cargadas
            dataGridView.Rows.Clear();
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            disconnected();
        }

        private void bAlemania_Click(object sender, EventArgs e)
        {
            string connection_data = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";

            MySqlConnection co = null;
            co = new MySqlConnection(connection_data);
            co.Open();
            string sql = "SELECT nif FROM allowGermany";
            var command = new MySqlCommand(sql, co);
            MySqlDataReader r = command.ExecuteReader();
            bool found = false;

            while (r.Read())
            {
                if(r.GetString(0) == user.Nif)
                {
                    GermanyInfrom GI = new GermanyInfrom(user);
                    this.Visible = false;
                    GI.ShowDialog();
                    this.Visible = true;
                    found = true;
                }
            }
            if(!found)
            {
                MessageBox.Show("No tienes permiso para acceder a esta area.");
            }
        }

        private void bHolanda_Click(object sender, EventArgs e)
        {
            string connection_data = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";

            MySqlConnection co = null;
            co = new MySqlConnection(connection_data);
            co.Open();
            string sql = "SELECT nif FROM allowNetherlands";
            var command = new MySqlCommand(sql, co);
            MySqlDataReader r = command.ExecuteReader();
            bool found = false;

            while (r.Read())
            {
                if (r.GetString(0) == user.Nif)
                {
                    NetherlandsInform NI = new NetherlandsInform(user);
                    this.Visible = false;
                    NI.ShowDialog();
                    this.Visible = true;
                    found = true;
                }
            }
            if (!found)
            {
                MessageBox.Show("No tienes permiso para acceder a esta area.");
            }
        }

        private void cargarCuentas()
        {
            string connection_data = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";

            MySqlConnection co = null;

            try
            {
                co = new MySqlConnection(connection_data);
                co.Open();
                string sql = "SELECT accountNumber FROM customerXAccount WHERE nif = " + user.Nif;
                var command = new MySqlCommand(sql, co);
                MySqlDataReader r = command.ExecuteReader();

                while (r.Read())
                {
                    dataGridView.ColumnCount = colCuentasBancarias.Length;
                    for (int i = 0; i < colCuentasBancarias.Length; i++)
                        dataGridView.Columns[i].Name = colCuentasBancarias[i];

                    MySqlConnection co2 = new MySqlConnection(connection_data);
                    co2.Open();
                    string query = "SELECT accountNumber, balance, currency FROM bankAccount "
                        + "WHERE accountNumber = " + r.GetString(0);
                    MySqlDataReader rdr = new MySqlCommand(query, co2).ExecuteReader();
                    while (rdr.Read())
                    {
                        int tam = rdr.FieldCount;
                        string[] lect = new string[tam];
                        for (int i = 0; i < tam; i++)
                        {
                            string str = rdr[i].ToString();
                            lect[i] = !String.Equals(str, "") ? str : "no existente";
                        }
                        dataGridView.Rows.Add(lect);
                    }
                    rdr.Close();
                    co2.Close();
                }
            }
            catch (Exception e)
            {
                throw new Error("Error al recuperar los datos de las cuentas bancarias. " + e.ToString());
            }
            finally
            {
                if (co != null)
                {
                    co.Close();
                }
            }
        }

    }
}
