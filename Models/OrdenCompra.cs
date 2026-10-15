namespace WebAPI_01_10_2026.Models
{
    public class OrdenCompra
    {
        public string ID { get; set; } = string.Empty;

        public string ClienteID { get; set; } = string.Empty;

        public DateOnly Fecha { get; set; }

        public decimal Total { get; set; }

        public List<ProductoOrden> Items { get; set; } = new()
        {
            new ProductoOrden { ProductoID = "P-001", Nombre = "Mouse inalámbrico", Cantidad = 1, PrecioUnitario = 29.99m },
            new ProductoOrden { ProductoID = "P-002", Nombre = "Teclado mecánico", Cantidad = 2, PrecioUnitario = 29.99m }
        };
    }

    public class ProductoOrden
    {
        public string ProductoID { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
