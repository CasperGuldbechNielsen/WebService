using Windows.UI.Popups;
using HotelMVVM.Model;
using HotelMVVM.Persistancy;
using HotelMVVM.ViewModel;

namespace HotelMVVM.Handler
{
    public class HotelHandler
    {
        public HotelViewModel HotelViewModel { get; set; }

        public HotelHandler(HotelViewModel hotelViewModel)
        {
            HotelViewModel = hotelViewModel;
        }

        public void CreateHotel()
        {
            Hotel hotel = new Hotel(HotelViewModel.NewHotel.Hotel_Number,
                HotelViewModel.NewHotel.Hotel_Name,
                HotelViewModel.NewHotel.Hotel_Address);

            new PersistenceFacade().SaveHotel(hotel);

            var hotels = new PersistenceFacade().GetHotels();

            HotelViewModel.HotelCatalogSingleton.Hotels.Clear();
            foreach (var hotel1 in hotels)
            {
                HotelViewModel.HotelCatalogSingleton.Hotels.Add(hotel1);
            }

            HotelViewModel.NewHotel.Hotel_Number = 0;
            HotelViewModel.NewHotel.Hotel_Name = "";
            HotelViewModel.NewHotel.Hotel_Address = "";

        }

        public void DeleteHotel()
        {
            new PersistenceFacade().DeleteHotel(HotelViewModel.SelectedHotel);

            var hotels = new PersistenceFacade().GetHotels();

            HotelViewModel.HotelCatalogSingleton.Hotels.Clear();
            foreach (var hotel in hotels)
            {
                HotelViewModel.HotelCatalogSingleton.Hotels.Add(hotel);
            }
        }

        public void UpdateHotel()
        {
            Hotel hotel = new Hotel(HotelViewModel.NewHotel.Hotel_Number,
                HotelViewModel.NewHotel.Hotel_Name,
                HotelViewModel.NewHotel.Hotel_Address);

            new PersistenceFacade().UpdateHotel(hotel);

            var hotels = new PersistenceFacade().GetHotels();

            HotelViewModel.HotelCatalogSingleton.Hotels.Clear();
            foreach (var hotel1 in hotels)
            {
                HotelViewModel.HotelCatalogSingleton.Hotels.Add(hotel1);
            }

            HotelViewModel.NewHotel.Hotel_Number = 0;
            HotelViewModel.NewHotel.Hotel_Name = "";
            HotelViewModel.NewHotel.Hotel_Address = "";
        }
    }
}