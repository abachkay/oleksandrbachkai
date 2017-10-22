using System.Web.Mvc;

namespace oleksandrbachkai.Controllers
{    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }        
        public ActionResult Default()
        {
            return RedirectToAction("Index");
        }        
    }
}