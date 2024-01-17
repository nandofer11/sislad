using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio;
using MySql.Data.MySqlClient;

namespace Datos
{
    public class CargoDAO
    {
        private Conexion conexion;

        public CargoDAO(Conexion conexion)
        {
            this.conexion = conexion;
        }

        //Listar todos los cargos que tiene la tabla cargo_usuario
        public List<CargoUsuario> ListarCargosUsuario(string nombre)
        {
            List<CargoUsuario> listaUsuarios = new List<CargoUsuario>();
            CargoUsuario cargoUsuario;
            String sentenciaSQL = "SELECT id_cargo_usuario, nombre_cargo from cargo_usuario where nombre_cargo like '%" + nombre + "%' order by nombre_cargo";
            try
            {
                MySqlDataReader resultado = conexion.EjecutarConsulta(sentenciaSQL);
                while (resultado.Read())
                {
                    cargoUsuario = CrearObjetoCargoUsuario(resultado);
                    listaUsuarios.Add(cargoUsuario);
                }
                resultado.Close();
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CargoUsuario CrearObjetoCargoUsuario(MySqlDataReader resultado)
        {
            CargoUsuario cargoUsuario = new CargoUsuario();
            cargoUsuario.Id_cargo_usuario = resultado.GetInt32(0);
            cargoUsuario.Nombre = resultado.GetString(1);
            return cargoUsuario;

        }
    }
}
