using CadMinerva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMinerva
{
    public class UsuarioCln
    {
        public static int insertar(Usuario usuario)
        {
            using (var db = new MinervaEntities())
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return usuario.id;
            }
        }

        public static int actualizar(Usuario usuario)
        {
            using (var db = new MinervaEntities())
            {
                var actual = db.Usuario.Find(usuario.id);
                actual.usuario = usuario.usuario;
                actual.idEmpleado = usuario.idEmpleado;
                return db.SaveChanges();
            }
        }

        public static int eliminar(int idUsuario, string usuario) // Lógico
        {
            using (var db = new MinervaEntities())
            {
                var actual = db.Usuario.Find(idUsuario);
                actual.usuarioRegistro = usuario;
                actual.registroActivo = false;
                return db.SaveChanges();
            }
        }

        public static Usuario get(int idUsuario) 
        {
            using (var db = new MinervaEntities())
            {
                return db.Usuario.Find(idUsuario);
            }
        }

        public static List<Usuario> listar() 
        {
            using (var db = new MinervaEntities())
            {
                return db.Usuario.Where(x => x.registroActivo == true).ToList();
            }
        }

        public static List<paUsuarioListar_Result> listarPa(string parametro) 
        {
            using (var db = new MinervaEntities())
            {
                return db.paUsuarioListar(parametro).ToList();
            }
        }

        public static Usuario validar(string usuario, string clave)
        {
            using (var db = new MinervaEntities())
            {
                return db.Usuario.Where(x => x.usuario == usuario && x.clave == clave).FirstOrDefault();
            }
        }
    }
}
