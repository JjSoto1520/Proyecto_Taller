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
    public class Vehiculos
    {
        public int Cedula { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public bool Estado { get; set; }
        public string accion { get; set; }
        ConexionDB Conexion = new ConexionDB();
        private SqlCommand cmdVehiculo = new SqlCommand();
        private SqlDataReader LeerFilas;

        public DataTable D_listar_Vehiculo()
        {
            cmdVehiculo.Connection = Conexion.AbrirConexion();
            cmdVehiculo.CommandText = "SViewVehiculos";
            cmdVehiculo.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmdVehiculo);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Conexion.CerrarConexion();
            return dt;
        }

        public DataTable D_listar_Cliente()
        {
            cmdVehiculo.Connection = Conexion.AbrirConexion();
            cmdVehiculo.CommandText = "SComboClientes";
            cmdVehiculo.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmdVehiculo);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Conexion.CerrarConexion();
            return dt;
        }



        public String D_mantenimiento_Vehiculo()
        {
            cmdVehiculo.Connection = Conexion.AbrirConexion();
            cmdVehiculo = new SqlCommand("SMantenimientoVehiculos", Conexion.Conexion);
            cmdVehiculo.CommandType = CommandType.StoredProcedure;
            cmdVehiculo.Parameters.Add(new SqlParameter("@Placa", Placa));
            cmdVehiculo.Parameters.Add(new SqlParameter("@Marca", Marca));
            cmdVehiculo.Parameters.Add(new SqlParameter("@Modelo", Modelo));
            cmdVehiculo.Parameters.Add(new SqlParameter("@Año", Año));
            cmdVehiculo.Parameters.Add(new SqlParameter("@Estado", Estado));
            cmdVehiculo.Parameters.Add(new SqlParameter("@cedula", Cedula));
            cmdVehiculo.Parameters.Add("@accion", SqlDbType.VarChar, 50).Value = accion;
            cmdVehiculo.Parameters["@accion"].Direction = ParameterDirection.InputOutput;
            if (Conexion.Conexion.State == ConnectionState.Open) Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            cmdVehiculo.ExecuteNonQuery();
            accion = cmdVehiculo.Parameters["@accion"].Value.ToString();
            Conexion.CerrarConexion();
            return accion;
        }

    }
}
