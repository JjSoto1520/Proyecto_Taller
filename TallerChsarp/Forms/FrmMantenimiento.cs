using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocios;
namespace TallerChsarp.Forms
{
    public partial class FrmMantenimiento : Form
    {
        Mantenimiento c = new Mantenimiento();
        Usuarios u = new Usuarios();
        Vehiculos v = new Vehiculos();
        public FrmMantenimiento()
        {
            InitializeComponent();
        }
        #region menu 
        private void btnCliente_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmClientes clientes = new FrmClientes();
                this.Hide();
                clientes.ShowDialog();
                
            }
            catch (Exception ex)
            {
                // atrapa el error en la consola
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error al cargar la pantalla de clientes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVehiculo_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmVehiculos vehiculos = new FrmVehiculos();
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

        private void btnServicio_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmServicios frmServicios = new FrmServicios();
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

        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmMantenimiento mantenimiento = new FrmMantenimiento();
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

        private void btnInicio_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmInicio i = new FrmInicio();
                this.Hide();
                i.Show();
            }
            catch (Exception ex)
            {
                // atrapa el error en la consola
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error al cargar la pantalla de Inicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Metodos generales
        private void limpiaCampos()
        {
            Mantenimiento c = new Mantenimiento();
            txtId.Text = "";
            txtNombre.Text = "";
            txtApellido1.Text = "";
            txtApellido2.Text = "";
            txtModelo.Text = "";
            ttxtTotal.Text = "";

            txtPlaca.Text = "";
            txtModelo.Text = "";
            txtaño.Text = "";
            txtMarca.Text = "";
            cmbServicio.SelectedIndex = -1;
            GvServicios.DataSource = c.D_listar_Taller();
        }

        void mantenimiento(String accion)
        {
            
            Mantenimiento c = new Mantenimiento();

            if (string.IsNullOrEmpty(txtId.Text))
            {
                c.UsuarioEntrega = txtUsuario.Text;
                c.Cliente = null;

                

            }
            else if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                c.Cliente = Convert.ToInt32(txtId.Text);
                c.UsuarioEntrega = "";
            }
            c.Placa = txtPlaca.Text;
            c.IDServicio = Convert.ToInt32(cmbServicio.SelectedValue);
            c.Fecha_Ingreso = Convert.ToDateTime(dtIngreso.Value);
            c.Fecha_Egreso = Convert.ToDateTime(dtRegreso.Value);
            c.Total_Pagar = Convert.ToDouble(ttxtTotal.Text);
            c.accion = accion;
            
            String msj = c.D_mantenimiento_Taller();
            MessageBox.Show(msj, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        ////******************************************************************
        private void SeleccionCombos()
        {
            txtId.Clear();
            txtUsuario.Clear();
            if (rbCliente.Checked == true)
            {
                txtId.Enabled = true;
                txtUsuario.Enabled = false;
                btnC.Enabled = true;
                txtPlaca.Enabled=true;
            }
            else if (rbUsuario.Checked == true)
            {
                txtId.Enabled = false;
                txtUsuario.Enabled = true;
                btnC.Enabled = false;
                txtPlaca.Enabled = true;
            }
        }

        public void cargar(int? ID)
        {
            //validacion de datos 
            try
            {
                
                string[] prod = new string[8];
                prod = c.validar(ID);
                DataSet ds = new DataSet();

                u.Nombre = prod[0].ToString();
                u.Apellido_1 = prod[1].ToString();
                u.Apellido_2 = prod[2].ToString();
                v.Placa = prod[3].ToString();
                v.Modelo = prod[4].ToString();
                v.Marca = prod[5].ToString();
                v.Año = Convert.ToInt32(prod[6].ToString());
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        //*******************************************************************
        #region Seleccion Clientes
        private void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            SeleccionCombos();
            limpiaCampos();
        }
        private void rbUsuario_CheckedChanged(object sender, EventArgs e)
        {
            SeleccionCombos();
            limpiaCampos();
        }
        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {   
                if (txtId.Text != "" || txtUsuario.Text!="" )
                {
                    if (MessageBox.Show("¿Deseas registrar a " + txtId.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        mantenimiento("1");
                        limpiaCampos();
                    }
                    else
                    {
                        limpiaCampos();
                    }
                }
        }

        private void FrmMantenimiento_Load(object sender, EventArgs e)
        {
            Mantenimiento c = new Mantenimiento();
            GvServicios.DataSource = c.D_listar_Taller();

            cmbServicio.DisplayMember = "Descripcion";
            cmbServicio.ValueMember = "IDServicio";
            cmbServicio.DataSource = c.D_listar_Servicio();
            cmbServicio.SelectedIndex = -1;
            btnC.Enabled = false;
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void btnC_Click(object sender, EventArgs e)
        {
            try
            {
                c.Cliente = Convert.ToInt32(txtId.Text);
                cargar(c.Cliente);
                txtNombre.Text = u.Nombre;
                txtApellido1.Text = u.Apellido_1;
                txtApellido2.Text = u.Apellido_2;
                txtPlaca.Text = v.Placa;
                txtModelo.Text = v.Modelo;
                txtMarca.Text = v.Marca;
                txtaño.Text = Convert.ToString(v.Año);
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Servicios servicios = new Servicios();
            int cantidad = Convert.ToInt32(numcantidad.Text);
            servicios.Costo = Convert.ToDouble(lblprecio.Text);
            double? total = 0;
            total = cantidad * servicios.Costo;
            ttxtTotal.Text = Convert.ToString(total);
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            Servicios servicios = new Servicios();
            servicios.IDServicio = Convert.ToInt32(cmbServicio.SelectedValue);
            servicios.cargar(servicios.IDServicio);
            lblprecio.Text = Convert.ToString(servicios.Costo);
        }
    }
}
