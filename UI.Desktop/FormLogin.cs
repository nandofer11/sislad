using Aplicacion;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Utilidades;

namespace UI.Desktop
{
    public partial class FormLogin : Form
    {
        private UsuarioControlador usuarioControlador;

        public FormLogin()
        {
            InitializeComponent();
            usuarioControlador = new UsuarioControlador();
        }

        private bool ValidarCampos(string usuario, string contraseña)
        {
            // Validar que el usuario no esté vacío
            if (string.IsNullOrEmpty(usuario))
            {
                MostrarError(txtUsuario, "El campo Usuario es requerido.");
                return false;
            }

            // Validar que la contraseña no esté vacía y tenga entre 6 y 20 caracteres
            if (string.IsNullOrEmpty(contraseña) || contraseña.Length < 6 || contraseña.Length > 20)
            {
                MostrarError(txtContraseña, "La contraseña debe tener entre 6 y 20 caracteres.");
                return false;
            }

            return true;
        }


        private void MostrarError(Control control, string mensaje)
        {
            // Mostrar mensaje de error usando ErrorProvider
            errorProvider1.SetError(control, mensaje);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UIUtils.AlternarVisibilidadContraseña(txtContraseña, btnVer);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Limpiar mensajes de error previos
            errorProvider1.Clear();

            // Obtener datos del usuario y la contraseña
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text;

            // Realizar validaciones
            if (ValidarCampos(usuario, contraseña))
            {
                Usuario usuarioActual = usuarioControlador.IniciarSesion(usuario, contraseña);

                // Intentar iniciar sesión si las validaciones pasan
                if (usuarioActual != null)
                {
                    // Aquí puedes realizar acciones adicionales, como abrir el formulario principal, etc.
                    //MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MDIPrincipal mDIPrincipal = new MDIPrincipal(this, usuarioActual);
                    mDIPrincipal.Show();
                    this.Hide();
                    
                }
                else
                {
                    MessageBox.Show("Inicio de sesión fallido. Verifica tu usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
