using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Windows.UI.Popups;
using HotelMVVM.Model;
using Newtonsoft.Json;

namespace HotelMVVM.Persistancy
{
    /// <summary>
    /// A class for connecting to the database
    /// </summary>
    internal class PersistenceFacade
    {
        private const string ServerUrl = "http://localhost:50000";
        private HttpClientHandler handler;

        public PersistenceFacade()
        {
            handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
        }

        /// <summary>
        /// Retrieves all hotels in the database
        /// </summary>
        /// <returns>Returns a list of all hotels</returns>
        public List<Hotel> GetHotels()
        {
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
                        var hotelList = response.Content.ReadAsAsync<IEnumerable<Hotel>>().Result;
                        return hotelList.ToList();
                    }

                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
                }
                return null;
            }
        }
        /// <summary>
        /// Saves a hotel to the databse
        /// </summary>
        /// <param name="hotel">Pass in the hotel wanted to be saved</param>
        public void SaveHotel(Hotel hotel)
        {
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    string postBody = JsonConvert.SerializeObject(hotel);
                    var content = new StringContent(postBody, Encoding.UTF8, "application/json");
                    var response = client.PostAsync("api/Hotels", content).Result;
                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
        /// <summary>
        /// Detele a hotel from the database
        /// </summary>
        /// <param name="hotel">Pass in the hotel wanted to be deleted</param>
        public void DeleteHotel(Hotel hotel)
        {
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = client.DeleteAsync("api/Hotels/" + hotel.Hotel_Number).Result;
                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        /// <summary>
        /// Updates a given hotel
        /// </summary>
        /// <param name="hotel">Pass in the hotel wanted to be updated</param>
        public void UpdateHotel(Hotel hotel)
        {
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    string postBody = JsonConvert.SerializeObject(hotel);
                    var content = new StringContent(postBody, Encoding.UTF8, "application/json");

                    var response = client.PutAsync("api/Hotels/" + hotel.Hotel_Number, content).Result;
                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
    }
}
