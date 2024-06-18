using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.Http;
using System.Threading.Tasks;
namespace AppClientes
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<string, (int counter, string estado)> serviceCounters = new Dictionary<string, (int, string)>();
        //{
        //    {"Caja 1", 1 },
        //    {"Caja 2",1 },
        //    { "Caja 3",1 }
        //};
        private string selectedService;
        private HttpClient httpClient = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
            //ServicePicker.SelectedIndex = 0; //Este selecciona el primer servicio por defecto
            //selectedService = ServicePicker.Items[0];
            //UpdateNumberLabel();
            //ServicePicker.SelectedIndexChanged += OnServiceSelected;
        }

        private void OnServiceSelected(object? sender, EventArgs e)
        {
            //if (ServicePicker.SelectedItem != null)
            //{
            //    selectedService = ServicePicker.SelectedItem.ToString();
            //    UpdateNumberLabel();
            ////}
        }


        private async Task LoadServicesFromApi()
        {
            string apiUrl = "AQUI_VA_LA_API";//URL DE LA API PARA DEVOLVER LOS SERVICIOS

            try
            {
                var response = await httpClient.GetStringAsync(apiUrl);
                var services = JsonConvert.DeserializeObject<List<string>>(response);
                serviceCounters.Clear();
                foreach (var service in services)
                {
                    serviceCounters[service] = (1, "En Espera");//inicializar el contador para cada servicio
                }
                //ServicePicker.ItemsSource = services;
                if (services.Count > 0)
                {
                    //ServicePicker.SelectedIndex = 0;
                    selectedService = services[0];
                    UpdateNumberLabel();
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", "No se pudieron cargar los servicios: " + ex.Message, "OK");
            }
        }

        private void UpdateNumberLabel()
        {
            if (string.IsNullOrEmpty(selectedService)) return;
            var (counter, estado) = serviceCounters[selectedService];
            //    NumberLabel.Text = counter.ToString();
            //    TurnStateLabel.Text = estado;
            //}
        }
    }
}
//        private void Button_Clicked(object sender, EventArgs e)
//        {
//            //verifica si hay un servicio seleccionado
//            if(string.IsNullOrEmpty(selectedService)) return;
//            var (counter,estado)=serviceCounters[selectedService];
//            counter++;
//            estado = "En Espera";

//            //Inrementa el contador del servicio
//            serviceCounters[selectedService] =(counter,estado);
//            //actualiza label
//            UpdateNumberLabel() ;
//        }

//        private void ServicePicker_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            selectedService = ServicePicker.SelectedItem.ToString();
//            UpdateNumberLabel();
//        }

//        private async void Button_Clicked_1(object sender, EventArgs e)
//        {
//            await LoadServicesFromApi();
//        }
//        //Este metodo es opcional si lo quieren dejar, es para cambiar el estado del turno
//        private void ChangeTurnState(string newState)
//        {
//            if (string.IsNullOrEmpty(selectedService)) return;
//            var(counter, estado)=serviceCounters[selectedService];
//            estado=newState;
//            serviceCounters[selectedService]=(counter,estado);
//            UpdateNumberLabel();
//        }
//    }

//}
