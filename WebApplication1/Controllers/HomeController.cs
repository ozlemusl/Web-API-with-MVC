using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication2;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // Base URL adress holding on string
        public string BaseURL = "http://localhost:16681/";
        public async Task<IActionResult> GetAllProducts()
        {
            List<WebApplication2.Product> products = new List<WebApplication2.Product>();
            using (var client = new HttpClient())
            {
                //Defining client variable
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                //Defining request type that going to be accepted, in case app or json format
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Creating response variable, it is async call because wep api will support async calls
                HttpResponseMessage Res = await client.GetAsync("api/Products");
                if(Res.IsSuccessStatusCode)
                {
                    //store variable in result
                    var Result = Res.Content.ReadAsStringAsync().Result;
                    // Convert result to list
                    products = JsonConvert.DeserializeObject<List<Product>>(Result);
                }
            }
            return View(products);
        }
    }
}
