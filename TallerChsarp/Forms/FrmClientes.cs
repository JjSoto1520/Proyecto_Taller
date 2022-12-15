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
namespace TallerChsarp.Forms
{
    public partial class FrmClientes : Form
    {
        bool status = false;
        public FrmClientes()
        {
            InitializeComponent();
        }
        //******************************************************************************
        #region Menu
        private void btnCliente_Click(object sender, EventArgs e)
        {
        }
        //****************************************************************************
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
        //*******************************************************************************
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
        //********************************************************************************
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
        //**********************************************************************************
        //**********************************************************************************
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
        //************************************************************************************
        #endregion
        //----------------------------------------------------------------
        #region Acciones

        private void button10_Click(object sender, EventArgs e)
        {
            limpiaCampos();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidacionErrores() == false)
            {
                if (txtId.Text != "")
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
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                if (MessageBox.Show("¿Deseas modificar a " + txtId.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("2");
                    limpiaCampos();
                }
            }
            else
            {
                MessageBox.Show("Selecciona una clientes de la lista.", "Selecciona Cliente",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                if (MessageBox.Show("¿Deseas eliminar a " + txtId.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("3");
                    limpiaCampos();
                }
            }
        }

        #endregion
        //****************************************************************************
        #region Metodos generales
        private void limpiaCampos()
        {
            Usuarios c = new Usuarios();
            txtId.Text = "";
            txtNombre.Text = "";
            txtApellido1.Text = "";
            txtApellido2.Text = "";
            txtDireccion.Text = "";
            txttelefono.Text = "";
            txtContrasena.Text = "";
            cmbEstado.SelectedIndex = -1;
            GvClientes.DataSource =c.D_listar_Usuario();
        }
        void mantenimiento(String accion)
        {
            if (cmbEstado.SelectedIndex == 0)
            {
                status = true;
            }
            else if (cmbEstado.SelectedIndex == 1)
            {
                status = false;
            }

            Usuarios c = new Usuarios();
            //EmpresasE objent = new EmpresasE();
           c.Cedula= Convert.ToInt32(txtId.Text);
           c.Nombre = txtNombre.Text;
           c.Apellido_1 = txtApellido1.Text;
           c.Apellido_2 = txtApellido2.Text;
           c.Direccion = txtDireccion.Text;
           //c.Telefono = Convert.ToInt32(txttelefono.Text);
           c.password = txtContrasena.Text;
           c.Estado = status;
           c.accion = accion;
            String msj =c.D_mantenimiento_Usuarios();
            //c.D_mantenimiento_Telefono(); ;
            MessageBox.Show(msj, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //******************************************************************
        private bool ValidacionErrores()
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("la cedula es un campo requerido");
                return true;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("el nombre es un campo requerido");
                return true;
            }

            if (string.IsNullOrEmpty(txtApellido1.Text))
            {
                MessageBox.Show("El primer apellido  es un campo requerido");
                return true;
            }


            if (string.IsNullOrEmpty(txtApellido2.Text))
            {
                MessageBox.Show("El segundo apellido es un campo requerido");
                return true;
            }


            if (string.IsNullOrEmpty(txtContrasena.Text))
            {
                MessageBox.Show("la contraseña es un campo requerido");
                return true;
            }


            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                MessageBox.Show("La direccion es un campo requerido");
                return true;
            }


            if (string.IsNullOrEmpty(txttelefono.Text))
            {
                MessageBox.Show("El telefono es un campo requerido");
                return true;
            }


            if (cmbEstado.Text == "Seleccione")
            {
                MessageBox.Show("Seleccione el estado del servicio");
                return true;
            }

            return false;
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            Usuarios c = new Usuarios();
            GvClientes.DataSource = c.D_listar_Usuario();
            
        }

        private void GvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = e.RowIndex;
            DataGridViewRow row = GvClientes.Rows[fila];
            txtId.Text = row.Cells[0].Value.ToString();
            txtNombre.Text = row.Cells[1].Value.ToString();
            txtApellido1.Text = row.Cells[2].Value.ToString();
            txtApellido2.Text = row.Cells[3].Value.ToString();
            txtDireccion.Text = row.Cells[4].Value.ToString();
            cmbEstado.Text = row.Cells[5].Value.ToString();
        }
        #endregion
        //****************************************************************************

    }
}
