using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Domain.Data;
using Domain.Model;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (CarsDbContext context = new CarsDbContext()) {
                List<Car> cars = context.Cars.ToList();
                return View(cars.ToList());
            }
        }
    }
}