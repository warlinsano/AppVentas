using AppVentas.Data;
using AppVentas.Helpers;
using AppVentas.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace AppVentas.ViewModels
{
    public class FacturaByClientContentPageViewModel : ViewModelBase
    {
        private SQLiteConnection _conn;
        private readonly INavigationService _NavigationService;

        public FacturaByClientContentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Facturas";
            _NavigationService = navigationService;
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
            LoadClientListViw();
        }

        public List<Facturas> ListFacturas { get; set; }

        public void LoadClientListViw()
        {
            //var facturas = _conn.Table<Clientes>().Where(c => c.Cedula == Settings.Clients.Cedula).Select(c=>c.Facturas);
            //var ClienteId = _conn.Table<Clientes>().Where(c => c.Cedula == Settings.Clients.Cedula).Select(c => c.ClienteId).FirstOrDefault();
            ListFacturas = _conn.Table<Facturas>().Where(c => c.ClienteId == Settings.Clients.ClienteId).ToList();
        }
    }
}
