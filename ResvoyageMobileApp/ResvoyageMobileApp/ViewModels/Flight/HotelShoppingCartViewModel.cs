using ResvoyageMobileApp.Models.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class HotelShoppingCartViewModel : BaseViewModel
    {
        public HotelShoppingCartViewModel(HotelInformation selectedHotel)
        {
            _selectedHotel = selectedHotel;
        }
        private HotelInformation _selectedHotel;

        public HotelInformation SelectedHotel
        {
            get { return _selectedHotel; }
            set { SetValue(ref _selectedHotel, value); }
        }
    }
}
