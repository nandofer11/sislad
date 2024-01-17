using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Conexion
    {
        MySqlConnection conexion;
        MySqlTransaction transaccion;

        public void AbrirConexion()
        {
            try
            {
                conexion = new MySqlConnection();
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["conexion_bd_sislad"].ToString();
                conexion.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CerrarConexion()
        {
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MySqlCommand CrearComandoSQL(String sentenciaSQL)
        {
            try
            {
                MySqlCommand comando = conexion.CreateCommand();
                if (transaccion != null)
                    comando.Transaction = transaccion;
                comando.CommandText = sentenciaSQL;
                comando.CommandType = CommandType.Text;
                return comando;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public MySqlDataReader EjecutarConsulta(String sentenciaSQL)
        {
            try
            {
                MySqlCommand comando = conexion.CreateCommand();
                if (transaccion != null)
                    comando.Transaction = transaccion;
                comando.CommandText = sentenciaSQL;
                comando.CommandType = CommandType.Text;
                MySqlDataReader resultado = comando.ExecuteReader();
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
