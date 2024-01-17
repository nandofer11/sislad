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

namespace UI.Desktop
{
    public partial class FormGestionarEmpleados : Form
    {

        public FormGestionarEmpleados()
        {
            InitializeComponent();
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            FormDatosEmpleado formDatosEmpleado = new FormDatosEmpleado();
            formDatosEmpleado.ShowDialog();
            ListarEmpleados("");
        }

        private void FormGestionarEmpleados_Load(object sender, EventArgs e)
        {
            //VerificarCantidadEmpleados();
        }

        private void FormGestionarEmpleados_Shown(object sender, EventArgs e)
        {
            ListarEmpleados("");
            VerificarCantidadEmpleados();
        }

        public void VerificarCantidadEmpleados()        
        {
            if (dgvEmpleados.Rows.Count != 0)
            {
                lblTotalEmpleados.Text = dgvEmpleados.Rows.Count.ToString();
            }
            else
                lblTotalEmpleados.Text = "0";
        }

        private void ListarEmpleados(string nombre)
        {
            try
            {
                EmpleadoControlador empleadoControlador = new EmpleadoControlador();
                List<Empleado> listaEmpleados = empleadoControlador.ListarEmpleados(nombre);
                if (listaEmpleados.Count > 0)
                {
                    dgvEmpleados.Columns.Clear();
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = listaEmpleados;
                    dgvEmpleados.DataSource = bindingSource;
                    ConfigurarColumnasDataGridView();
                    dgvEmpleados.Rows[0].Selected = false;
                }else
                    dgvEmpleados.Rows.Clear(); // se eliminan todas las filas existentes del DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ocurrio un problema al obtener el listado de empleados. \n\nIntente de nuevo. " + ex.Message, "Sistema: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasDataGridView()
        {
            dgvEmpleados.RowHeadersVisible = false; // desactivar la primera columna con la fecha

            DataGridViewColumn columna0, columna1, columna2, columna3, columna4, columna5, columna6, columna7, columna8, columna9, columna10;

            columna0 = dgvEmpleados.Columns[0]; //Recuperar la columan id_empleado
            columna0.Visible = false; // Ocultamos columna id_empleado

            columna1 = dgvEmpleados.Columns[1];
            columna1.HeaderText = "N°. DNI";

            columna2 = dgvEmpleados.Columns[2];
            columna2.HeaderText = "Nombres";

            columna3 = dgvEmpleados.Columns[3];
            columna3.HeaderText = "Apellidos";

            columna4 = dgvEmpleados.Columns[4];
            columna4.HeaderText = "Ciudad";

            columna5 = dgvEmpleados.Columns[5];
            columna5.HeaderText = "Dirección";

            columna6 = dgvEmpleados.Columns[6];
            columna6.HeaderText = "Teléfono";

            columna7 = dgvEmpleados.Columns[7];
            columna7.HeaderText = "Pago día";

            columna8 = dgvEmpleados.Columns[8];
            columna8.HeaderText = "Inicio Contrato";

            columna9 = dgvEmpleados.Columns[9];
            columna9.HeaderText = "Fin Contrato";

            columna10 = dgvEmpleados.Columns[10];
            columna10.HeaderText = "Estado";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id_empleado;
            DataGridViewRow fila = dgvEmpleados.CurrentRow;

            if (fila != null)
            {
                id_empleado = Convert.ToInt32(fila.Cells[0].Value);
                EmpleadoControlador empleadoControlador = new EmpleadoControlador();
                Empleado empleado = empleadoControlador.BuscarEmpleadoPorId(id_empleado);
                if (empleado != null)
                {
                    FormDatosEmpleado formDatosEmpleado = new FormDatosEmpleado(empleado);
                    formDatosEmpleado.ShowDialog();
                }else
                    MessageBox.Show("El empleado seleccionado ya no existe, verifique.", "Sistema: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ListarEmpleados("");
            }
            else
            {
                MessageBox.Show("Seleccione un empleado para modificar.", "Sistema: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvEmpleados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnModificar.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id_empleado;
            int registros;
            string nombres;

            DataGridViewRow fila = dgvEmpleados.CurrentRow;

            if (fila == null)
            {
                MessageBox.Show("Seleccione un empleado para eliminar.", "Sistema: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                id_empleado = Convert.ToInt32(fila.Cells[0].Value.ToString());
                nombres = fila.Cells[2].Value.ToString();

                DialogResult respuesta = MessageBox.Show("Seguro que desea eliminar al empleado " + nombres, "Sistema: Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    Empleado empleado = new Empleado();
                    empleado.Id_empleado = id_empleado;
                    EmpleadoControlador empleadoControlador = new EmpleadoControlador();
                    registros = empleadoControlador.EliminarEmpleado(empleado);
                    if (registros == 1)
                    {
                        MessageBox.Show("El empleado seleccionado fue eliminado.", "Sistema: Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("El empleado seleccionado ya no existe, verifique.", "Sistema: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    VerificarCantidadEmpleados();
                    ListarEmpleados("");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dgvEmpleados_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            VerificarCantidadEmpleados();
        }

        private void dgvEmpleados_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            VerificarCantidadEmpleados();
        }
    }
}
