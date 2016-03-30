using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebService;

namespace WebServiceClientConsole
{
    internal class Program
    {
        const string ServerUrl = "http://localhost:50000";

        private static void Main(string[] args)
        {


            Console.Write(
                "1.List hotels\n2.Select a hotel\n3.List all hotels in Roskilde\n4.List all single rooms for hotels in Roskilde\n5.Update hotel_No 3\n6.Insert a new hotel");
            Console.Write("\n7.Delete a hotel\n8.Update Room prices\n9.List all single rooms in Roskilde using a View\n10.Update Room prices using view\n0.End\nPlease enter your choice:");
            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        Exercise1();
                        break;
                    case 2:
                        Exercise2();
                        break;
                    case 3:
                        Exercise3();
                        break;
                    case 4:
                        Exercise4();
                        break;
                    case 5:
                        Exercise5();
                        break;
                    case 6:
                        Exercise6();
                        break;
                    case 7:
                        Exercise7();
                        break;
                    case 8:
                        Exercise8();
                        break;
                    case 9:
                        Exercise9();
                        break;
                    case 10:
                        Exercise10();
                        break;
                }
                Console.Write(
                    "1.List hotels\n2.Select a hotel\n3.List all hotels in Roskilde\n4.List all single rooms for hotels in Roskilde\n5.Update hotel_No 3\n6.Insert a new hotel");
                Console.Write("\n7.Delete a hotel\n8.Update Room prices\n9.List all single rooms in Roskilde using a View\n10.Update Room prices using view\n0.End\nPlease enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.Clear();
            }
        }

        private static void Exercise1()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/Hotels").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Hotel> hotelData = response.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;
                        foreach (var hotel in hotelData)
                        {
                            Console.WriteLine(hotel);
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }

        private static void Exercise2()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    Console.Write("Please enter a hotel number: ");
                    int hotelNumber = Int32.Parse(Console.ReadLine());

                    var response = client.GetAsync("api/Hotels/" + hotelNumber).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Hotel hotelData = response.Content.ReadAsAsync<Hotel>().Result;

