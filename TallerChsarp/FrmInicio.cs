using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TallerChsarp.Forms;

namespace TallerChsarp
{
    public partial class FrmInicio : Form
    {
        //Fields
        //private Button currentButton;
        //private Random random;
        //private int tempIndex;
        //private Form activeForm;
        public FrmInicio()
        {
            InitializeComponent();
            //random = new Random();
            //btnCloseChildForm.Visible = false;
            //this.Text = string.Empty;
            //this.ControlBox = false;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        // load 
        private void FrmInicio_Load(object sender, EventArgs e)
        {

        }
        //botones del menu
        #region "Botones del Menu"
        private void button1_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmClientes clientes=new FrmClientes();
                this.Hide();
                clientes.ShowDialog();
                
            }
            catch (Exception ex)
            { 
                // atrapa el error en la consola
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error al cargar la pantalla de clientes","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmVehiculos vehiculos =new FrmVehiculos(); 
                this.Hide();
                vehiculos.ShowDialog();
                
            }
            catch (Exception ex)
            {
                // atrapa el error en la consola
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error al cargar la pantalla de Vehiculos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmServicios frmServicios =new FrmServicios();
                this.Hide();
                frmServicios.ShowDialog();
                  
            }
            catch (Exception ex)
            {
                // atrapa el error en la consola
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error al cargar la pantalla de servicios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmMantenimiento mantenimiento =new FrmMantenimiento();
                this.Hide();
                mantenimiento.ShowDialog();
            }
            catch (Exception ex)
            {
                // atrapa el error en la consola
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error al cargar la pantalla de mantenimientos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmAcercaDe frmAcercaDe =new FrmAcercaDe();
                frmAcercaDe.ShowDialog();
            }
            catch (Exception ex)
            {
                // atrapa el error en la consola
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error al cargar la pantalla de Acerca De", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------
        private void button6_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                Inicio frmLogin=new Inicio();
                this.Hide();
               frmLogin.ShowDialog();
            }
            catch (Exception ex)
            {
                // atrapa el error en la consola
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error para cerrar sesion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------
        #endregion
    }
}
