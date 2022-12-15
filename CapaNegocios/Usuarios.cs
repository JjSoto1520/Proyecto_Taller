using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
namespace CapaNegocios
{
    public class Usuarios
    {
		public string Nombre { get; set; }
        public string Apellido_1 { get; set; }
        public string Apellido_2 { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public int Cedula { get; set; }
        public string password { get; set; }
        public bool Estado { get; set; }
        public string accion { get; set; }

        ConexionDB Conexion = new ConexionDB();
        private SqlCommand cmdClientes = new SqlCommand();
        private SqlCommand cmdClientes1 = new SqlCommand();


        #region "metodos"
        public bool ValidateLogin()
        {
            cmdClientes.Connection = Conexion.AbrirConexion();
            cmdClientes.CommandText = "LoginCliente";
            cmdClientes.Parameters.Add(new SqlParameter("@Cedula", Cedula));
            cmdClientes.Parameters.Add(new SqlParameter("@Contrasena", password));
            cmdClientes.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmdClientes);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Conexion.CerrarConexion();
                return true;
            }
            else
            {
                Conexion.CerrarConexion();
                return false;
            }                 
        }

        public DataTable D_listar_Usuario()
        {
            cmdClientes.Connection = Conexion.AbrirConexion();
            cmdClientes.CommandText = "SViewClientes";
            cmdClientes.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmdClientes);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Conexion.CerrarConexion();
            return dt;
        }

        public String D_mantenimiento_Usuarios()
        {
            cmdClientes.Connection = Conexion.AbrirConexion();
            cmdClientes = new SqlCommand("SMantenimientoClientes", Conexion.Conexion);
            cmdClientes.CommandType = CommandType.StoredProcedure;
            cmdClientes.Parameters.Add(new SqlParameter("@Cedula", Cedula));
            cmdClientes.Parameters.Add(new SqlParameter("@Nombre", Nombre));
            cmdClientes.Parameters.Add(new SqlParameter("@Apellido_1", Apellido_1));
            cmdClientes.Parameters.Add(new SqlParameter("@Apellido_2", Apellido_2));
            cmdClientes.Parameters.Add(new SqlParameter("@Direccion", Direccion));
            cmdClientes.Parameters.Add(new SqlParameter("@Contrasena",password));
            cmdClientes.Parameters.Add(new SqlParameter("@Estado", Estado));
            cmdClientes.Parameters.Add("@accion", SqlDbType.VarChar, 50).Value = accion;
            cmdClientes.Parameters["@accion"].Direction = ParameterDirection.InputOutput;
            if (Conexion.Conexion.State == ConnectionState.Open) Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            cmdClientes.ExecuteNonQuery();
            accion = cmdClientes.Parameters["@accion"].Value.ToString();
            Conexion.CerrarConexion();
            return accion;
        }

        public String D_mantenimiento_Telefono()
        {
            cmdClientes.Connection = Conexion.AbrirConexion();
            cmdClientes = new SqlCommand("SMantenimientoTelefono", Conexion.Conexion);
            cmdClientes.CommandType = CommandType.StoredProcedure;
            cmdClientes.Parameters.Add(new SqlParameter("@Cedula", Cedula));
            cmdClientes.Parameters.Add(new SqlParameter("@telefono", Telefono));
            cmdClientes.Parameters.Add("@accion", SqlDbType.VarChar, 50).Value = accion;
            cmdClientes.Parameters["@accion"].Direction = ParameterDirection.InputOutput;
            if (Conexion.Conexion.State == ConnectionState.Open) Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            cmdClientes.ExecuteNonQuery();
            accion = cmdClientes.Parameters["@accion"].Value.ToString();
            Conexion.CerrarConexion();
            return accion;
        }


        #endregion
        //*************************************
    }
}
