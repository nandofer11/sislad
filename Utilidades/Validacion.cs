using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilidades
{
    public static class Validacion
    {
        public static bool SoloNumeros(KeyPressEventArgs e)
        {
            bool rspta;
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                rspta = true;
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
                rspta = false;
            }
            return rspta;
        }

        public static bool SoloLetras(KeyPressEventArgs e)
        {
            bool rspta;
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                rspta = true;
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
                rspta = false;
            }
            return rspta;
        }

        public static void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Permitir números del 0 al 9, teclas de control y el punto decimal
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == '.' && textBox.Text.IndexOf('.') == -1))
            {
                // Si es el punto decimal, validar que no haya más de dos dígitos después del punto
                if (e.KeyChar == '.')
                {
                    int decimalIndex = textBox.Text.IndexOf('.');
                    if (decimalIndex != -1 && textBox.Text.Substring(decimalIndex).Length > 2)
                    {
                        e.Handled = true;
                    }
                }
                else if (char.IsDigit(e.KeyChar))
                {
                    // Validar que no haya más de cuatro dígitos antes del punto
                    int decimalIndex = textBox.Text.IndexOf('.');
                    int lengthBeforeDecimal = (decimalIndex == -1) ? textBox.Text.Length : decimalIndex;
                    if (lengthBeforeDecimal >= 4)
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        public static void TextoTitulo(TextBox textBox) {
            textBox.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(textBox.Text);
            textBox.SelectionStart = textBox.Text.Length;
        }
    }
}
