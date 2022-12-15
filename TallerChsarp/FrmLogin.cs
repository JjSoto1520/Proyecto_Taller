using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocios;



namespace TallerChsarp
{
    public partial class Inicio : Form
    {
        Usuarios users = new Usuarios();


        public Inicio()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                Application.Exit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                if (string.IsNullOrEmpty(txtCedula.Text) && string.IsNullOrEmpty(txtContrasena.Text))
                {
                    MessageBox.Show("Campos son requeridos","Campos vacios",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    users.Cedula = Convert.ToInt32(txtCedula.Text);
                    users.password = txtContrasena.Text;
                    if (users.ValidateLogin()==true)
                    {
                        FrmInicio frm = new FrmInicio();
                        this.Hide();
                        frm.ShowDialog();
                        
                    }
                    else
                    {
                        MessageBox.Show("Usuario y/o Contraseña incorrecto", "usuario Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtContrasena.Clear();
                    }
                }
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }
    }
}
