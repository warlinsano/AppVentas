using AppVentas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppVentas.Helpers
{
   public static class Settings
    {
        public static ObservableCollection<Clientes> ListClients { get; set; }

        public static Clientes Clients { get; set; }
    }
}
