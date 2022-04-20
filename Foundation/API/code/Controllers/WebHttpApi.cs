using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace Sitecore.Foundation.API.Controllers
{
    public class WebHttpApi : Controller
    {
        //Get CarModelAll
        public static string ApiCall(string ApiPath,string Url)
        {
            string result = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiPath); //baseUrl
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET
                    HttpResponseMessage resposes =  client.GetAsync(Url).Result;

                    if (resposes.IsSuccessStatusCode)
                    {
                       result =  resposes.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                result = "{\"Message\":" + ex.Message + "\"}";
            }
            return(result);
        }

    }

    }
