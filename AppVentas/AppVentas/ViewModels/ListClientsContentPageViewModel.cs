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
    public class ListClientsContentPageViewModel : ViewModelBase
    {

        private DelegateCommand _agregarClientButton;
        private SQLiteConnection _conn;
        private readonly INavigationService _NavigationService;

        public ListClientsContentPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Lista de Clientes";
            _NavigationService = navigationService;
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
            LoadClientListViw();
        }

        public DelegateCommand agregarClientButton => _agregarClientButton ?? (_agregarClientButton = new DelegateCommand(IrAgregar));

        public ObservableCollection<ClientsItemViewModel> ListClients { get; set; }

        public void LoadClientListViw()
        {
            //var ResulRegistros = _conn.Table<Clientes>().ToList();
            //ListClients = new ObservableCollection<Clientes>(ResulRegistros);
            //ListClients = Settings.ListClients;
            var Clientes = _conn.Table<Clientes>().ToList();
            ListClients = new ObservableCollection<ClientsItemViewModel>(Clientes.Select(c => new ClientsItemViewModel(_NavigationService)
            {
                ClienteId = c.ClienteId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Cedula = c.Cedula,
                Telephone = c.Telephone,
                Adress = c.Adress
            }).ToList()); 
         }

        private async void IrAgregar()
        {
            await NavigationService.NavigateAsync("RegisterClientsContentPage");
        }


    }
}
