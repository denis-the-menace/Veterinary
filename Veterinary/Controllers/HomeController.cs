using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Veterinary.Models;

namespace Veterinary.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
            //return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View("VetLogin");
        }

        [HttpPost]
        public IActionResult LoginPost(VetLoginVM vet)
        {
            var request = new RestRequest("auth/login", Method.Post);
            var jsonBody = JsonConvert.SerializeObject(vet).ToString();
            request.AddBody(jsonBody);
            var response = Client._client.Execute(request);
            var json = JsonConvert.DeserializeObject<dynamic>(response.Content);
            if (json["success"]=="false")
            {
                return RedirectToAction("Login");
            }
            string token = json["message"];
            var authentication = new JwtAuthenticator(token);
            Client._client.Authenticator = authentication;
            //httpOnly olduğu için js ile cookieye ulaşılamaz o yüzden güvenli.
            //xaccesstoken constant olduğu için static işlevi
            //görür bu yüzden tekrar bi instance yaratman gerekmez.
            //Response.Cookies.Append("test", token, new CookieOptions
            //{
            //    HttpOnly = true,
            //    SameSite = SameSiteMode.Strict
            //});
            return RedirectToAction("GetClients", "Vet");
            //return RedirectToAction("Index","Vet");
        }
		[HttpGet]
        public IActionResult Register()
		{
            return View("VetRegister");
		}
		[HttpPost]
        public IActionResult Register(VetRegisterVM vet)
        {
            var request = new RestRequest("auth/register", Method.Post);
            var jsonBody = JsonConvert.SerializeObject(vet).ToString();
            request.AddBody(jsonBody);
            var response = Client._client.Execute(request);
            var json = JsonConvert.DeserializeObject<dynamic>(response.Content);
            string token = json["message"];
            var authentication = new JwtAuthenticator(token);
            Client._client.Authenticator = authentication;
            return RedirectToAction("AddClient", "Vet");
        }

        public IActionResult LogOut()
        {
            Client._client.Authenticator = null;
            return RedirectToAction("Login");
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
