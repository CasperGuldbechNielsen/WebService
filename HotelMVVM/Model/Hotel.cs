namespace HotelMVVM.Model
{
    public class Hotel
    {
        public int Hotel_Number { get; set; }

        public string Hotel_Name { get; set; }

        public string Hotel_Address { get; set; }

        public Hotel(int hotelNo, string name, string address)
        {
            Hotel_Number = hotelNo;
            Hotel_Name = name;
            Hotel_Address = address;
        }

        public Hotel()
        {
            
        }

        public override string ToString()
        {
            return string.Format("Hotel Number: {0}\n Name: {1}\n Address: {2}", Hotel_Number, Hotel_Name, Hotel_Address);
        }
    }
}