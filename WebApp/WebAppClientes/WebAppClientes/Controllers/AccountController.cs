using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebAppClientes.Models;

namespace WebAppClientes.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7294/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var credentials = new
                    {
                        Login = model.Username,
                        Senha = model.Password
                    };

                    var jsonCredentials = JsonConvert.SerializeObject(credentials);

                    var content = new StringContent(jsonCredentials, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("api/Usuario/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var token = await response.Content.ReadAsStringAsync();

                        HttpContext.Session.SetString("AuthObject", token);

                        return RedirectToAction("ListarClientes", "Cliente");
                    }
                    else
                    {
                        
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }
    }
}
