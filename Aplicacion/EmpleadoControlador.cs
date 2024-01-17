using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class EmpleadoControlador
    {
        private Conexion conexion;
        private EmpleadoDAO empleadoDAO;

        public EmpleadoControlador()
        {
            this.conexion = new Conexion();
            this.empleadoDAO = new EmpleadoDAO(conexion);
        }

        public int RegistrarEmpleado(Empleado empleado)
        {
            try
            {
                conexion.AbrirConexion();
                int registros_afectados = empleadoDAO.RegistrarEmpleado(empleado);
                conexion.CerrarConexion();
                return registros_afectados;
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        public int ModificarEmpleado(Empleado empleado)
        {
            try
            {
                conexion.AbrirConexion();
                int registros_afectados = empleadoDAO.ModificarEmpleado(empleado);
                conexion.CerrarConexion();
                return registros_afectados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EliminarEmpleado(Empleado empleado)
        {
            try
            {
                conexion.AbrirConexion();
                int registros = empleadoDAO.EliminarEmpleado(empleado);
                conexion.CerrarConexion();
                return registros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Empleado> ListarEmpleados(string nombre)
        {
            try
            {
                conexion.AbrirConexion();
                List<Empleado> listaEmpleados = empleadoDAO.ListarEmpleados(nombre);
                conexion.CerrarConexion();
                return listaEmpleados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Empleado BuscarEmpleadoPorId(int id_empleado)
        {
            try
            {
                conexion.AbrirConexion();
                Empleado empleado = empleadoDAO.BuscarEmpleadoPorId(id_empleado);
                conexion.CerrarConexion();
                return empleado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
