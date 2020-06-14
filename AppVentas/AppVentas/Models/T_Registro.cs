using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppVentas.Models
{
    public class T_Registro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Nombre { get; set; }
        [MaxLength(255)]

        [Unique]
        public string Usuario { get; set; }
        [MaxLength(255)]
        public string Contrasenia { get; set; }
    }
}
