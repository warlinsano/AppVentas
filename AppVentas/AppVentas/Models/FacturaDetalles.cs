using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppVentas.Models
{
    public class FacturaDetalles
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int FacturaId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Monto { get; set; }

        public decimal Descuento { get; set; }
    }
}
