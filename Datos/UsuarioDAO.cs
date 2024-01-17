using Dominio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utilidades;

namespace Datos
{
    public class UsuarioDAO
    {
        private Conexion conexion;

        public UsuarioDAO(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public Usuario IniciarSesion(string usuario, string contraseña)
        {
            string sentenciaSQL = "SELECT id_usuario, nombre_completo, usuario, contraseña, id_cargo_usuario FROM usuario WHERE usuario = @usuario AND contraseña = @contraseña";

            try
            {
                MySqlCommand comando = conexion.CrearComandoSQL(sentenciaSQL);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@contraseña", contraseña);

                // Desencriptar la contraseña almacenada en la base de datos antes de compararla
                //comando.Parameters.AddWithValue("@contraseña", SecurityManager.Encrypt(usuario.Contraseña));

                MySqlDataReader resultado = comando.ExecuteReader();

                if (resultado.Read())
                {
                    Usuario usuarioEncontrado = CrearObjetoUsuario(resultado);
                    resultado.Close();
                    return usuarioEncontrado;
                }

                resultado.Close();
                return null; // Usuario no encontrado
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> ListarUsuarios(string nombre)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            Usuario usuario;
            String sentenciaSQL = "SELECT id_usuario, nombre_completo, usuario, contraseña, id_cargo_usuario from usuario where nombre_completo like '%" + nombre + "%' order by nombre_completo";
            try
            {
                MySqlDataReader resultado = conexion.EjecutarConsulta(sentenciaSQL);
                while (resultado.Read())
                {
                    usuario = CrearObjetoUsuario(resultado);
                    listaUsuarios.Add(usuario);
                }
                resultado.Close();
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RegistrarUsuario(Usuario usuario)
        {
            int registros_afectados;
            String sentenciaSQL = "INSERT INTO usuario(nombre_completo, usuario, contraseña, id_cargo_usuario) values(@nombre_completo, @usuario, @contraseña, @id_cargo_usuario)";
            try
            {
                MySqlCommand comando = conexion.CrearComandoSQL(sentenciaSQL);
                AsignarParametros(usuario, comando);
                // Encriptar la contraseña antes de guardarla
                comando.Parameters["@contraseña"].Value = SecurityManager.Encrypt(usuario.Contraseña);

                registros_afectados = comando.ExecuteNonQuery();
           
                return registros_afectados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ModificarUsuario(Usuario usuario)
        {
            int registros;
            String sentenciaSQL = "update usuario set nombre_completo = @nombre_completo, usuario = @usuario, contraseña = @contraseña, id_cargo_usuario = @id_cargo_usuario  where id_usuario = @id_usuario";
            try
            {
                MySqlCommand comando = conexion.CrearComandoSQL(sentenciaSQL);
                AsignarParametros(usuario, comando);
                // Encriptar la contraseña antes de guardarla
                comando.Parameters["@contraseña"].Value = SecurityManager.Encrypt(usuario.Contraseña);

                comando.Parameters.AddWithValue("@id_usuario", usuario.Id_usuario);
                registros = comando.ExecuteNonQuery();
                return registros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EliminarUsuario(Usuario usuario)
        {
            int registros;
            String sentenciaSQL = "delete from usuario where id_usuario = @id_usuario";
            try
            {
                MySqlCommand comando = conexion.CrearComandoSQL(sentenciaSQL);
                comando.Parameters.AddWithValue("@id_usuario", usuario.Id_usuario);
                registros = comando.ExecuteNonQuery();
                return registros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AsignarParametros(Usuario usuario, MySqlCommand comando)
        {
            comando.Parameters.AddWithValue("@nombre_completo", usuario.Nombre_completo);
            comando.Parameters.AddWithValue("@usuario", usuario.User);
            comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
            comando.Parameters.AddWithValue("@id_cargo_usuario", usuario.Id_cargo_usuario);
        }

        private Usuario CrearObjetoUsuario(MySqlDataReader resultado)
        {
            Usuario usuario;
            usuario = new Usuario();
            usuario.Id_usuario = resultado.GetInt32(0);
            usuario.Nombre_completo = resultado.GetString(1);
            usuario.User = resultado.GetString(2);
            usuario.Contraseña = resultado.GetString(3);
            usuario.Id_cargo_usuario = resultado.GetInt32(4);
            return usuario;
        }

        public Usuario BuscarUsuarioPorId(int id_usuario)
        {
            Usuario usuario = null;
            String sentenciaSQL = "select id_usuario, nombre_completo, usuario, contraseña, id_cargo_usuario from Usuario where id_usuario = " + id_usuario;
            try
            {
                MySqlDataReader resultado = conexion.EjecutarConsulta(sentenciaSQL);
                if (resultado.Read())
                {
                    usuario = CrearObjetoUsuario(resultado);
                }
                resultado.Close();
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
