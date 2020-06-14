using AppVentas.Data;
using AppVentas.Helpers;
using AppVentas.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AppVentas.ViewModels
{
    public class EditClientContentPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private SQLiteConnection _conn;
        private SQLiteAsyncConnection _connAsnyc;
        private DelegateCommand _editarButton;
        //private DelegateCommand _guardarButton;
        private DelegateCommand _eliminarButton;
        //private DelegateCommand _CancelarButton;
        private bool _isEnabled;
        private bool _isVisible;
        private string _firstName;
        private string _lastName;
        private string _cedula;
        private string _adress;
        private string _telephone;

        public EditClientContentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
             Title = "Detalles";
            _navigationService = navigationService;
            _connAsnyc = DependencyService.Get<ISQLiteDB>().GetConnectionAsync();
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
            LoadClient();
            IsEnabled = true;
        }
        public DelegateCommand EditarButton => _editarButton ?? (_editarButton = new DelegateCommand(SaveClient));
        //public DelegateCommand GuardarButton => _guardarButton ?? (_guardarButton = new DelegateCommand(SaveClient));
        public DelegateCommand EliminarButton => _eliminarButton ?? (_eliminarButton = new DelegateCommand(DeleteClient));
        //public DelegateCommand CancelarButton => _CancelarButton ?? (_CancelarButton = new DelegateCommand(Cancelar));
     
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
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

        private void LoadClient()
        {
            FirstName = Settings.Clients.FirstName;
            LastName = Settings.Clients.LastName;
            Cedula = Settings.Clients.Cedula;
            Telephone = Settings.Clients.Telephone;
            Adress = Settings.Clients.Adress;
        }

        //private void EditClient()
        //{
        //    IsEnabled = true; //campos habilitados
        //    IsVisible = true; // guadar y canselar
        //    IsVisible = false; // editar y Eliminar

        //}
        private async void DeleteClient()
        {
            IsEnabled = false;
            try
            {
                //var client = _conn.Table<Clientes>().Where(x => x.Cedula == Cedula).FirstOrDefault();
                //if (client == null)
                //{
                //    await App.Current.MainPage.DisplayAlert("Informacion", "Ya Cliente llano existe...", "Acceptar");
                //    IsEnabled = true;
                //    return;
                //}
               if( await App.Current.MainPage.DisplayAlert("Informacion", "Esta seguro que desea eliminar este cliente?... Si tiene factuaras estas tambien seran eliminadas", "Acceptar", "Cancelar")) 
                {
                    var delete = _conn.Table<Clientes>().Delete(x => x.Cedula == Cedula);
                    await App.Current.MainPage.DisplayAlert("Informacion", "Se elimino Con exito el Cliente....", "Acceptar");
                    await NavigationService.NavigateAsync("/VentasMasterDetailPage/NavigationPage/ListClientsContentPage");
                }

            }
            catch (Exception ex)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", " La operacion de actualizar No se pudo ser completada: " + ex, "Acceptar");
                return;
            }

        }
        private async void SaveClient()
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
                var client = _conn.Table<Clientes>().Where(x => x.ClienteId == Settings.Clients.ClienteId).FirstOrDefault();
                client.FirstName = FirstName.Trim();
                client.LastName = LastName.Trim();
                client.Cedula = Cedula.Trim();
                client.Adress = Adress.Trim();
                client.Telephone = Telephone.Trim();

                var ExistClientByDocumnet = _conn.Table<Clientes>().Where(x => x.Cedula == Cedula && x.ClienteId != client.ClienteId).Count();
                //TODO: falta validar cedula, validar espacios y doble espacios..
                if (ExistClientByDocumnet > 0)
                {
                    await App.Current.MainPage.DisplayAlert("Informacion", "Ya existe un cliente registrado con esta Cedula, favor intente con otra", "Acceptar");
                    Cedula = string.Empty;
                    IsEnabled = true;
                    return;
                }
                _conn.Update(client);
                await App.Current.MainPage.DisplayAlert("Informacion", "Se Actualizo Con Exito....", "Acceptar");
                await NavigationService.NavigateAsync("/VentasMasterDetailPage/NavigationPage/ListClientsContentPage");
            }
            catch (Exception ex)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", " La operacion de actualizar No se pudo ser completada: " + ex, "Acceptar");
                return;
            }
        }

        //private async void Cancelar()
        //{
        //    IsEnabled = false; //campos inhabilitados
        //    IsVisible = false; // guadar y canselar
        //    IsVisible = true; // editar y Eliminar

        //    //var ResulRegistros = await _connAsnyc.Table<Clientes>().ToListAsync();
        //    //Settings.ListClients = new ObservableCollection<Clientes>(ResulRegistros);
        //    await NavigationService.NavigateAsync("/VentasMasterDetailPage/NavigationPage/ListClientsContentPage");
        //}

    }
}
