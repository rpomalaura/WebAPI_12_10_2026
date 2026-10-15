using Microsoft.AspNetCore.Mvc;
using WebAPI_01_10_2026.Models;

namespace WebAPI_01_10_2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenCompraController : ControllerBase
    {
        private static readonly List<OrdenCompra> _ordenes = new()
        {
            new OrdenCompra
            {
                ID = "ORD-1001",
                ClienteID = "C-200",
                Fecha = DateOnly.FromDateTime(DateTime.UtcNow),
                Items = new List<ProductoOrden>
                {
                    new ProductoOrden { ProductoID = "P-001", Nombre = "Mouse inalámbrico", Cantidad = 1, PrecioUnitario = 29.99m },
                    new ProductoOrden { ProductoID = "P-002", Nombre = "Teclado mecánico", Cantidad = 2, PrecioUnitario = 29.99m }
                }
            }
        };

        public OrdenCompraController()
        {
            // asegurar que el Total esté calculado
            foreach (var o in _ordenes)
            {
                o.Total = o.Items.Sum(p => p.Subtotal);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrdenCompra>> GetAll() => Ok(_ordenes);

        [HttpGet("{id}")]
        public ActionResult<OrdenCompra> GetById(string id)
        {
            var orden = _ordenes.FirstOrDefault(o => o.ID == id);
            if (orden == null) return NotFound();
            return Ok(orden);
        }

        [HttpPost]
        public ActionResult<OrdenCompra> Create(OrdenCompra orden)
        {
            if (string.IsNullOrWhiteSpace(orden.ID)) orden.ID = Guid.NewGuid().ToString();
            orden.Total = orden.Items?.Sum(p => p.Subtotal) ?? 0m;
            _ordenes.Add(orden);
            return CreatedAtAction(nameof(GetById), new { id = orden.ID }, orden);
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, OrdenCompra updated)
        {
            var index = _ordenes.FindIndex(o => o.ID == id);
            if (index == -1) return NotFound();
            updated.ID = id;
            updated.Total = updated.Items?.Sum(p => p.Subtotal) ?? 0m;
            _ordenes[index] = updated;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var orden = _ordenes.FirstOrDefault(o => o.ID == id);
            if (orden == null) return NotFound();
            _ordenes.Remove(orden);
            return NoContent();
        }
    }
}
