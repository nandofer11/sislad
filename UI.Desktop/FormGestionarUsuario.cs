using Aplicacion;
using Dominio;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using static UI.Desktop.MaterialUI;

namespace UI.Desktop
{
    public partial class FormGestionarUsuario : Form
    {

        private static FormGestionarUsuario instancia = null;

        Usuario usuario;
        private const int ACCION_CREAR = 1;
        private const int ACCION_EDITAR = 2;
        private int tipo_accion;

        public FormGestionarUsuario()
        {
            InitializeComponent();
            //loadMaterial(this);

            this.usuario = new Usuario();
            tipo_accion = ACCION_CREAR;
            btnGuardar.Enabled = false;
        }

        public static FormGestionarUsuario GetInstancia()
        {
            if (instancia == null)
            {
                return instancia = new FormGestionarUsuario();
            }
            return instancia;
        }


        private void FormUsuario_Shown(object sender, EventArgs e)
        {
            ObtenerListadoUsuarios("");
            ObtenerCargosUsuario("");
        }
        public void ObtenerListadoUsuarios(string nombre)
        {
            try
            {
                UsuarioControlador usuarioControlador = new UsuarioControlador();
                List<Usuario> listaUsuarios = usuarioControlador.ListarUsuarios(nombre);
                if (listaUsuarios.Count > 0)
                {
                    dgvUsuarios.Rows.Clear();
                    BindingSource datosEnlazados = new BindingSource(); // Instancia para enlazar el resultado de la consulta al DataGridView
                    datosEnlazados.DataSource = listaUsuarios;
                    dgvUsuarios.DataSource = datosEnlazados; // se enlaza el resultado de la consulta al DataGridView
                    ConfigurarColumnasDataGridView();
                    dgvUsuarios.Rows[0].Selected = false;
                    
                }
                else
                {
                    dgvUsuarios.Rows.Clear(); // se eliminan todas las filas existentes del DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un problema al obtener el listado de usuarios. \n\nIntente de nuevo. " + ex.Message, "Sislad: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
        private void ObtenerCargosUsuario(string nombre)
        {
            try
            {
                CargoUsuarioControlador cargoUsuarioControlador = new CargoUsuarioControlador();
                List<CargoUsuario> listaCargosUsuario = cargoUsuarioControlador.ListarCargosUsuarios(nombre);
                cbTipoUsuario.DataSource = listaCargosUsuario;
                cbTipoUsuario.DisplayMember = "nombre_cargo";
                cbTipoUsuario.SelectedItem = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigurarColumnasDataGridView()
        {
            DataGridViewColumn columna0, columna1, columna2, columna3, columna4; // objetos columna
            // modificar los encabezados de columnas de la tabla
            columna0 = dgvUsuarios.Columns[0]; // se recupera la columna id_usuario
            columna0.Visible = false; // se oculta la columna
            columna1 = dgvUsuarios.Columns[1];
            columna1.HeaderText = "Nombre Completo";
            columna2 = dgvUsuarios.Columns[2];
            columna2.HeaderText = "Usuario";
            columna3 = dgvUsuarios.Columns[3];
            columna3.HeaderText = "Contraseña";
            columna4 = dgvUsuarios.Columns[4];
            columna4.HeaderText = "Rol";
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            LimpiarCampos();
            txtNombreCompleto.Focus();
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                int registros;
                CargoUsuario cargoUsuario = (CargoUsuario)cbTipoUsuario.SelectedItem;
                UsuarioControlador usuarioControlador = null;

                if (cargoUsuario != null)
                {
                    //usuario.Id_usuario = Convert.ToInt32(txtIdUsuario.Text);
                   
                    try
                    {
                        int idCargoUsuarioSeleccionado = cargoUsuario.Id_cargo_usuario;

                        if (tipo_accion == ACCION_CREAR)
                        {

                            usuario.Nombre_completo = txtNombreCompleto.Text;
                            usuario.User = txtUsuario.Text.Trim();
                            usuario.Contraseña = txtContraseña.Text.Trim();
                            usuario.Id_cargo_usuario = idCargoUsuarioSeleccionado;

                            usuarioControlador = new UsuarioControlador();
                            registros = usuarioControlador.Guardar(usuario);

                            if (registros == 1)
                            {
                                MessageBox.Show("El usuario fue registrado correctamente.", "Sislad: Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ObtenerListadoUsuarios("");
                                LimpiarCampos();
                                groupBox1.Enabled = false;
                              
                            }
                            else
                            {
                                MessageBox.Show("El usuario no pudo ser registrado.", "Sislad: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            usuario.Id_usuario = Convert.ToInt32(txtIdUsuario.Text.Trim());
                            usuario.Nombre_completo = txtNombreCompleto.Text;
                            usuario.User = txtUsuario.Text.Trim();
                            usuario.Contraseña = txtContraseña.Text.Trim();
                            usuario.Id_cargo_usuario = idCargoUsuarioSeleccionado;

                            usuarioControlador = new UsuarioControlador();
                            registros = usuarioControlador.Editar(usuario);
                            if (registros == 1)
                            {
                                MessageBox.Show("El usuario fue editado correctamente.", "Sislad: Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ObtenerListadoUsuarios("");
                                LimpiarCampos();
                                groupBox1.Enabled = false;
                            }
                            else
                                MessageBox.Show("El usuario seleccionado no existe, verifique.", "Sislad: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Number == 1062)
                        {
                            MessageBox.Show("El usuario ya existe, vuelva a intentarlo.", "SisLad: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUsuario.Text = "";
                            txtUsuario.Focus();
                            errorProvider1.SetError(txtUsuario, "Cambie el nombre de usuario.");

                        }
                    }
                }
            }

        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombreCompleto.Text))
            {
                errorProvider1.SetError(txtNombreCompleto, "Ingresa nombres y apellidos del usuario.");
                return false;
            }
            else if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtUsuario, "Ingresa un usuario.");
                return false;
            }
            else if (string.IsNullOrEmpty(txtContraseña.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtContraseña, "Ingresa una contraseña.");
                return false;
            }
            else if (txtContraseña.Text.Length < 6)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtContraseña, "Ingresa una contraseña mayor o igual a 6 caracteres.");
                return false;
            }
            else if (cbTipoUsuario.SelectedItem == null)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cbTipoUsuario, "Seleccione un cargo del usuario.");
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            //dgvUsuarios.SelectedRows[0].Selected = false;

            errorProvider1.Clear();
            txtIdUsuario.Text = "";
            txtNombreCompleto.Text = "";
            txtUsuario.Text = "";
            txtContraseña.Text = "";
            cbTipoUsuario.SelectedItem = null;            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                int id_usuario;
                DataGridViewRow fila = dgvUsuarios.CurrentRow;
                tipo_accion = ACCION_EDITAR;

                if (fila != null)
                {
                    id_usuario = Convert.ToInt32(fila.Cells[0].Value.ToString()); // Se obtine el valor de la primera celda de la fila seleccionada
                    UsuarioControlador usuarioControlador = new UsuarioControlador();
                    Usuario usuario = usuarioControlador.BuscarPorId(id_usuario);

                    if (usuario != null)
                    {
                        groupBox1.Enabled = true;
                        btnGuardar.Enabled = true;
                        txtIdUsuario.Text = usuario.Id_usuario.ToString();
                        txtNombreCompleto.Text = usuario.Nombre_completo;
                        txtUsuario.Text = usuario.User;
                        txtContraseña.Text = Utilidades.SecurityManager.Decrypt(usuario.Contraseña);

                        int idCargoUsuarioSeleccionado = usuario.Id_cargo_usuario; // Supongamos que tienes este valor

                        // Itera a través de los elementos del ComboBox para encontrar el que coincide con el IdCargoUsuario
                        foreach (CargoUsuario cargo in cbTipoUsuario.Items)
                        {
                            if (cargo.Id_cargo_usuario == idCargoUsuarioSeleccionado)
                            {
                                cbTipoUsuario.SelectedItem = cargo; // Selecciona el elemento que coincide con el IdCargoUsuario
                                break; // Sal del bucle una vez que lo encuentres
                            }
                        }
                    }
                    else
                        MessageBox.Show("El usuario seleccionado ya no existe, verifique.", "SisLad: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ObtenerListadoUsuarios("");

                }
                else
                {
                    //Desactivar el groupBox de datos del usuario
                    groupBox1.Enabled = false;

                    MessageBox.Show("Seleccione un usuario.", "SisLad: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("No hay fila seleccionada para editar.", "SisLad: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                int id_usuario;
                int registros;
                string nombres;
                DataGridViewRow fila = dgvUsuarios.CurrentRow;

                if (fila != null)
                {
                    try
                    {
                        id_usuario = Convert.ToInt32(fila.Cells[0].Value.ToString());
                        nombres = fila.Cells[1].Value.ToString();
                        DialogResult respuesta = MessageBox.Show("Seguro que desea eliminar al usuario " + nombres, "Sislad: Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (respuesta == DialogResult.Yes)
                        {
                            Usuario usuario = new Usuario();
                            usuario.Id_usuario = id_usuario;
                            UsuarioControlador usuarioControlador = new UsuarioControlador();
                            registros = usuarioControlador.Eliminar(usuario);

                            if (registros == 1)
                            {
                                MessageBox.Show("El usuario seleccionado fue eliminado.", "Sislad: Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarCampos();
                                ObtenerListadoUsuarios("");
                            }
                            else
                            {
                                MessageBox.Show("El usuario seleccionado ya no existe, verifique.", "Sislad: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                ObtenerListadoUsuarios("");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay fila seleccionada para eliminar.", "Sislad: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Cambia el tipo de contraseña del TextBox entre "Password" y "Text"
            if (txtContraseña.PasswordChar == '*')
            {
                // Muestra los caracteres de contraseña
                txtContraseña.PasswordChar = '\0';
                btnVerContraseña.Text = "Ocultar"; // Cambia el texto del botón
            }
            else
            {
                // Oculta los caracteres de contraseña (reemplaza por asteriscos)
                txtContraseña.PasswordChar = '*';
                btnVerContraseña.Text = "Mostrar"; // Cambia el texto del botón
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
