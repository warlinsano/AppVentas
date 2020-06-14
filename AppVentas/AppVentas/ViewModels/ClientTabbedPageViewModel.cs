using AppVentas.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppVentas.ViewModels
{
    public class ClientTabbedPageViewModel : ViewModelBase
    {
        public ClientTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = $"Cliente: {Settings.Clients.FullName}";
        }
    }
}
