using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_01_10_2026.Controllers
{
    public class OrdenCompraViewsController : Controller
    {
        // GET: OrdenCompra
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrdenCompra/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrdenCompra/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdenCompra/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdenCompra/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrdenCompra/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdenCompra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrdenCompra/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
