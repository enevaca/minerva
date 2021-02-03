using CadMinerva;
using ClnMinerva;
using RestSharp;
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
    public partial class FrmPrincipal : Form
    {
        FrmAutenticacion frmAutenticacion;
        public FrmPrincipal(FrmAutenticacion frmAutenticacion)
        {
            this.frmAutenticacion = frmAutenticacion;
            InitializeComponent();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            new FrmProveedor().ShowDialog();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            new FrmEmpleado().ShowDialog();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            //Usuario usuario = new Usuario();
            //usuario.usuario = "eddy";
            //usuario.clave = Util.Encrypt("hola1234");
            //usuario.idEmpleado = 1;
            //usuario.usuarioRegistro = Util.usuario.usuario;
            //usuario.fechaRegistro = DateTime.Now;
            //usuario.registroActivo = true;
            //UsuarioCln.insertar(usuario);

            var client = new RestClient("https://localhost:44359/api/proveedores");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmAutenticacion.Visible = true;
        }
    }
}
