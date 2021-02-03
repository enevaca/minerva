using CadMinerva;
using ClnMinerva;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpMinerva
{
    public partial class FrmEmpleado : Form
    {
        bool esNuevo;
        public FrmEmpleado()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var lista = EmpleadoCln.listar(txtParametro.Text);
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
            if (lista.Count > 0) dgvLista.Columns["cedulaIdentidad"].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            gbxDatos.Enabled = true;
            gbxLista.Enabled = false;
            txtCI.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gbxDatos.Enabled = false;
            gbxLista.Enabled = true;
            limpiar();
            txtParametro.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                Empleado empleado = new Empleado();
                empleado.cedulaIdentidad = txtCI.Text;
                empleado.nombres = txtNombre.Text.Trim();
                empleado.paterno = txtPaterno.Text.Trim();
                empleado.materno = txtMaterno.Text.Trim();
                empleado.direccion = txtDireccion.Text.Trim();
                empleado.celuar = Convert.ToInt64(txtCelular.Text);
                empleado.cargo = txtCargo.Text.Trim();
                empleado.usuarioRegistro = Util.usuario.usuario;

                if (esNuevo)
                {
                    empleado.fechaRegistro = DateTime.Now;
                    empleado.registroActivo = true;
                    EmpleadoCln.insertar(empleado);
                }
                else
                {
                    var row = dgvLista.Rows[dgvLista.CurrentRow.Index];
                    empleado.id = Convert.ToInt32(row.Cells["id"].Value);
                    EmpleadoCln.actualizar(empleado);
                }
                MessageBox.Show($"Empleado guardado correctamente.", "::: Minerva - Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listar();
                btnCancelar.PerformClick();
            }
        }

        private bool validar()
        {
            bool esValido = true;
            erpNombre.SetError(txtNombre, string.Empty);
            erpApellido.SetError(txtPaterno, string.Empty);
            erpCI.SetError(txtCI, string.Empty);

            if (string.IsNullOrEmpty(txtNombre.Text)) { erpNombre.SetError(txtNombre, "El Nombre es obligatorio"); esValido = false; }
            if (string.IsNullOrEmpty(txtPaterno.Text) && string.IsNullOrEmpty(txtMaterno.Text)) { erpApellido.SetError(txtPaterno, "Al menos un apellido es obligatorio"); esValido = false; }
            if (string.IsNullOrEmpty(txtCI.Text)) { erpCI.SetError(txtCI, "La cédula de identidad es obligatoria"); esValido = false; }

            return esValido;
        }

        private void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtPaterno.Text = string.Empty;
            txtMaterno.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtCargo.Text = string.Empty;
        }

        private void cargarDatos()
        {
            var row = dgvLista.Rows[dgvLista.CurrentRow.Index];
            txtCI.Text = row.Cells["cedulaIdentidad"].Value.ToString();
            txtNombre.Text = row.Cells["nombres"].Value.ToString();
            txtPaterno.Text = row.Cells["paterno"].Value.ToString();
            txtMaterno.Text = row.Cells["materno"].Value.ToString();
            txtDireccion.Text = row.Cells["direccion"].Value.ToString();
            txtCelular.Text = row.Cells["celuar"].Value.ToString();
            txtCargo.Text = row.Cells["cargo"].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            gbxDatos.Enabled = true;
            gbxLista.Enabled = false;
            cargarDatos();
            txtCI.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var row = dgvLista.Rows[dgvLista.CurrentRow.Index];
            var ci = row.Cells["cedulaIdentidad"].Value.ToString();
            var msg = MessageBox.Show($"¿Está seguro que sea eliminar el empleado con CI {ci}?", "::: Minverva - Consulta :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes == msg)
            {
                EmpleadoCln.eliminar(Convert.ToInt32(row.Cells["id"].Value), Util.usuario.usuario);
                MessageBox.Show($"Empleado dado de baja.", "::: Minerva - Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listar();
            }
        }
    }
}
