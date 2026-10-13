using Microsoft.AspNetCore.Mvc;
using WebAPI_01_10_2026.Models;

namespace WebAPI_01_10_2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private static readonly List<Producto> _productos =
        [
            new Producto { Id = 1, Nombre = "Laptop", Descripcion = "Laptop HP 15 pulgadas", Precio = 899.99m, Stock = 10 },
            new Producto { Id = 2, Nombre = "Mouse", Descripcion = "Mouse inalámbrico Logitech", Precio = 29.99m, Stock = 50 },
            new Producto { Id = 3, Nombre = "Teclado", Descripcion = "Teclado mecánico RGB", Precio = 79.99m, Stock = 25 }
        ];

        private static int _siguienteId = 4;

        // GET: api/productos
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetTodos()
        {
            return Ok(_productos);
        }

        // GET: api/productos/5
        [HttpGet("{id:int}")]
        public ActionResult<Producto> GetPorId(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);

            if (producto is null)
                return NotFound(new { mensaje = $"No se encontró el producto con id {id}" });

            return Ok(producto);
        }

        // GET: api/productos/buscar?nombre=lap
        [HttpGet("buscar")]
        public ActionResult<IEnumerable<Producto>> Buscar([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest(new { mensaje = "Debe indicar un nombre para buscar" });

            var resultados = _productos
                .Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(resultados);
        }

        // POST: api/productos
        [HttpPost]
        public ActionResult<Producto> Crear([FromBody] Producto producto)
        {
            producto.Id = _siguienteId++;
            producto.FechaCreacion = DateTime.Now;
            _productos.Add(producto);

            return CreatedAtAction(nameof(GetPorId), new { id = producto.Id }, producto);
        }

        // PUT: api/productos/5
        [HttpPut("{id:int}")]
        public ActionResult Actualizar(int id, [FromBody] Producto productoActualizado)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);

            if (producto is null)
                return NotFound(new { mensaje = $"No se encontró el producto con id {id}" });

            producto.Nombre = productoActualizado.Nombre;
            producto.Descripcion = productoActualizado.Descripcion;
            producto.Precio = productoActualizado.Precio;
            producto.Stock = productoActualizado.Stock;
            producto.Activo = productoActualizado.Activo;

            return NoContent();
        }

        // DELETE: api/productos/5
        [HttpDelete("{id:int}")]
        public ActionResult Eliminar(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);

            if (producto is null)
                return NotFound(new { mensaje = $"No se encontró el producto con id {id}" });

            _productos.Remove(producto);

            return NoContent();
        }

        // POST: api/productos/aleatorios/10
        [HttpPost("aleatorios/{cantidad:int}")]
        public ActionResult<IEnumerable<Producto>> GenerarAleatorios(int cantidad)
        {
            if (cantidad < 1 || cantidad > 100)
                return BadRequest(new { mensaje = "La cantidad debe estar entre 1 y 100" });

            var generados = new List<Producto>();

            for (int i = 0; i < cantidad; i++)
            {
                var producto = GenerarProductoAleatorio();
                _productos.Add(producto);
                generados.Add(producto);
            }

            return Ok(generados);
        }

        private static Producto GenerarProductoAleatorio()
        {
            string[] categorias = ["Laptop", "Mouse", "Teclado", "Monitor", "Auriculares", "Webcam", "Impresora", "Tablet", "Disco SSD", "Memoria RAM", "Cable HDMI", "Hub USB", "Silla", "Escritorio", "Micrófono"];
            string[] marcas = ["HP", "Logitech", "Samsung", "Dell", "Lenovo", "Asus", "Razer", "Kingston", "LG", "Corsair", "Genérico"];
            string[] caracteristicas = ["inalámbrico", "RGB", "4K", "mecánico", "ergonómico", "portátil", "gaming", "bluetooth", "USB-C", "con retroiluminación"];

            string categoria = categorias[Random.Shared.Next(categorias.Length)];
            string marca = marcas[Random.Shared.Next(marcas.Length)];
            string caracteristica = caracteristicas[Random.Shared.Next(caracteristicas.Length)];

            return new Producto
            {
                Id = _siguienteId++,
                Nombre = $"{categoria} {marca}",
                Descripcion = $"{categoria} {caracteristica} marca {marca}",
                Precio = Math.Round((decimal)(Random.Shared.NextDouble() * 999 + 1), 2),
                Stock = Random.Shared.Next(0, 201),
                FechaCreacion = DateTime.Now,
                Activo = Random.Shared.Next(10) > 1
            };
        }
    }
}
