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
    public partial class Register : Form
    {

        private bool registroCorrecto = true;

        public Register()
        {
            InitializeComponent();
        }

        private void bVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ocultarTodo()
        {
            panelCorporativo.Visible = false;
            panelParticular.Visible = false;
        }

        private void bRegistrarse_Click(object sender, EventArgs e)
        {
            if(!radioButtonCorporativo.Checked && !radioButtonParticular.Checked)
            {
                MessageBox.Show("Por favor, seleccione el tipo de cliente.");
            }

            if(radioButtonParticular.Checked)
            {
                //Comprueba que los campos no están vacíos
                if (tUsuario.TextLength == 0 || tContrasena.TextLength == 0 || tNIF.TextLength == 0 
                    || tNombre.TextLength == 0 || tApellidos.TextLength == 0 || tCiudad.TextLength == 0 
                    || tCalle.TextLength == 0 || tCodigoPostal.TextLength == 0 || tNumero.TextLength == 0)
                {
                    MessageBox.Show("Por favor, rellene los datos obligatorios.");
                }
                else
                {
                    registroCorrecto = true;
                    compruebaUsuario(tUsuario.Text);
                    compruebaNIF(int.Parse(tNIF.Text));
                    compruebaFechaNacimiento(monthCalendar.SelectionStart);

                    if(registroCorrecto)
                    {
                        MessageBox.Show("Te has registrado satisfactoriamente.");
                        this.Close();
                    }
                }

            }

            if (radioButtonCorporativo.Checked)
            {
                //Comprueba que los campos no están vacíos
                if (tUsuarioC.TextLength == 0 || tContrasenaC.TextLength == 0 || tNIFC.TextLength == 0
                    || tNombreC.TextLength == 0 || tCiudadC.TextLength == 0
                    || tCalleC.TextLength == 0 || tCodigoPostalC.TextLength == 0 || tNumeroC.TextLength == 0)
                {
                    MessageBox.Show("Por favor, rellene todo los campos.");
                }
                else
                {
                    registroCorrecto = true;
                    compruebaUsuario(tUsuarioC.Text);
                    int n = int.Parse(tNIFC.Text);
                    compruebaNIF(n);

                    if (registroCorrecto)
                    {
                        MessageBox.Show("Su empresa ha sido registrada satisfactoriamente.");
                        this.Close();
                    }
                }

            }
        }

        private void compruebaFechaNacimiento(DateTime d)
        {
            try
            {
                double dias = DateTime.Now.Subtract(d).TotalDays;
                if( (dias/365) < 18)
                {
                    registroCorrecto = false;
                    throw new Error("Los menores de edad no se pueden registrar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void compruebaNIF(int n)
        {
            string connection_data = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";
            MySqlConnection co = null;

            try
            {
                co = new MySqlConnection(connection_data);
                co.Open();
                string sql = "SELECT nif FROM particular;";
                var command = new MySqlCommand(sql, co);
                MySqlDataReader r = command.ExecuteReader();
                bool existe = false;
                while (r.Read())
                {
                    if (r.GetInt32(0) == n)
                    {
                        existe = true;
                    }
                }
                if (existe)
                {
                    registroCorrecto = false;
                    throw new Error("Ya existe una cuenta asociada a este NIF.");
                }
                else
                {
                    // Cuando exista la nueva base de datos, aquí añadiremos el usuario a la BD
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

        private void compruebaUsuario(String u)
        {
            string connection_data = "server=eburyrequisitos.cobadwnzalab.eu-central-1.rds.amazonaws.com;user=grupo03;database=grupo03DB;port=3306;password=2zzd92Xe7sr4BRxW";
            MySqlConnection co = null;

            try
            {
                co = new MySqlConnection(connection_data);
                co.Open();
                string sql = "SELECT username FROM customer;";
                var command = new MySqlCommand(sql, co);
                MySqlDataReader r = command.ExecuteReader();
                bool existe = false;
                while(r.Read())
                {
                    if(r.GetString(0) == u)
                    {
                        existe = true;
                    }
                }
                if(existe)
                {
                    registroCorrecto = false;
                    throw new Error("Ya existe una cuenta con este nombre de usuario. Por favor, eliga otro.");
                }
                else
                {
                    // Cuando exista la nueva base de datos, aquí añadiremos el usuario a la BD
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

        private void radioButtonCorporativo_CheckedChanged(object sender, EventArgs e)
        {
            ocultarTodo();
            panelCorporativo.Visible = true;
            bRegistrarse.Visible = true;
        }

        private void radioButtonParticular_CheckedChanged(object sender, EventArgs e)
        {
            ocultarTodo();
            panelParticular.Visible = true;
            bRegistrarse.Visible = true;
        }
    }
}
