using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CargoUsuario
    {
        private int id_cargo_usuario;
        private string nombre_cargo;

        public int Id_cargo_usuario { get => id_cargo_usuario; set => id_cargo_usuario = value; }
        public string Nombre { get => nombre_cargo; set => nombre_cargo = value; }

        public override string ToString()
        {
            return nombre_cargo;
        }
    }
}
