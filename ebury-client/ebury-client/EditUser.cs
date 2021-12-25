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
        private String nombre;
        private String Apellido;
        private String fNacimiento;
        private String cp;
        private String ciudad;
        private String calle;
        private String num;
        private String NOEXISTENTE = "no existente";
        


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
            string connection_data = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW; convert zero datetime=True";
            MySqlConnection co = null;

            try
            {
                co = new MySqlConnection(connection_data);
                co.Open();
                string sql = "SELECT nif, firstName, lastName, birthDate, city, street, number, postalCode, country FROM particular where nif='"+user.Nif+"';";
                var command = new MySqlCommand(sql, co);
                MySqlDataReader r = command.ExecuteReader();
                
                r.Read(); 
                
                lNIF.Text = user.Nif;


                nombre = (r.IsDBNull(1) ? NOEXISTENTE : r.GetString(1));
                tNombre.Text = nombre;

                Apellido = (r.IsDBNull(2) ? NOEXISTENTE : r.GetString(2));
                tApellidos.Text = Apellido;

                fNacimiento = (r.IsDBNull(3) ? DateTime.Now.ToString("yyyy-MM-dd") : r.GetDateTime(3).ToString("yyyy-MM-dd"));
                monthCalendar.SetDate(DateTime.Parse(fNacimiento));

                ciudad = (r.IsDBNull(4) ? NOEXISTENTE : r.GetString(4)); 
                tCiudad.Text = ciudad;

                calle  = (r.IsDBNull(5) ? NOEXISTENTE : r.GetString(5) ); 
                tCalle.Text = calle;

                num  = (r.IsDBNull(6) ? NOEXISTENTE: r.GetString(6) );
                tNumero.Text = num;

                cp = (r.IsDBNull(7) ? NOEXISTENTE : r.GetString(7)); ;
                tCodigoPostal.Text = cp;
                    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRORi: " + ex.Message);
            }
            finally
            {
                co.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tContrasena.Text == null || tNContrasena == null)  
            {
                MessageBox.Show("ERROR: " + "El campo no de contrasena no puede ser vacio");
            }else if(tContrasena.Text != user.Pass ){

                MessageBox.Show("ERROR: " + "La contrasna no es correcta");
            }
            else
            {
                string connection_data = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";
                MySqlConnection co = null;

                try
                {
                    co = new MySqlConnection(connection_data);
                    co.Open();
                    string sql = "Update customer SET password='" + tNContrasena.Text + "' where NIF='" +user.Nif+"'";
                    var command = new MySqlCommand(sql, co);
                    command.ExecuteNonQuery();
                    
                    lResult.Visible = true;
                    lResult.Text = "Se ha establecido nueva contrasena";
                    timer1.Enabled=true;


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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lResult.Hide();
            timer1.Enabled = false; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
                //---------------------------------------------------

                if (!nombre.Equals(tNombre.Text)) updateSql("firstName",addSignString(tNombre.Text));
                if (!Apellido.Equals(tApellidos.Text)) updateSql("lastName", addSignString(tApellidos.Text));
                if (!cp.Equals(tCodigoPostal.Text)) updateSql("postalCode", addSignString(tCodigoPostal.Text));
                if (!calle.Equals(tCalle.Text)) updateSql("street", addSignString(tCalle.Text));
                if (!ciudad.Equals(tCalle.Text)) updateSql("city", addSignString(tCiudad.Text));
                if (!num.Equals(tNumero.Text)) updateSql("number", addSignString(tNumero.Text));
                if (!fNacimiento.Equals(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"))) updateSql("birthDate", addSignString(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd")));

                mostrarDatos();
          
        }

        private void updateSql(string p1, string p2)
        {
            string connection_data = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";
            MySqlConnection co = null;

            try
            {
                co = new MySqlConnection(connection_data);
                co.Open();
                string sql = "Update particular SET "+ p1 +" ="+ p2 +" where NIF='" + user.Nif + "'";
                Console.WriteLine(sql);
                var command = new MySqlCommand(sql, co);
                command.ExecuteNonQuery();
                
                /*lResult.Visible = true;
                lResult.Text = "Se ha establecido nueva contrasena";
                timer1.Enabled = true; */


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
        private string addSignString(string p)
        {
            return "'" + p + "'";
        }

        private void bHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bAlemania_Click(object sender, EventArgs e)
        {
            GermanyInfrom GI = new GermanyInfrom(user);
            this.Close();
            GI.ShowDialog();
        }

        private void bHolanda_Click(object sender, EventArgs e)
        {
            NetherlandsInform NI = new NetherlandsInform(user);
            this.Close();
            NI.ShowDialog();
        }
    }
}
