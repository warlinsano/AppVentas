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
    public class RegisterClientsContentPageViewModel : ViewModelBase
    {
        private readonly INavigationService _NavigationService;
        private SQLiteConnection _conn;
        private SQLiteAsyncConnection _connAsnyc;
        private DelegateCommand _agregarButton;
        private DelegateCommand _CancelarButton;
        private bool _isEnabled;
        private string _firstName;
        private string _lastName;
        private string _cedula;
        private string _adress;
        private string _telephone;

        public RegisterClientsContentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Registrar Clientes";
            IsEnabled = true;
            _NavigationService = navigationService;
            _connAsnyc = DependencyService.Get<ISQLiteDB>().GetConnectionAsync();
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
        }
        public DelegateCommand agregarButton => _agregarButton ?? (_agregarButton = new DelegateCommand(Agregar));
        public DelegateCommand CancelarButton => _CancelarButton ?? (_CancelarButton = new DelegateCommand(IrALsiatdo));

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string Cedula
        {
            get => _cedula;
            set => SetProperty(ref _cedula, value);
        }
        public string Adress
        {
            get => _adress;
            set => SetProperty(ref _adress, value);
        }
        public string Telephone
        {
            get => _telephone;
            set => SetProperty(ref _telephone, value);
        }


        private async void Agregar()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Informacion", "Deve llenar el campo Nombre.", "Acceptar");
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Informacion", "Deve llenar el campo Apellido.", "Acceptar");
                return;
            }
            if (string.IsNullOrEmpty(Cedula))
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Informacion", "Deve llenar el campo Cedula.", "Acceptar");
                return;
            }
            if (string.IsNullOrEmpty(Adress))
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Informacion", "Deve llenar el campo Adress", "Acceptar");
                return;
            }
            if (string.IsNullOrEmpty(Telephone))
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Informacion", "Deve llenar el campo Telefono", "Acceptar");
                return;
            }

            IsEnabled = false;
            try
            {
                var ExistClientByDocumnet = _conn.Table<Clientes>().Where(x => x.Cedula == Cedula).Count();
                //TODO: falta validar cedula, validar espacios y doble espacios..
                if (ExistClientByDocumnet > 0)
                {
                    await App.Current.MainPage.DisplayAlert("Informacion", "Ya existe un cliente registrado con esta Cedula, favor intente con otra", "Acceptar");
                    Cedula = string.Empty;
                    IsEnabled = true;
                    return;
                }
                var DatosRegistro = new Clientes
                {
                    FirstName = FirstName.Trim(),
                    LastName = LastName.Trim(),
                    Cedula = Cedula.Trim(),
                    Adress = Adress.Trim(),
                    Telephone = Telephone.Trim()
                };
                await _connAsnyc.InsertAsync(DatosRegistro);

                IsEnabled = true;
                FirstName = string.Empty;
                LastName = string.Empty;
                Cedula = string.Empty;
                Adress = string.Empty;
                Telephone = string.Empty;

                await App.Current.MainPage.DisplayAlert("Informacion", "Guardado Con Exito....", "Acceptar");
                await NavigationService.NavigateAsync("/VentasMasterDetailPage/NavigationPage/ListClientsContentPage");
            }
            catch (Exception ex)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", " La operacion Registar No se pudo ser completada: " + ex, "Acceptar");
                return;
            }
        }
        private async void IrALsiatdo()
        {
            //var ResulRegistros = await _connAsnyc.Table<Clientes>().ToListAsync();
            //Settings.ListClients = new ObservableCollection<Clientes>(ResulRegistros);
            await NavigationService.NavigateAsync("/VentasMasterDetailPage/NavigationPage/ListClientsContentPage");
        }
    }
}
