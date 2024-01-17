using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class UsuarioControlador
    {
        private UsuarioDAO usuarioDAO;
        private Conexion conexion;

        public UsuarioControlador()
        {
            this.conexion = new Conexion();
            this.usuarioDAO = new UsuarioDAO(conexion);
        }

        public Usuario IniciarSesion(string usuario, string contraseña)
        {
            try
            {
                conexion.AbrirConexion();
                Usuario usuarioEncontrado = usuarioDAO.IniciarSesion(usuario, contraseña);
                conexion.CerrarConexion();
                return usuarioEncontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> ListarUsuarios(string nombre)
        {
            try
            {
                conexion.AbrirConexion();
                List<Usuario> listaUsuarios = usuarioDAO.ListarUsuarios(nombre);
                conexion.CerrarConexion();
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Guardar(Usuario usuario)
        {
            try
            {
                conexion.AbrirConexion();
                int registros = usuarioDAO.RegistrarUsuario(usuario);
                conexion.CerrarConexion();
                return registros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario BuscarPorId(int id_usuario)
        {
            try
            {
                conexion.AbrirConexion();
                Usuario usuario = usuarioDAO.BuscarUsuarioPorId(id_usuario);
                conexion.CerrarConexion();
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Editar(Usuario usuario)
        {
            try
            {
                conexion.AbrirConexion();
                int registro = usuarioDAO.ModificarUsuario(usuario);
                conexion.CerrarConexion();
                return registro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Eliminar(Usuario usuario)
        {
            try
            {
                conexion.AbrirConexion();
                int registro = usuarioDAO.EliminarUsuario(usuario);
                conexion.CerrarConexion();
                return registro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
