﻿using ClnMinerva;
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
    public partial class FrmAutenticacion : Form
    {
        public FrmAutenticacion()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var usuario = UsuarioCln.validar(txtUsuario.Text.Trim(), Util.Encrypt(txtClave.Text));
                if (usuario != null)
                {
                    Util.usuario = usuario;
                    txtClave.Text = string.Empty;
                    txtClave.Focus();
                    this.Visible = false;
                    new FrmPrincipal(this).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña no válidos", "::: Minerva - Mensaje :::", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool validar()
        {
            bool esValido = true;
            erpUsuario.SetError(txtUsuario, string.Empty);
            erpClave.SetError(txtClave, string.Empty);

            if (string.IsNullOrEmpty(txtUsuario.Text)) { erpUsuario.SetError(txtUsuario, "El Usuario es obligatorio"); esValido = false; }
            if (string.IsNullOrEmpty(txtClave.Text)) { erpClave.SetError(txtClave, "La Contraseña social es obligatoria"); esValido = false; }
            return esValido;
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }
    }
}
