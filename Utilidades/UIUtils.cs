using System.Windows.Forms;
using RecursosCompartidos;

namespace Utilidades
{
    public static class UIUtils
    {
        public static void AlternarVisibilidadContraseña(TextBox textBox, Button button)
        {
            // Cambia el atributo de contraseña del TextBox
            textBox.UseSystemPasswordChar = !textBox.UseSystemPasswordChar;

            // Cambia la imagen del botón basado en la visibilidad actual de la contraseña
            //button.Image = ; 
        }
    }
}
