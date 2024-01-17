using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class CargoUsuarioControlador
    {
        Conexion conexion;
        CargoDAO cargoDAO;

        public CargoUsuarioControlador()
        {
            this.conexion = new Conexion();
            this.cargoDAO = new CargoDAO(conexion);
        }

        public List<CargoUsuario> ListarCargosUsuarios(string nombre)
        {
            try
            {
                conexion.AbrirConexion();
                List<CargoUsuario> listaCargosUsuario = cargoDAO.ListarCargosUsuario(nombre);
                conexion.CerrarConexion();
                return listaCargosUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
