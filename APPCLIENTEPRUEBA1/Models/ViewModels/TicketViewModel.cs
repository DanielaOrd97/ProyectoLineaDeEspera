using APPCLIENTEPRUEBA1.Models.DTOs;
using APPCLIENTEPRUEBA1.Models.Entities;
using APPCLIENTEPRUEBA1.Repositories;
using APPCLIENTEPRUEBA1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCLIENTEPRUEBA1.Models.ViewModels
{
    public partial class TicketViewModel : ObservableObject
    {
        CajasRepository CajasRepository = new();

        public ObservableCollection<Caja> ListaCajas { get; set; } = new();

        Service service = new();

        [ObservableProperty]
        private CajaDTO? caja;

        public TicketViewModel()
        {
            CargarCajas();
            //EVENTO
            service.DatosActualizados += Service_DatosActualizados;
        }

        private void Service_DatosActualizados()
        {
            CargarCajas();
        }

        async void CargarCajas()
        {
            if(ListaCajas.Count == 0)
            {
                await service.GetCajas();
            }

            ListaCajas.Clear();

            foreach (var caja in CajasRepository.GetAll())
            {
                ListaCajas.Add(caja);
            }
        }
    }
}
