using Aplicacion;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilidades;

namespace UI.Desktop
{
    public partial class FormDatosEmpleado : Form
    {

        Empleado empleado;
        private const int accion_registrar = 1;
        private const int accion_modificar = 2;
        private int tipoAccion;

        //Constructor para registrar un nuevo empleado
        public FormDatosEmpleado()
        {
            InitializeComponent();
            tipoAccion = accion_registrar;
            this.empleado = new Empleado();

            //Establece la fecha fin de contrato sumado 3 meses la fecha de inicio
            DateTime fechaInicio = this.dtpFechaContrato.Value;
            DateTime fechaFinNueva = fechaInicio.AddMonths(3);
            dtpFechaFinContrato.Value = fechaFinNueva;
        }

        //Constructor para modificar un empleado
        public FormDatosEmpleado(Empleado empleado)
        {
            InitializeComponent();
            tipoAccion = accion_modificar;
            this.empleado = empleado;
            txtIdEmpleado.Text = Convert.ToString(empleado.Id_empleado);
            txtDni.Text = empleado.Dni;
            txtNombres.Text = empleado.Nombres;
            txtApellidos.Text = empleado.Apellidos;
            txtCiudad.Text = empleado.Ciudad;
            txtDireccion.Text = empleado.Direccion;
            txtTelefono.Text = empleado.Telefono;
            txtPagoDia.Text = Convert.ToString(empleado.Pago_dia);
            dtpFechaContrato.Text = Convert.ToString(empleado.Fecha_contrato);
            dtpFechaFinContrato.Text = Convert.ToString(empleado.Fecha_fin_contrato);
            cbEstado.Text = empleado.Estado;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDatosEmpleado_Load(object sender, EventArgs e)
        {
            

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            int registros;

            try
            {
                EmpleadoControlador empleadoControlador = new EmpleadoControlador();

                if (!ValidarCampos())
                {
                    return;
                }
                else
                {
                    empleado.Dni = txtDni.Text.Trim();
                    empleado.Nombres = txtNombres.Text.Trim();
                    empleado.Apellidos = txtApellidos.Text.Trim();
                    empleado.Ciudad = txtCiudad.Text.Trim();
                    empleado.Direccion = txtDireccion.Text.Trim();
                    empleado.Telefono = txtTelefono.Text.Trim();
                    empleado.Pago_dia = Convert.ToDouble(txtPagoDia.Text.Trim());
                    empleado.Fecha_contrato = dtpFechaContrato.Value.Date;
                    empleado.Fecha_fin_contrato = dtpFechaFinContrato.Value.Date;
                    empleado.Estado = cbEstado.Text.Trim();

                    if (tipoAccion == accion_registrar)
                    {
                        registros = empleadoControlador.RegistrarEmpleado(empleado);
                        if (registros == 1)
                        {
                            MessageBox.Show("El empleado fue registrado correctamente", "Sistema: Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("El empleado no pudo ser registrado.", "Sistema: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        registros = empleadoControlador.ModificarEmpleado(empleado);
                        if (registros == 1) { 
                            MessageBox.Show("El empleado fue modificado.", "Sistem: Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); 
                        }
                        else
                            MessageBox.Show("El empleado seleccionado no existe, verifique.", "Sistema: Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ocurrio un problema al registrar el empleado. \n\nIntente de nuevo. " + ex.Message, "Sistema: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            bool rspta;
            if (string.IsNullOrEmpty(txtDni.Text))
            {
                errorProvider1.SetError(txtDni, "Nro. documento requerido.");
                rspta = false;
            } else if (txtDni.TextLength < 8)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtDni, "Nro. documento igual a 8 dígitos.");
                rspta = false;
            } else if (string.IsNullOrEmpty(txtNombres.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtNombres, "Nombre del empleado requerido.");
                rspta = false;
            } else if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtApellidos, "Apellidos del empleado requerido.");
                rspta = false;
            } else if (string.IsNullOrEmpty(txtCiudad.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtCiudad, "Ciudad del empleado requerido.");
                rspta = false;
            } else if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtDireccion, "Dirección del empleado requerido.");
                rspta = false;
            } else if (string.IsNullOrEmpty(txtPagoDia.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPagoDia, "Pago por día del empleado requerido.");
                rspta = false;
            } else if (dtpFechaContrato.Value == dtpFechaContrato.MinDate)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(dtpFechaContrato, "Fecha de contrato del empleado requerido.");
                rspta = false;
            }else if (cbEstado.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cbEstado, "Estado del empleado requerido.");
                rspta = false;
            }
            else
            {
                errorProvider1.Clear();
                rspta = true;
            }
            return rspta;
        }

      

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
         
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloNumeros(e);
        }


        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetras(e);
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetras(e);
        }

        private void txtCiudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloLetras(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloNumeros(e);
        }

        private void txtPagoDia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validacion.SoloDecimales(sender, e);
        }

        private void txtNombres_TextChanged(object sender, EventArgs e)
        {
            Validacion.TextoTitulo(txtNombres);
        }

        private void txtApellidos_TextChanged(object sender, EventArgs e)
        {
            Validacion.TextoTitulo(txtApellidos);
        }

        private void txtCiudad_TextChanged(object sender, EventArgs e)
        {
            Validacion.TextoTitulo(txtCiudad);
        }
    }
}
