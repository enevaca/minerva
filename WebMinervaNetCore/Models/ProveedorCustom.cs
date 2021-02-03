using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMinervaNetCore.Models
{
    public class ProveedorCustom
    {
        public int id { get; set; }
        public long nit { get; set; }
        public string razonSocial { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string representante { get; set; }
        public string usuarioRegistro { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public bool? registroActivo { get; set; }
    }
}
