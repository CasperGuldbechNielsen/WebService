using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HotelMVVM.Common;
using HotelMVVM.Model;

namespace HotelMVVM.ViewModel
{
    public class HotelViewModel : INotifyPropertyChanged
    {
        private Hotel _newHotel;
        private static Hotel _selectedHotel;

        public Hotel SelectedHotel
        {
            get { return _selectedHotel; }
            set { _selectedHotel = value; OnPropertyChanged();
            }
        }

        public Hotel NewHotel
        {
            get { return _newHotel; }
            set { _newHotel = value; OnPropertyChanged(); }
        }

        public Handler.HotelHandler HotelHandler { get; set; }

        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SelectedEventCommand { get; set; }
        public ICommand UpdateCommand { get; set; }


        public HotelCatalogSingleton HotelCatalogSingleton { get; set; }

        public HotelViewModel() 
        {
            HotelCatalogSingleton = HotelCatalogSingleton.Instance;
            NewHotel = new Hotel();
            HotelHandler = new Handler.HotelHandler(this);
            CreateCommand = new RelayCommand(HotelHandler.CreateHotel);
            DeleteCommand = new RelayCommand(HotelHandler.DeleteHotel);
            UpdateCommand = new RelayCommand(HotelHandler.UpdateHotel);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]     string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}