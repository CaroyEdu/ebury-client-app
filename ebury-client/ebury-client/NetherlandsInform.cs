using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ebury_client
{
    public partial class NetherlandsInform : Form
    {
        private User mainUser;
        private string[] colClientes = { "username", "startDate", "endDate", "nif" },
            colCuentasBancarias = { "accountNumber", "balance", "currency" };
        private HForms form;

        public NetherlandsInform(User u)
        {
            InitializeComponent();
            this.mainUser = u;
            showLeftPannel();
            form = new HForms(mainUser);
        }

        private void showLeftPannel()
        {
            bAlemania.Visible = false;
            bHolanda.Visible = false;
            pictureDefault.Visible = true;
            panelLeft.Visible = true;
            bHome.Visible = true;
            labelUser.Visible = true;
            labelHello.Visible = true;
            labelUser.Text = mainUser.userName;
        }

        private void bHome_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tFiltro_Click(object sender, EventArgs e)
        {
            tPrimerNombre.Text = "";
        }

        private void bAlemania_Click(object sender, EventArgs e)
        {
            Visible = false;
            Close();
            GermanyInfrom GI = new GermanyInfrom(mainUser);
            GI.ShowDialog();
        }

        private void bConexion_Click(object sender, EventArgs e)
        {
            string connStr = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                lEstado.Text = "Conectado";
                lEstado.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {
                lEstado.Text = "Desconectado";
                lEstado.ForeColor = System.Drawing.Color.Red;
            }
            conn.Close();
        }

        private void bHome_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.Equals(cSeleccion.Text, "Clientes"))
            {
                pClientes.Visible = true;
                pCuentas.Visible = false;
            }
            else if (String.Equals(cSeleccion.Text, "Cuentas bancarias"))
            {
                pCuentas.Visible = true;
                pClientes.Visible = false;
            }
        }

        private void bDescargar_Click(object sender, EventArgs e)
        {
            if (String.Equals(cSeleccion.Text, "Clientes"))
                form.clientInform(tPrimerNombre.Text, tSegundoNombre.Text, tCiudad.Text, tCalle.Text, tNumero.Text, tCodigoPostal.Text);
            else if (String.Equals(cSeleccion.Text, "Cuentas bancarias"))
                if (cActivas.Checked || cInactivas.Checked)
                    form.accountForm(cActivas.Checked, cInactivas.Checked, tNumeroProducto.Text);
                else
                    MessageBox.Show("Seleccione un checkbox.");
        }
    }
}
