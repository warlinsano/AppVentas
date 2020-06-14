using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppVentas.Models
{
    [Table("Facturas")]
    public class Facturas
    {
        [PrimaryKey, AutoIncrement]
        public int FacturaId { get; set; }

        public decimal Total { get; set; }

        public DateTime Fecha { get; set; }

        public bool Saldada { get; set; }

        public bool IsCredito { get; set; }

        [ForeignKey(typeof(Clientes))]
        public int ClienteId { get; set; }
    }
}
