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
    public partial class FrmProveedor : Form
    {
        bool esNuevo;
        public FrmProveedor()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var lista = ProveedorCln.listarPa(txtParametro.Text);
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
            if (lista.Count > 0) dgvLista.Columns["nit"].Selected = true;
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
            txtNit.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gbxDatos.Enabled = false;
            gbxLista.Enabled = true;
            limpiar();
            txtParametro.Focus();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (validar())
            {
                Proveedor proveedor = new Proveedor();
                proveedor.nit = Convert.ToInt64(txtNit.Text);
                proveedor.razonSocial = txtRazonSocial.Text.Trim();
                proveedor.direccion = txtDireccion.Text.Trim();
                proveedor.telefono = txtTelefono.Text.Trim();
                proveedor.representante = txtRepresentante.Text.Trim();
                proveedor.usuarioRegistro = Util.usuario.usuario;

                if (esNuevo)
                {
                    proveedor.fechaRegistro = DateTime.Now;
                    proveedor.registroActivo = true;
                    ProveedorCln.insertar(proveedor);
                }
                else
                {
                    var row = dgvLista.Rows[dgvLista.CurrentRow.Index];
                    proveedor.id = Convert.ToInt32(row.Cells["id"].Value);
                    ProveedorCln.actualizar(proveedor);
                }
                MessageBox.Show($"Proveedor guardado correctamente.", "::: Minerva - Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listar();
                btnCancelar.PerformClick();
            }
        }

        private bool validar()
        {
            bool esValido = true;
            erpNit.SetError(txtNit, string.Empty);
            erpRazonSocial.SetError(txtRazonSocial, string.Empty);
            erpRepresentante.SetError(txtRepresentante, string.Empty);

            if (string.IsNullOrEmpty(txtNit.Text)) { erpNit.SetError(txtNit, "El NIT es obligatorio"); esValido = false; }
            if (string.IsNullOrEmpty(txtRazonSocial.Text)) { erpRazonSocial.SetError(txtRazonSocial, "La razón social es obligatoria"); esValido = false; }
            if (string.IsNullOrEmpty(txtRepresentante.Text)) { erpRepresentante.SetError(txtRepresentante, "La dirección es obligatoria"); esValido = false; }

            return esValido;
        }

        private void limpiar()
        {
            txtNit.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtRepresentante.Text = string.Empty;
        }

        private void cargarDatos() 
        {
            var row = dgvLista.Rows[dgvLista.CurrentRow.Index];
            txtNit.Text = row.Cells["nit"].Value.ToString();
            txtRazonSocial.Text = row.Cells["razonSocial"].Value.ToString();
            txtDireccion.Text = row.Cells["direccion"].Value.ToString(); ;
            txtTelefono.Text = row.Cells["telefono"].Value.ToString(); ;
            txtRepresentante.Text = row.Cells["representante"].Value.ToString(); ;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            gbxDatos.Enabled = true;
            gbxLista.Enabled = false;
            cargarDatos();
            txtNit.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var row = dgvLista.Rows[dgvLista.CurrentRow.Index];
            var nit = row.Cells["nit"].Value.ToString();
            var msg = MessageBox.Show($"¿Está seguro que sea eliminar el proveedor con nit {nit}?", "::: Minverva - Consulta :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes == msg)
            {
                ProveedorCln.eliminar(Convert.ToInt32(row.Cells["id"].Value), Util.usuario.usuario);
                MessageBox.Show($"Proveedor dado de baja.", "::: Minerva - Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listar();
            }
        }

        private void FrmProveedor_Load(object sender, EventArgs e)
        {
            //cbxEmpleado.DataSource = EmpleadoCln.listar("");
            //cbxEmpleado.ValueMember = "id";
            //cbxEmpleado.DisplayMember = "nombres";
        }
    }
}
