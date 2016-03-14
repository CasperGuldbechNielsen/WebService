using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebService;

namespace WebServiceClientConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            const string ServerUrl = "http://localhost:50000";

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

            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();
        }
    }
}
