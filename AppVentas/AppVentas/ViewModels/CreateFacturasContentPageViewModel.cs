using AppVentas.Data;
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
    public class CreateFacturasContentPageViewModel : ViewModelBase
    {
        private readonly INavigationService _NavigationService;
        private SQLiteConnection _conn;
        private SQLiteAsyncConnection _connAsnyc;
        private DelegateCommand _agregarButton;
        private bool _isEnabled;
        private decimal _total;
        private string _cedula;

        public CreateFacturasContentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Nueva Factura";
            IsEnabled = true;
            _NavigationService = navigationService;
            _connAsnyc = DependencyService.Get<ISQLiteDB>().GetConnectionAsync();
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
        }
        public DelegateCommand agregarButton => _agregarButton ?? (_agregarButton = new DelegateCommand(AgregarFactura));

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public decimal Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }
        public string Cedula
        {
            get => _cedula;
            set => SetProperty(ref _cedula, value);
        }
        private async void AgregarFactura()
        {
            if (string.IsNullOrEmpty(Cedula))
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Informacion", "Deve llenar el campo Cedula.", "Acceptar");
                return;
            }
            if (Total.ToString()=="0")
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Informacion", "Deve llenar el campo total.", "Acceptar");
                return;
            }

            IsEnabled = false;
            try
            {
                var cliente = _conn.Table<Clientes>().Where(x => x.Cedula == Cedula).FirstOrDefault();
                //TODO: falta validar cedula, validar espacios y doble espacios..
                if (cliente == null)
                {
                    await App.Current.MainPage.DisplayAlert("Informacion", "No existe un cliente registrado con esta Cedula, favor intente con otra", "Acceptar");
                    Cedula = string.Empty;
                    IsEnabled = true;
                    return;
                }

                var Factura = new Facturas
                {
                    Total = Total,
                    Fecha = DateTime.Now,
                    Saldada = true,
                    IsCredito = false,
                    ClienteId = cliente.ClienteId
                };
                var rows = _conn.Insert(Factura);

                //cliente.Facturas.Add(new Facturas
                //{
                //    Total = Total,
                //    Fecha = DateTime.Now,
                //    Saldada = true,
                //    IsCredito = false,
                //});

                //await _connAsnyc.InsertAsync(DatosRegistro);
                IsEnabled = true;
                Total = 0;
                Cedula = string.Empty;
             
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

    }
}
