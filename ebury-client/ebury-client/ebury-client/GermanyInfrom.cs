using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ebury_client
{
    public partial class GermanyInfrom : Form
    {
        
        private User mainUser;
        private GForms form;

        public GermanyInfrom(User user)
        {   

            InitializeComponent();
            hideIndicator();
            this.mainUser = user;
            showLeftPannel();
            InformTypeLable.Visible = false;
            form = new GForms(mainUser );
        }


        private void showLeftPannel()
        {
            bAlemania.Visible = true;
            bHolanda.Visible = true;
            pictureDefault.Visible = true;
            panelLeft.Visible = true;
            bHome.Visible = true;
            labelUser.Visible = true;
            labelHello.Visible = true;
            labelUser.Text = mainUser.userName;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (initialInform.Checked)
            {

                initialinformCreator();

            }
            else if (weeklyInform.Checked)
            {
                weeklyinformCreator();
            }
            else
            {
                MessageBox.Show("porfavor selecciona un informe");
            }
        }

        private void weeklyinformCreator()
        {
            //throw new NotImplementedException();
            form.weeklyInform();
            showIndicator();
        }

        private void initialinformCreator()
        {
            //throw new NotImplementedException();
            form.initialInform();
            showIndicator();
        }


        
        private void showIndicator()
        {
            indicatiorLable.Visible = true;
            pathLable.Text = form.fileName(); 
            pathLable.Visible = true;

        }
        private void hideIndicator()
        {
            indicatiorLable.Visible = false;
            pathLable.Visible = false;
        }
        private void loadAllDataFromDataGridView()
        {
            /* int aux = dataGridView1.Columns.Count ; 
             foreach(DataGridViewRow row in dataGridView1.Rows)
             {
                 String cellText = ""; 
                 for (int i = 0; i < dataGridView1.Columns.Count; i++)
                 {
                     cellText += (string)row.Cells[i].Value ;
                     cellText += delimiter;
                 }
                 csvFileCreator(cellText); 
             }
           
             showIndicator();
         }*/




        }

        private void initialInform_CheckedChanged(object sender, EventArgs e)
        {
            if (initialInform.Checked)
            {
                InformTypeLable.Text = "Informe Inicial";
                InformTypeLable.Visible = true; 
            }
        }

        private void weeklyInform_CheckedChanged(object sender, EventArgs e)
        {
            if (weeklyInform.Checked)
            {
                InformTypeLable.Text = "Informe Semanal";
                InformTypeLable.Visible = true;
            }
        }

    }
}
