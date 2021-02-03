using CadMinerva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMinerva
{
    public class ProveedorCln
    {
        public static int insertar(Proveedor proveedor)
        {
            using (var db = new MinervaEntities())
            {
                db.Proveedor.Add(proveedor);
                db.SaveChanges();
                return proveedor.id;
            }
        }

        public static int actualizar(Proveedor proveedor)
        {
            using (var db = new MinervaEntities())
            {
                var actual = db.Proveedor.Find(proveedor.id);
                actual.nit = proveedor.nit;
                actual.razonSocial = proveedor.razonSocial;
                actual.direccion = proveedor.direccion;
                actual.telefono = proveedor.telefono;
                actual.representante = proveedor.representante;
                actual.usuarioRegistro = proveedor.usuarioRegistro;
                return db.SaveChanges();
            }
        }

        public static int eliminar(int idProveedor, string usuario) // Lógico
        {
            using (var db = new MinervaEntities())
            {
                var actual = db.Proveedor.Find(idProveedor);
                actual.usuarioRegistro = usuario;
                actual.registroActivo = false;
                return db.SaveChanges();
            }
        }

        public static Proveedor get(int idProveedor) 
        {
            using (var db = new MinervaEntities())
            {
                return db.Proveedor.Find(idProveedor);
            }
        }

        public static List<Proveedor> listar() 
        {
            using (var db = new MinervaEntities())
            {
                return db.Proveedor.Where(x => x.registroActivo == true).ToList();
            }
        }

        public static List<paProveedorListar_Result> listarPa(string parametro)
        {
            using (var db = new MinervaEntities())
            {
                return db.paProveedorListar(parametro).ToList();
            }
        }
    }
}
