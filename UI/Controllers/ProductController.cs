using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using web_api.Models;

namespace UI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

            private readonly HttpClient _client;

            public ProductController()
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:7269/api/");
            }

        // GET: Product/Index
        //public IActionResult Index()
        //{
        //    List<Product> productList = new List<Product>();
        //    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/grocessary/GetAllProduct/all").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = response.Content.ReadAsStringAsync().Result;
        //        productList = JsonConvert.DeserializeObject<List<Product>>(data);
        //    }


        //    return View(productList);
        
        //}
        
        HttpClient client = new HttpClient();

        //public ActionResult Index()
        //{
        //    List<Product> products = new List<Product>();
        //    client.BaseAddress = new Uri("https://localhost:7269/api/");
        //    var response = client.GetAsync("Grocessary");
        //    response.Wait();    
            
        //    var test = response.Result;
        //    if(test.IsSuccessStatusCode)
        //    {
        //        //var display = test.Content.ReadAsAsync<List<Product>>();
        //        var display = test.Content.ReadAsAsync<List<Product>>();
        //        display.Wait();
        //        products= display.Result;
        //    }
        //    return View(products);

        //}

        public async Task<ActionResult> Index()
        {
            List<Product> products = new List<Product>();

            // Call API endpoint
            HttpResponseMessage response = await _client.GetAsync("Grocessary/GetAllProduct");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(data);
            }

            return View(products);
        }



        [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            
            [HttpPost]
            public IActionResult Create(Product model)
            {
                try
                {
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Grocessary/AddProduct", content).Result;
                   

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Product Created.";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                    return View();
                }

                return View();
            }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Product product = new Product();

                //HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Grocessary/GetProductById").Result;
                HttpResponseMessage response = await _client.GetAsync($"Grocessary/GetProductById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(data);
                }
                return View(product); // view ma product pass karvathi data show kare chhe je product change karvanu hoy a
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }

        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(Product model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    try
        //    {
        //        string data = JsonConvert.SerializeObject(model);
        //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        //        // Await the async call
        //        HttpResponseMessage response = await _client.PutAsync($"api/Grocessary/UpadateProduct/{model.Id}", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["successMessage"] = "Product updated successfully!";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            string error = await response.Content.ReadAsStringAsync();
        //            TempData["errorMessage"] = $"Update failed: {error}";
        //            return View(model); // keep the model so the form is filled
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["errorMessage"] = $"Exception: {ex.Message}";
        //        return View(model);
        //    }
        //}


        [HttpPost]
        public IActionResult Edit(Product model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                //HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "Grocessary/UpadateProduct", content).Result;
                //HttpResponseMessage response = _client.PutAsync($"Grocessary/UpdateProduct/{model.Id}", content).Result;
                HttpResponseMessage response = _client.PutAsync($"api/Grocessary/UpadateProduct/{model.Id}", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Exception: {ex.Message}";
            }
            return View();
        }


        //[HttpPost]
        //public IActionResult Edit(Product model)
        //{
        //    try
        //    {

            //        string data = JsonConvert.SerializeObject(model);

            //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");


            //        HttpResponseMessage response = _client.PutAsync($"api/Grocessary/UpadateProduct/{model.Id}", content).Result;

            //        if (response.IsSuccessStatusCode)
            //        {
            //            TempData["successMessage"] = "Product updated successfully!";
            //            return RedirectToAction("Index");
            //        }

            //        string error = response.Content.ReadAsStringAsync().Result;
            //        TempData["errorMessage"] = $"Failed to update. Status: {response.StatusCode}, Error: {error}";
            //    }
            //    catch (Exception ex)
            //    {
            //        TempData["errorMessage"] = $"Exception: {ex.Message}";
            //    }

            //    return View(model);
            //}



            //    return View(model);
            //}


            //edit nahi thatu and redirect pan nahi thatu
            //[HttpPost]
            //public IActionResult Edit(Product model)
            //{
            //    try
            //    {
            //        string data = JsonConvert.SerializeObject(model);
            //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //        // HttpResponseMessage response = _client.PutAsync($"Grocessary/UpdateProduct/{model.Id}", content).Result;
            //        HttpResponseMessage response = _client.PutAsync($"Grocessary/{model.Id}", content).Result;


            //        if (response.IsSuccessStatusCode)
            //        {
            //            TempData["successMessage"] = "Product updated successfully!";
            //            return RedirectToAction("Index");
            //        }

            //        string error = response.Content.ReadAsStringAsync().Result;
            //        TempData["errorMessage"] = $"Failed to update. Status: {response.StatusCode}, Error: {error}";
            //    }
            //    catch (Exception ex)
            //    {
            //        TempData["errorMessage"] = $"Exception: {ex.Message}";
            //    }

            //    return View(model);
            //}



            //edit pan nahi thatu nd redirect pan nahi thatu
            //[HttpPost]
            //public IActionResult Edit(Product model)
            //{
            //    try
            //    {
            //        string data = JsonConvert.SerializeObject(model);
            //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //        // Correct endpoint: PUT /api/Grocessary/{id}
            //        HttpResponseMessage response = _client.PutAsync($"Grocessary/{model.Id}", content).Result;

            //        if (response.IsSuccessStatusCode)
            //        {
            //            TempData["successMessage"] = "Product updated successfully!";
            //            return RedirectToAction("Index");
            //        }

            //        string error = response.Content.ReadAsStringAsync().Result;
            //        TempData["errorMessage"] = $"Failed to update. Status: {response.StatusCode}, Error: {error}";
            //    }
            //    catch (Exception ex)
            //    {
            //        TempData["errorMessage"] = $"Exception: {ex.Message}";
            //    }

            //    return View(model);
            //}






            // delte kare chhe pan deletion time a data show nahi kartu kai product chhe and also redirect  nahi kartu
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        Product product = null;

        //        //Product product = new Product();
        //        //HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Grocessary/GetProductById" + id).Result;
        //        HttpResponseMessage response = await _client.DeleteAsync($"Grocessary/DeleteProduct/{id}");


        //        if (response.IsSuccessStatusCode)
        //        {
        //            string data = response.Content?.ReadAsStringAsync().Result;
        //            product = JsonConvert.DeserializeObject<Product>(data);
        //        }
        //        return View(product);

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["errorMessage"] = ex.Message;
        //        return View();
        //    }
        //}

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product product = null;

                HttpResponseMessage response = await _client.GetAsync($"Grocessary/GetProductById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(data);
                }

                return View(product);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }




        // na redirect thay chhe na delete thay chhe

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Grocessary/DeleteProduct" + id).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}



        // it is in async format , work completely
        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    HttpResponseMessage response = await _client.DeleteAsync($"Grocessary/DeleteProduct/{id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    TempData["errorMessage"] = "Could not delete product.";
        //    return RedirectToAction("Delete", new { id });
        //}

        //delete and redirect kare chhe
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync($"Grocessary/DeleteProduct/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

           TempData["errorMessage"] = "Failed to delete product!"; 
            return View();
        }

   





    }
}

