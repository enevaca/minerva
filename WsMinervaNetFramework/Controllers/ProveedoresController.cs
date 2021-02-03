using CadMinerva;
using ClnMinerva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WsMinervaNetFramework.Controllers
{
    public class ProveedoresController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var proveedores = ProveedorCln.listarPa("");
            return Ok(proveedores);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var proveedores = ProveedorCln.listarPa("").Where(x => x.id == id).FirstOrDefault();
            return Ok(proveedores);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Proveedor modelo)
        {
            modelo.fechaRegistro = DateTime.Now;
            modelo.registroActivo = true;
            var idProveedor = ProveedorCln.insertar(modelo);

            if (idProveedor > 0) return Ok(new { code = "200", message = "OK", id = idProveedor });
            else return Ok(new { code = "400", message = "Error"});
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody] Proveedor modelo)
        {
            modelo.id = id;
            ProveedorCln.actualizar(modelo);
            return Ok(new { code = "200", message = "OK" });
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}