using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI_01_10_2026.Models;

namespace WebAPI_01_10_2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private static readonly List<Producto> _productos = new()
        {
            new Producto { Id = 1, Nombre = "Mouse inalámbrico", Descripcion = "Mouse inalámbrico Logitech", Precio = 29.99m, Stock = 50 },
            new Producto { Id = 2, Nombre = "Teclado mecánico", Descripcion = "Teclado mecánico RGB", Precio = 79.99m, Stock = 25 }
        };

        private static int _siguienteId = 3;

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetAll() => Ok(_productos);

        [HttpGet("{id:int}")]
        public ActionResult<Producto> GetById(int id)
        {
            var p = _productos.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public ActionResult<Producto> Create(Producto producto)
        {
            producto.Id = _siguienteId++;
            producto.FechaCreacion = DateTime.Now;
            _productos.Add(producto);
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id, Producto updated)
        {
            var index = _productos.FindIndex(p => p.Id == id);
            if (index == -1) return NotFound();
            updated.Id = id;
            _productos[index] = updated;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var p = _productos.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            _productos.Remove(p);
            return NoContent();
        }
    }
}
