using Microsoft.AspNetCore.Mvc;
using WebAPI_01_10_2026.Models;

namespace WebAPI_01_10_2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private static readonly List<Cliente> _clientes = new()
        {
            new Cliente { ClienteID = "C-200", Nombre = "Empresa Ejemplo", Email = "contacto@ejemplo.com", Telefono = "+34 600 000 000", Direccion = "Calle Falsa 123" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetAll() => Ok(_clientes);

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(string id)
        {
            var c = _clientes.FirstOrDefault(x => x.ClienteID == id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        [HttpPost]
        public ActionResult<Cliente> Create(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.ClienteID)) cliente.ClienteID = Guid.NewGuid().ToString();
            _clientes.Add(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.ClienteID }, cliente);
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, Cliente updated)
        {
            var index = _clientes.FindIndex(c => c.ClienteID == id);
            if (index == -1) return NotFound();
            updated.ClienteID = id;
            _clientes[index] = updated;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var c = _clientes.FirstOrDefault(x => x.ClienteID == id);
            if (c == null) return NotFound();
            _clientes.Remove(c);
            return NoContent();
        }
    }
}
