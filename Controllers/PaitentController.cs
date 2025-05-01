using BussinessLayer.Concrete;
using DataAccessesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace KVH.Controllers
{
    public class PaitentController : Controller
    {
        PatientManager pm = new PatientManager(new EfPatientRepository());

        // Patient kaydetme işlemi
        [HttpPost]
        public IActionResult SavePatient(Patient model)
        {
            // TcNo'nun boş veya null olup olmadığını kontrol et
            if (string.IsNullOrEmpty(model.TcNo))
            {
                ModelState.AddModelError("TcNo", "TcNo is required.");
                return View(model);  // Hata varsa view'a geri dön
            }

            if (ModelState.IsValid)
            {
                // Model geçerliyse veritabanına ekle
                pm.TAdd(model);
                return RedirectToAction("Index");  // Başka bir sayfaya yönlendirebilirsiniz
            }

            // Model geçerli değilse, aynı view'a geri dön
            return View(model);
        }


    }
}
