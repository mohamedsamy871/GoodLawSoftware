using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodLawSoftware.Controllers
{
    public class PasswordsController : Controller
    {
        private readonly IUnitOfWork<PasswordItem> _passwordItem;

        public PasswordsController(IUnitOfWork<PasswordItem> passwordItem)
        {
            _passwordItem = passwordItem;
        }
        // GET: PasswordsController
        public ActionResult Index()
        {
            return View(_passwordItem.Entity.GetAll().ToList());
        }

        // GET: PasswordsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PasswordsController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PasswordsController/Create
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

        // GET: PasswordsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PasswordsController/Edit/5
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

        // GET: PasswordsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PasswordsController/Delete/5
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
