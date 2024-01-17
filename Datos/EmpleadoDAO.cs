using Dominio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class EmpleadoDAO
    {
        private Conexion conexion;

        public EmpleadoDAO(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public int RegistrarEmpleado(Empleado empleado)
        {
            int registros_afectados;
            string sentenciaSQL = "INSERT INTO empleado (dni, nombres, apellidos, ciudad, direccion, telefono, pago_dia, fecha_contrato, fecha_fin_contrato, estado) VALUES (@dni, @nombres, @apellidos, @ciudad, @direccion, @telefono, @pago_dia, @fecha_contrato, @fecha_fin_contrato, @estado)";
            try
            {
                MySqlCommand comando = conexion.CrearComandoSQL(sentenciaSQL);
                AsignarParametros(empleado, comando);
                return registros_afectados = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ModificarEmpleado(Empleado empleado)
        {
            int registros;
            String sentenciaSQL = "update empleado set dni = @dni, nombres = @nombres, apellidos = @apellidos, ciudad = @ciudad, direccion = @direccion, telefono = @telefono, pago_dia = @pago_dia, fecha_contrato = @fecha_contrato, fecha_fin_contrato = @fecha_fin_contrato, estado = @estado  where id_empleado = @id_empleado";
            try
            {
                MySqlCommand comando = conexion.CrearComandoSQL(sentenciaSQL);
                AsignarParametros(empleado, comando);

                comando.Parameters.AddWithValue("@id_empleado", empleado.Id_empleado);
                registros = comando.ExecuteNonQuery();
                return registros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EliminarEmpleado(Empleado empleado)
        {
            int registros;
            string sentenciaSQL = "delete from empleado where id_empleado = @id_empleado";
            try
            {
                MySqlCommand comando = conexion.CrearComandoSQL(sentenciaSQL);
                comando.Parameters.AddWithValue("@id_empleado", empleado.Id_empleado);
                registros = comando.ExecuteNonQuery();
                return registros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AsignarParametros(Empleado empleado, MySqlCommand comando)
        {
            comando.Parameters.AddWithValue("@dni", empleado.Dni);
            comando.Parameters.AddWithValue("@nombres", empleado.Nombres);
            comando.Parameters.AddWithValue("@apellidos", empleado.Apellidos);
            comando.Parameters.AddWithValue("@ciudad", empleado.Ciudad);
            comando.Parameters.AddWithValue("@direccion", empleado.Direccion);
            comando.Parameters.AddWithValue("@telefono", empleado.Telefono);
            comando.Parameters.AddWithValue("@pago_dia", empleado.Pago_dia);
            comando.Parameters.AddWithValue("@fecha_contrato", empleado.Fecha_contrato);
            comando.Parameters.AddWithValue("@fecha_fin_contrato", empleado.Fecha_fin_contrato);
            comando.Parameters.AddWithValue("@estado", empleado.Estado);
        }

        public List<Empleado> ListarEmpleados(string nombre)
        {
            List<Empleado> listaEmpleados = new List<Empleado>();
            Empleado empleado;
            String sentenciaSQL = "select id_empleado, dni, nombres, apellidos, ciudad, direccion, telefono, pago_dia, fecha_contrato, fecha_fin_contrato, estado from Empleado where nombres like '%" + nombre + "%' order by nombres";
            try
            {
                MySqlDataReader resultado = conexion.EjecutarConsulta(sentenciaSQL);
                while (resultado.Read())
                {
                    empleado = CrearObjetoEmpleado(resultado);
                    listaEmpleados.Add(empleado);
                }
                resultado.Close();
                return listaEmpleados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Empleado BuscarEmpleadoPorId(int id_empleado)
        {
            Empleado empleado = null;
            string sentenciaSQL = "SELECT id_empleado, dni, nombres, apellidos, ciudad, direccion, telefono, pago_dia, fecha_contrato, fecha_fin_contrato, estado FROM empleado WHERE id_empleado =  '"+ id_empleado +"'";
            try
            {
                MySqlDataReader resultado = conexion.EjecutarConsulta(sentenciaSQL);
                if (resultado.Read())
                {
                    empleado = CrearObjetoEmpleado(resultado);
                }
                resultado.Close();
                return empleado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Empleado CrearObjetoEmpleado(MySqlDataReader resultado)
        {
            Empleado empleado = new Empleado();
            empleado.Id_empleado = resultado.GetInt32(0);
            empleado.Dni = resultado.GetString(1);
            empleado.Nombres = resultado.GetString(2);
            empleado.Apellidos = resultado.GetString(3);
            empleado.Ciudad = resultado.GetString(4);
            empleado.Direccion = resultado.GetString(5);
            empleado.Telefono = resultado.GetString(6);
            empleado.Pago_dia = resultado.GetDouble(7);
            empleado.Fecha_contrato = resultado.GetDateTime(8).Date;
            empleado.Fecha_fin_contrato = resultado.GetDateTime(9).Date;
            empleado.Estado = resultado.GetString(10);
            return empleado;
        }
    }
}
