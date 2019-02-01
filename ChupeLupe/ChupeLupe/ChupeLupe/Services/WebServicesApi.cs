using ChupeLupe.Services;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ChupeLupe.Models;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using FireSharp.Extensions;

[assembly: Dependency(typeof(WebServicesApi))]
namespace ChupeLupe.Services
{
    public class WebServicesApi : IWebServicesApi
    {
        const string _baseUrl = "https://chupelupedw.firebaseio.com/";
        const string _authSecret = "DmBKuDnLnk1ItOgHEzJhFC44NjbWeYROVyjlB37e";
        public IFirebaseClient Client { get; set; }

        public WebServicesApi()
        {
            if(Client == null)
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = _authSecret,
                    BasePath = _baseUrl
                };
                Client = new FirebaseClient(config);
            }
        }   

        public async Task<List<Promotion>> GetPromotions()
        {
            List<Promotion> promotions = new List<Promotion>();
            try
            {
                
                FirebaseResponse response = await Client.GetAsync("promotions");
                if(response == null)
                {
                    return promotions;
                }
                if (string.IsNullOrEmpty(response.Body))
                {
                    return promotions;
                }
                var jsonResponse = response.Body;
                var jsonObject = JObject.Parse(jsonResponse);
                foreach(var item in jsonObject)
                {
                    var promotion = await Task.Run(() =>
                    JsonConvert.DeserializeObject<Promotion>(item.Value.ToJson()));
                    if(promotion == null)
                    {
                        continue;
                    }
                    promotions.Add(promotion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.Source);
            }
            return promotions;
        }
    }
}