                        Console.WriteLine(hotelData);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }


        private static void Exercise3()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/Hotels/").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Hotel> hotelData = response.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;

                        var hotelsInRoskilde =
                            from hotel in hotelData
                            where hotel.Hotel_Address.Contains("Roskilde")
                            select hotel;

                        foreach (var hotel in hotelsInRoskilde)
                        {
                            Console.WriteLine(hotel);
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
            Console.ReadKey();
        }

        private static void Exercise4()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/Hotels").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var response1 = client.GetAsync("api/Rooms").Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            IEnumerable<Hotel> hotelData = response.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;
                            IEnumerable<Room> roomData = response1.Content.ReadAsAsync<IEnumerable<Room>>().Result;

                            var roomQuery =
                                from hotel in hotelData
                                where hotel.Hotel_Address.Contains("Roskilde")
                                join room in roomData on hotel.Hotel_Number equals room.Hotel_Number
                                select new {hotel, room};
                            
                            foreach (var room in roomQuery)
                            {
                                Console.WriteLine("Hotel name: {0}\t Hotel number: {1}\t Room number: {2}\t Address: {3}", room.hotel.Hotel_Name, room.hotel.Hotel_Number, room.room.Room_Number, room.hotel.Hotel_Address);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }

        private static void Exercise5()
        {
            Console.Write("Please enter the number of the hotel you want to change: ");
            int hotelNumber = int.Parse(Console.ReadLine());

            Console.Write("Please enter the new name: ");
            var hotelName = Console.ReadLine();

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = client.GetAsync("api/Hotels/" + hotelNumber).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Hotel hotelData = response.Content.ReadAsAsync<Hotel>().Result;
                        hotelData.Hotel_Name = hotelName;

                        //we have to serialize the hotel3 object into json format
                        string jsonHotel3 = JsonConvert.SerializeObject(hotelData);

                        //Create the content we want to send with the Http put request
                        StringContent content = new StringContent(jsonHotel3, Encoding.UTF8, "Application/json");

                        //Using a Http Put Request we can update the Hotel number 3
                        var updateResponse = client.PutAsync("api/hotels/" + hotelNumber, content).Result;

                        if (updateResponse.IsSuccessStatusCode)
                        {
                            var response1 = client.GetAsync("api/Hotels/" + hotelNumber).Result;
                            if (response1.IsSuccessStatusCode)
                            {
                                Hotel newHotel = response1.Content.ReadAsAsync<Hotel>().Result;

                                Console.WriteLine(newHotel);
                            }

                        }

                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }

        private static void Exercise6()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/Hotels").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.Write("Please enter a name for the hotel: ");
                        var hotelName = Console.ReadLine();

                        Console.Write("Please enter an address for the hotel: ");
                        var hotelAddress = Console.ReadLine();

                        Hotel newHotel = new Hotel();

                        newHotel.Hotel_Name = hotelName;
                        newHotel.Hotel_Address = hotelAddress;

                        IEnumerable<Hotel> hotelData = response.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;

                        int hotelNumber = 0;

                        foreach (var hotel in hotelData)
                        {
                            hotelNumber = hotel.Hotel_Number;
                        }

                        newHotel.Hotel_Number = hotelNumber + 1;

                        //we have to serialize the hotel3 object into json format
                        string jsonHotel3 = JsonConvert.SerializeObject(newHotel);

                        //Create the content we want to send with the Http put request
                        StringContent content = new StringContent(jsonHotel3, Encoding.UTF8, "Application/json");

                        //Using a Http Put Request we can update the Hotel number 3
                        var updateResponse = client.PostAsync("api/Hotels", content).Result;
                        if (updateResponse.IsSuccessStatusCode)
                        {
                            hotelNumber = newHotel.Hotel_Number;
                            var response1 = client.GetAsync("api/Hotels/" + hotelNumber).Result;
                            if (response1.IsSuccessStatusCode)
                            {
                                Hotel hotel = response1.Content.ReadAsAsync<Hotel>().Result;

                                Console.WriteLine(hotel);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }

        private static void Exercise7()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    Console.Write("Please enter the number of the hotel you want to delete: ");
                    int hotelNumber = Int32.Parse(Console.ReadLine());

                    var response = client.DeleteAsync("api/Hotels/" + hotelNumber).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Hotel deleted. Now searching for the hotel to make sure...");

                        var response1 = client.GetAsync("api/Hotels").Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            IEnumerable<Hotel> hotelData = response1.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;

                            foreach (var hotel in hotelData)
                            {
                                Console.WriteLine(hotel);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }

        private static void Exercise8()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/Hotels/").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var response1 = client.GetAsync("api/Rooms").Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            IEnumerable<Hotel> hotelData = response.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;
                            IEnumerable<Room> roomData = response1.Content.ReadAsAsync<IEnumerable<Room>>().Result;

                            var hotelsInRoskilde =
                                from hotel in hotelData
                                where hotel.Hotel_Address.Contains("Roskilde")
                                select hotel;

                            var roomsInHotel =
                                from room in roomData
                                select room;

                            var roomsInRoskilde =
                                from hotel in hotelsInRoskilde
                                join room in roomsInHotel on hotel.Hotel_Number equals room.Hotel_Number
                                where room.Room_Type == "S"
                                select room;

                            foreach (var room in roomsInRoskilde)
                            {
                                Console.WriteLine("Hotel Number: {0}\t Room Number: {1}\t Room Type: {2}\t Room Price: {3}", room.Hotel_Number, room.Room_Number, room.Room_Type, room.Room_Price);
                            }

                            foreach (var room in roomsInRoskilde)
                            {
                                room.Room_Price *= 1.20;

                                //we have to serialize the hotel3 object into json format
                                string jsonRoom = JsonConvert.SerializeObject(room);

                                //Create the content we want to send with the Http put request
                                StringContent roomContent = new StringContent(jsonRoom, Encoding.UTF8, "Application/json");

                                //Using a Http Put Request we can update the Hotel number 3
                                var roomResponse = client.PutAsync("api/Rooms/" + room.Room_Number, roomContent).Result;
                                if (roomResponse.IsSuccessStatusCode)
                                {
                                    Console.WriteLine("Success!");
                                }
                            }

                            var roomResponse1 = client.GetAsync("api/Rooms").Result;
                            if (response1.IsSuccessStatusCode)
                            {
                                var hotelResponse1 = client.GetAsync("api/Hotels/").Result;
                                if (hotelResponse1.IsSuccessStatusCode)
                                {
                                    IEnumerable<Room> rooms =
                                        roomResponse1.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                                    IEnumerable<Hotel> hotels =
                                        hotelResponse1.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;


                                    var hotelsInRoskilde1 =
                                        from a in hotels
                                        join b in rooms on a.Hotel_Number equals b.Hotel_Number
                                        where a.Hotel_Address.Contains("Roskilde")
                                        select new { a, b };

                                    foreach (var item in roomsInRoskilde)
                                    {
                                        Console.WriteLine(
                                            "Hotel Number: {0}\t Room Number: {1}\t Room Type: {2}\t Room Price: {3}",
                                            item.Hotel_Number, item.Room_Number, item.Room_Type,
                                            item.Room_Price);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }

        private static void Exercise9()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/SingleRooms").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<SingleRoom> singleRooms = response.Content.ReadAsAsync<IEnumerable<SingleRoom>>().Result;
                        foreach (var room in singleRooms)
                        {
                            Console.WriteLine(room);
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }

        private static void Exercise10()
        {
            throw new NotImplementedException();
        }
    }
}
