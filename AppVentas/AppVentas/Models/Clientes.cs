using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppVentas.Models
{
    [Table("Clientes")]
    public class Clientes
    {
        [PrimaryKey, AutoIncrement]
        public int ClienteId { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [Unique]
        public string Cedula { get; set; }

        [MaxLength(255)]
        public string Adress { get; set; }

        [MaxLength(20)]
        public string Telephone { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert)]
        public List<Facturas> Facturas { get; set; }
        //public int? ClientTypeId { get; set; }
    }
}
