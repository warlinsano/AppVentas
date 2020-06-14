using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppVentas.Models
{
   public class Productos
    {
        [PrimaryKey, AutoIncrement]
        public int ProductoId { get; set; }

        public int Nombre { get; set; }

        public decimal Precio { get; set; }

        public int UnidadEnAlmacen { get; set; }
    }
}
