using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        private int id_usuario;
        private string nombre_completo;
        private string user;
        private string contraseña;
        private int id_cargo_usuario;

        public Usuario()
        {

        }

        public Usuario(string nombreCompleto, string user, string contraseña, int idCargoUsuario)
        {
            // No incluimos Id_usuario en el constructor ya que será generado automáticamente por la base de datos.
            Nombre_completo = nombreCompleto;
            User = user;
            Contraseña = contraseña;
            Id_cargo_usuario = idCargoUsuario;
        }

        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
        public string Nombre_completo { get => nombre_completo; set => nombre_completo = value; }
        public string User { get => user; set => user = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public int Id_cargo_usuario { get => id_cargo_usuario; set => id_cargo_usuario = value; }
    }
}
