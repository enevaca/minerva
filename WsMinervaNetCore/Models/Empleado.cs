using System;
using System.Collections.Generic;

namespace WsMinervaNetCore.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string CedulaIdentidad { get; set; }
        public string Nombres { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Direccion { get; set; }
        public long? Celuar { get; set; }
        public string Cargo { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool? RegistroActivo { get; set; }

        public ICollection<Usuario> Usuario { get; set; }
    }
}
