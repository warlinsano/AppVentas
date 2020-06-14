using AppVentas.Helpers;
using AppVentas.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppVentas.ViewModels
{
    public class ClientsItemViewModel : Clientes
    {

        private readonly INavigationService _navigationService;
        private DelegateCommand _SelectClientCommand;

        public ClientsItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectClientCommand => _SelectClientCommand ?? (_SelectClientCommand = new DelegateCommand(SelectPet));

        private async void SelectPet()
        {
             Settings.Clients = this;
            ///VentasMasterDetailPage/NavigationPage/
            await _navigationService.NavigateAsync("ClientTabbedPage");
        }
    }
}
