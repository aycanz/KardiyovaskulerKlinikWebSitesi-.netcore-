using BussinessLayer.Concrete;
using DataAccessesLayer.Concrete;
using DataAccessesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace KVH.Controllers
{
    [AllowAnonymous]

    public class LoginController : Controller
    {


        PatientManager pm = new PatientManager(new EfPatientRepository());


        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Patient model)
        {
            pm.TAdd(model);
            return RedirectToAction("SignIn", "Login");

        }



        public IActionResult SignIn()
        {
            return View();
        }



        [HttpPost]
        public async Task <IActionResult> SignIn(Patient p)
        {
            Context c = new Context();
            
                var datavalue = c.Patients.FirstOrDefault(x => x.TcNo == p.TcNo && x.Password == p.Password);

                if (datavalue != null)
                {
                var claims = new List<Claim> //asp.net core 48.video
                {
                    new Claim(ClaimTypes.Name, p.TcNo),
                                new Claim("PatientId", datavalue.PatientId.ToString()) // Hastanın ID'si


                };   

                var useridentity=new ClaimsIdentity(claims,"a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Message = "T.C. Kimlik Numarası veya Şifre Hatalı!";
                    return View();
                }
            }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }


    }
    }

/*Authentication="kimlik doğrulaması"
Authorization="kimlik yetkilendirme"
Claim=""kullanıcı hakkında bilgiler tutan yapılar diyebiliriz."
Kısaca Claim anlatmamız gerekirse Örneğin:Youtobe'a giriş yaptık ve Youtobe bize izleyici rolü tanımladı,
bu tanımlama ile beraber istediğmiz video'yu izleyebiliyoruz.Ama diyelim ki yaşımız 18'den küçük ve bazı korku gerilim 
videoları +18 sınır konulması gerekiyor,İşte burada yaş aralığını ölçebilmek için ilgili kullanıcıların 
yaş değerlerinin claim olarak atanması sağlanmalı ve claim bazlı bir yetkilendirme yapılmalıdır.*/


/*  Context c = new Context();
            
                var datavalue = c.Patients.FirstOrDefault(x => x.TcNo == p.TcNo && x.Password == p.Password);

                if (datavalue != null)
                {
                    HttpContext.Session.SetString("username", p.TcNo);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "T.C. Kimlik Numarası veya Şifre Hatalı!";
                    return View();
                }*/