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

namespace UI.Desktop
{
    public partial class MDIPrincipal : Form
    {
        private int childFormNumber = 0;
        private FormLogin FormLogin;
        private Usuario Usuario;

        public MDIPrincipal()
        {
            InitializeComponent();
        }

        public MDIPrincipal(FormLogin formLogin, Usuario usuario)
        {
            InitializeComponent();
            FormLogin = formLogin;
            Usuario = usuario;

            toolStripStatusLabelNombres.Text = usuario.Nombre_completo.ToUpper();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            FormGestionarUsuario formUsuario = FormGestionarUsuario.GetInstancia();
            formUsuario.MdiParent = this;
            formUsuario.Show();
            formUsuario.BringToFront();

            //Form childForm = new Form();
            //childForm.MdiParent = this;
            //childForm.Text = "Ventana " + childFormNumber++;
            //childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidarFormAbierto("FormGestionarEmpleado") == false)
            {
                FormGestionarEmpleados formGestionarEmpleados = new FormGestionarEmpleados();
                formGestionarEmpleados.MdiParent = this;
                formGestionarEmpleados.Show();
            }
        }

        private void MDIPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormLogin.Close();
        }

        private void windowsMenu_Click(object sender, EventArgs e)
        {

        }

        private bool ValidarFormAbierto(string nombre_form)
        {
            foreach (var form_hijo in this.MdiChildren)
            {
                if (form_hijo.Text == nombre_form)
                {
                    form_hijo.BringToFront();
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
