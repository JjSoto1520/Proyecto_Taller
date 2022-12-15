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
    public class Mantenimiento
    {
        public int ID_Mantenimiento { get; set; }

        public string Placa { get; set; }

        public int IDServicio { get; set; }

        public DateTime Fecha_Ingreso { get; set; }

        public DateTime Fecha_Egreso { get; set; }

        public double Total_Pagar { get; set; }

        public string UsuarioEntrega { get; set; }

        public string accion { get; set; }

        public int? Cliente { get; set; }

        ConexionDB Conexion = new ConexionDB();
        private SqlCommand cmdTaller = new SqlCommand();
        private SqlDataReader LeerFilas;


        public DataTable D_listar_Servicio()
        {
            cmdTaller.Connection = Conexion.AbrirConexion();
            cmdTaller.CommandText = "SComboServicios";
            cmdTaller.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmdTaller);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Conexion.CerrarConexion();
            return dt;
        }

        public DataTable D_listar_Taller()
        {
            cmdTaller.Connection = Conexion.AbrirConexion();
            cmdTaller.CommandText = "SViewTaller";
            cmdTaller.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmdTaller);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Conexion.CerrarConexion();
            return dt;
        }



        public String D_mantenimiento_Taller ()
        {
            cmdTaller.Connection = Conexion.AbrirConexion();
            cmdTaller = new SqlCommand("SMantenimientoTaller", Conexion.Conexion);
            cmdTaller.CommandType = CommandType.StoredProcedure;
            cmdTaller.Parameters.Add(new SqlParameter("@ID_Mantenimiento", ID_Mantenimiento));
            cmdTaller.Parameters.Add(new SqlParameter("@Placa", Placa));
            cmdTaller.Parameters.Add(new SqlParameter("@IDServicio", IDServicio));
            cmdTaller.Parameters.Add(new SqlParameter("@Fecha_Ingreso", Fecha_Ingreso));
            cmdTaller.Parameters.Add(new SqlParameter("@Fecha_Egreso", Fecha_Egreso));
            cmdTaller.Parameters.Add(new SqlParameter("@Total_Pagar", Total_Pagar));
            cmdTaller.Parameters.Add(new SqlParameter("@UsuarioEntrega", UsuarioEntrega));
            cmdTaller.Parameters.Add(new SqlParameter("@Cliente", Cliente));
            cmdTaller.Parameters.Add("@accion", SqlDbType.VarChar, 50).Value = accion;
            cmdTaller.Parameters["@accion"].Direction = ParameterDirection.InputOutput;
            if (Conexion.Conexion.State == ConnectionState.Open) Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            cmdTaller.ExecuteNonQuery();
            accion = cmdTaller.Parameters["@accion"].Value.ToString();
            Conexion.CerrarConexion();
            return accion;
        }






        public string[] validar(int? ID)
        {
            string[] arrproducto = new string[8];
            Boolean ban = true;
            //validacion de datos
            try
            {
                Conexion.AbrirConexion();

                cmdTaller = new SqlCommand("select  c.Nombre,c.Apellido_1,c.Apellido_2,v.Placa,v.Modelo,v.Marca,v.Año from Cliente c,ClienteXVehiculo cv,Vehiculo v where c.Cedula = cv.Cedula and v.Placa = v.Placa and c.Cedula = " + ID + "", Conexion.Conexion);
                SqlDataReader reader = cmdTaller.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                    
                        arrproducto[0] = reader["Nombre"].ToString().Trim();
                        arrproducto[1] = reader["Apellido_1"].ToString().Trim();
                        arrproducto[2] = reader["Apellido_2"].ToString().Trim();
                        arrproducto[3] = reader["Placa"].ToString().Trim();
                        arrproducto[4] = reader["Modelo"].ToString().Trim();
                        arrproducto[5] = reader["Marca"].ToString().Trim();
                        arrproducto[6] = reader["Año"].ToString().Trim();
                        arrproducto[7] = ban.ToString();
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
