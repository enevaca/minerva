using System;
using System.Collections.Generic;

namespace WsMinervaNetCore.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Usuario1 { get; set; }
        public string Clave { get; set; }
        public int IdEmpleado { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool? RegistroActivo { get; set; }

        public Empleado IdEmpleadoNavigation { get; set; }
    }
}
