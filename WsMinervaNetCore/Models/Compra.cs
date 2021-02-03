using System;
using System.Collections.Generic;

namespace WsMinervaNetCore.Models
{
    public partial class Compra
    {
        public Compra()
        {
            CompraDetalle = new HashSet<CompraDetalle>();
        }

        public int Id { get; set; }
        public string Transaccion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdProveedor { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool? RegistroActivo { get; set; }

        public Proveedor IdProveedorNavigation { get; set; }
        public ICollection<CompraDetalle> CompraDetalle { get; set; }
    }
}
