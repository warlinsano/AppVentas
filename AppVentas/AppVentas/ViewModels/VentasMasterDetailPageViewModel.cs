using AppVentas.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppVentas.ViewModels
{

        public class VentasMasterDetailPageViewModel : ViewModelBase
        {

            private readonly INavigationService _navigationService;

            public VentasMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
            {
                _navigationService = navigationService;
                LoadMenus();
            }

            public ObservableCollection<MenuItemViewModel> Menus { get; set; }

            private void LoadMenus()
            {
                var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_pets_menu",
                    PageName = "RegisterContentPage",
                    Title = "My Business"
                    //My mobile business
                },

                new Menu
                {
                    //Icon = "ic_list_alt",
                    //PageName = "AgendaPage",
                    //Title = "My Agenda"
                    Icon = "ic_list_alt",
                    PageName = "ListClientsContentPage",
                    Title = "Clientes"
                },

                new Menu
                {
                    Icon = "ic_map",
                    PageName = "CreateFacturasContentPage",
                    Title = "Facturas"
                },

                new Menu
                {
                    Icon = "ic_person",
                    PageName = "ProfilePage",
                    Title = "Modify Profile"
                },

                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Logout"
                }
            };

                Menus = new ObservableCollection<MenuItemViewModel>(
                    menus.Select(m => new MenuItemViewModel(_navigationService)
                    {
                        Icon = m.Icon,
                        PageName = m.PageName,
                        Title = m.Title
                    }).ToList());
            }
        }
    }

