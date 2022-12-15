using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
namespace CapaNegocios
{
    public class Servicios
    {
        #region variables
        public int IDServicio { get; set; }

        public string Descripcion { get; set; }

        public double? Costo { get; set; }

        public bool Estado { get; set; }

        public string accion { get; set; }

        public int Cedula { get; set; }

        ConexionDB Conexion = new ConexionDB();
        private SqlCommand cmdServicio = new SqlCommand();
        private SqlDataReader LeerFilas;

        #endregion

        public DataTable D_listar_Servicio()
        {
            cmdServicio.Connection = Conexion.AbrirConexion();
            cmdServicio.CommandText = "SViewServicios";
            cmdServicio.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmdServicio);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Conexion.CerrarConexion();
            return dt;
        }

        public String D_mantenimiento_Servicio()
        {
            cmdServicio.Connection = Conexion.AbrirConexion();
            cmdServicio = new SqlCommand("SMantenimientoServicios", Conexion.Conexion);
            cmdServicio.CommandType = CommandType.StoredProcedure;
            cmdServicio.Parameters.Add(new SqlParameter("@IDServicio", IDServicio));
            cmdServicio.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
            cmdServicio.Parameters.Add(new SqlParameter("@Costo", Costo));
            cmdServicio.Parameters.Add(new SqlParameter("@Estado", Estado));
            cmdServicio.Parameters.Add("@accion", SqlDbType.VarChar, 50).Value = accion;
            cmdServicio.Parameters["@accion"].Direction = ParameterDirection.InputOutput;
            if (Conexion.Conexion.State == ConnectionState.Open) Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            cmdServicio.ExecuteNonQuery();
            accion = cmdServicio.Parameters["@accion"].Value.ToString();
            Conexion.CerrarConexion();
            return accion;
        }

        public void cargar(int ID)
        {
            //validacion de datos 
            try
            {
                string[] prod = new string[2];
                prod = mvalidar(ID);
                DataSet ds = new DataSet();
                Costo = Convert.ToInt32(prod[0].ToString());
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string[] mvalidar(int ID)
        {
            string[] arrproducto = new string[2];
            Boolean ban = true;
            //validacion de datos
            try
            {
                Conexion.AbrirConexion();
                 cmdServicio = new SqlCommand("select s.Costo from Servicios s where s.IDServicio='" + ID + "'", Conexion.Conexion);
                SqlDataReader reader =  cmdServicio.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        arrproducto[0] = reader["Costo"].ToString().Trim();
                        arrproducto[1] = ban.ToString();
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
                Conexion.CerrarConexion();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return arrproducto;
        }
    }


}
