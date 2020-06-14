using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppVentas.Models
{
    public class PagoClientes
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int FacturaId { get; set; }

        public decimal PagoAbono { get; set; }

        public DateTime fechaPago { get; set; }

        public string Descripcion { get; set; }

    }
}
