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
    public partial class FrmServicios : Form
    {
        Servicios s=new Servicios();
        bool status = false;
        public FrmServicios()
        {
            InitializeComponent();
        }


        #region Botones de menu
        //**************************************************************
        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            FrmMantenimiento m = new FrmMantenimiento();
            this.Hide();
            m.Show();
        }
        //*************************************************************************
        private void button1_Click(object sender, EventArgs e)
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
        //*********************************************************************
        private void button2_Click(object sender, EventArgs e)
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
        //*********************************************************************
        private void btnServicio_Click(object sender, EventArgs e)
        {

        }


        //*********************************************************************
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
        //***********************************************************************

        #endregion"

        //****************************************************************
        // boton de acciones para el sistema. 
        #region "Acciones del modulo"
        private void button10_Click(object sender, EventArgs e)
        {
            limpiaCampos();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidacionErrores() == false)
                {
                    if (txtId.Text != "")
                    {
                        if (MessageBox.Show("¿Deseas registrar a " + txtServicio.Text + "?", "Mensaje",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                        {
                            mantenimiento("1");
                            limpiaCampos();
                            txtId.Enabled = false;
                        }
                        else
                        {
                            limpiaCampos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        //****************************************************************************************************
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                if (MessageBox.Show("¿Deseas modificar a " + txtServicio.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("2");
                    limpiaCampos();
                }
            }
            else
            {
                MessageBox.Show("Selecciona una servicio de la lista.", "Selecciona servicio",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //******************************************************************************************************
        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                if (MessageBox.Show("¿Deseas eliminar a " + txtServicio.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("3");
                    limpiaCampos();
                }
            }
        }
        #endregion
        #region metodos generales
            #region "limpiar campos"
        private void limpiaCampos()
            {
                Servicios s = new Servicios();
                txtId.Text = "";
                txtServicio.Text = "";
                txtCosto.Text = "";
                cmbEstado.SelectedIndex = -1;
                GvServicios.DataSource = s.D_listar_Servicio();
                txtId.Enabled = true;

            }
        #endregion
            //-----------------------------
            #region validacion de campos
            private bool ValidacionErrores()
            {
                if (string.IsNullOrEmpty(txtServicio.Text))
                {
                    MessageBox.Show("el servicio es un campo requerido");
                    return true;
                }

                if (string.IsNullOrEmpty(txtCosto.Text))
                {
                    MessageBox.Show("El costo es un campo requerido");
                    return true;
                }

                if (cmbEstado.Text=="Seleccione")
                {
                    MessageBox.Show("Seleccione el estado del servicio");
                    return true;
                }

                return false;
            }
        //--------------------------------------------------------
        #endregion
        //-----------------------------
            #region Mantenimiento
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

            Servicios s = new Servicios();
            s.IDServicio= Convert.ToInt32(txtId.Text);
            s.Descripcion = txtServicio.Text;
            s.Costo = Convert.ToInt32(txtCosto.Text);
            s.Estado = status;
            s.accion = accion;
            String msj = s.D_mantenimiento_Servicio();
            MessageBox.Show(msj, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmServicios_Load(object sender, EventArgs e)
        {
            Servicios s = new Servicios();
            GvServicios.DataSource = s.D_listar_Servicio();
        }

        private void GvServicios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = e.RowIndex;
            DataGridViewRow row = GvServicios.Rows[fila];
            txtId.Text = row.Cells[0].Value.ToString();
            txtServicio.Text = row.Cells[1].Value.ToString();
            txtCosto.Text = row.Cells[2].Value.ToString();
            cmbEstado.Text = row.Cells[3].Value.ToString();
        }
        #endregion

        #endregion
        //*****************************************************************
    }
}
