using System.Collections.ObjectModel;
using HotelMVVM.Persistancy;

namespace HotelMVVM.Model
{
    public class HotelCatalogSingleton
    {
        private static HotelCatalogSingleton instance = new HotelCatalogSingleton();

        public static HotelCatalogSingleton Instance
        {
            get { return instance; }
        }

        public ObservableCollection<Hotel> Hotels { get; set; }

        private HotelCatalogSingleton()
        {
            Hotels = new ObservableCollection<Hotel>();
            /*
            Hotel h1 = new Hotel(101, "London Hotel", "London Boulevard");
            Hotel h2 = new Hotel(102, "Berlin Hotel", "Berliner garten");
            Hotels.Add(h1);
            Hotels.Add(h2);
            */
            Hotels = new ObservableCollection<Hotel>(new PersistenceFacade().GetHotels());
        }

        public void Add(int Hotel_No, string Name, string Address)
        {
            Hotels.Add(new Hotel(Hotel_No, Name, Address));
        }
    }
}
