using CadMinerva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMinerva
{
    public class EmpleadoCln
    {
        public static int insertar(Empleado empleado)
        {
            using (var db = new MinervaEntities())
            {
                db.Empleado.Add(empleado);
                db.SaveChanges();
                return empleado.id;
            }
        }

        public static int actualizar(Empleado empleado)
        {
            using (var db = new MinervaEntities())
            {
                var actual = db.Empleado.Find(empleado.id);
                actual.cedulaIdentidad = empleado.cedulaIdentidad;
                actual.nombres = empleado.nombres;
                actual.paterno = empleado.paterno;
                actual.materno = empleado.materno;
                actual.direccion = empleado.direccion;
                actual.celuar = empleado.celuar;
                actual.cargo = empleado.cargo;
                return db.SaveChanges();
            }
        }

        public static int eliminar(int idEmpleado, string usuario) // Lógico
        {
            using (var db = new MinervaEntities())
            {
                var actual = db.Empleado.Find(idEmpleado);
                actual.usuarioRegistro = usuario;
                actual.registroActivo = false;
                return db.SaveChanges();
            }
        }

        public static Empleado get(int idEmpleado) 
        {
            using (var db = new MinervaEntities())
            {
                return db.Empleado.Find(idEmpleado);
            }
        }

        public static List<Empleado> listar(string parametro) 
        {
            using (var db = new MinervaEntities())
            {
                return db.Empleado.Where(x => x.registroActivo == true).ToList();
            }
        }
    }
}
