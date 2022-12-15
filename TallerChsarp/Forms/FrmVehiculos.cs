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
    public partial class FrmVehiculos : Form
    {
        bool status = false;
        public FrmVehiculos()
        {
            InitializeComponent();
        }

        #region Menu

        private void btnCliente_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            try
            {
                FrmClientes clientes = new FrmClientes();
                clientes.ShowDialog();
                this.Hide();
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
        //***********************************************************
        #region acciones
        private void button1_Click(object sender, EventArgs e)
        {
            limpiaCampos();
        }
        //***********************************************************
        private void button4_Click(object sender, EventArgs e)
        {
            if (ValidacionErrores() == false)
            {
                if (txtPlaca.Text != "")
                {
                    if (MessageBox.Show("¿Deseas registrar a " + txtPlaca.Text + "?", "Mensaje",
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
        //***********************************************************
        private void button3_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text != "")
            {
                if (MessageBox.Show("¿Deseas modificar a " + txtPlaca.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("2");
                    limpiaCampos();
                }
            }
            else
            {
                MessageBox.Show("Selecciona una vehiculo de la lista.", "Selecciona Empresa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //**********************************************************
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text != "")
            {
                if (MessageBox.Show("¿Deseas eliminar a " + txtPlaca.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("3");
                    limpiaCampos();
                }
            }
        }
        #endregion
        //*********************************************************
        #region Metodos generales
        private void limpiaCampos()
        {
            Vehiculos v = new Vehiculos();
            txtPlaca.Text = "";
            txtMarca.Text = "";
            txtaño.Text = "";
            txtModelo.Text = "";
            cmbEstado.SelectedIndex = -1;
            cmbCliente.SelectedIndex = -1;
            GvVehiculos.DataSource = v.D_listar_Vehiculo();
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

           Vehiculos v = new Vehiculos();
            v.Placa = txtPlaca.Text;
            v.Modelo = txtModelo.Text;
            v.Cedula = Convert.ToInt32(cmbCliente.SelectedValue);
            v.Año = Convert.ToInt32(txtaño.Text);
            v.Marca = txtMarca.Text;
            v.Estado = status;
            v.accion = accion;
            String msj = v.D_mantenimiento_Vehiculo();
            MessageBox.Show(msj, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //******************************************************************
        private bool ValidacionErrores()
        {
            if (string.IsNullOrEmpty(txtPlaca.Text))
            {
                MessageBox.Show("la placa es un campo requerido");
                return true;
            }

            if (string.IsNullOrEmpty(txtModelo.Text))
            {
                MessageBox.Show("el modelo es un campo requerido");
                return true;
            }

            if (string.IsNullOrEmpty(txtMarca.Text))
            {
                MessageBox.Show("La marca es un campo requerido");
                return true;
            }


            if (string.IsNullOrEmpty(txtaño.Text))
            {
                MessageBox.Show("El año es un campo requerido");
                return true;
            }

            if (cmbEstado.Text == "")
            {
                MessageBox.Show("Seleccione el estado del servicio");
                return true;
            }

            if (cmbCliente.Text == "")
            {
                MessageBox.Show("Seleccione el cliente del vehiculo");
                return true;
            }
            return false;
        }
        #endregion

        private void FrmVehiculos_Load(object sender, EventArgs e)
        {
            Vehiculos v = new Vehiculos();
            GvVehiculos.DataSource = v.D_listar_Vehiculo();

            cmbCliente.DisplayMember = "Nombre Completo";
            cmbCliente.ValueMember = "Cedula";
            cmbCliente.DataSource = v.D_listar_Cliente();
            cmbCliente.SelectedIndex = -1;

        }

        private void GvVehiculos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = e.RowIndex;
            DataGridViewRow row = GvVehiculos.Rows[fila];
            txtPlaca.Text = row.Cells[0].Value.ToString();
            txtMarca.Text = row.Cells[1].Value.ToString();
            txtModelo.Text = row.Cells[2].Value.ToString();
            txtaño.Text =row.Cells[3].Value.ToString();
            cmbCliente.Text= row.Cells[5].Value.ToString();
            cmbEstado.Text = row.Cells[6].Value.ToString();
        }
    }
}
