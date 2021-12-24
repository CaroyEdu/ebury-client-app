using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ebury_client
{
    public partial class Index : Form
    {

        private User user = null;

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
            catch
            {
                MessageBox.Show("Usuario/Contraseña Incorrecta");
            }
        }

        private void connected()
        {
            //Muestro
            labelUser.Visible = true;
            labelHello.Visible = true;
            labelUser.Text = user.userName;
            bAlemania.Visible = true;
            bHolanda.Visible = true;
            pictureDefault.Visible = true;
            panelLeft.Visible = true;
            bHome.Visible = true;
            bDisconnect.Visible = true;

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
        }

        private void disconnected()
        {
            //Oculto
            labelUser.Visible = false;
            labelHello.Visible = false;
            bAlemania.Visible = false;
            bHolanda.Visible = false;
            pictureDefault.Visible = false;
            panelLeft.Visible = false;
            bHome.Visible = false;
            bDisconnect.Visible = false;

            //Muestro
            labelPassword.Visible = true;
            labelUsername.Visible = true;
            panelRight.Visible = true;
            bLogin.Visible = true;
            pictureLeft.Visible = true;
            pictureRight.Visible = true;
            tUsername.Visible = true;
            tPassword.Visible = true;
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            disconnected();
        }

        private void bAlemania_Click(object sender, EventArgs e)
        {
            
           GermanyInfrom GI = new GermanyInfrom(user);
           this.Visible = false;
           GI.ShowDialog();
           this.Visible = true; 
        }
       
    }
}
