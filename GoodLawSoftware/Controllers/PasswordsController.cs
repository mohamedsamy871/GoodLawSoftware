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


        // GET: PasswordsController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PasswordsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PasswordItem passwordItem)
        {
            try
            {
                _passwordItem.Entity.Add(passwordItem);
                _passwordItem.Save();
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
            return View(_passwordItem.Entity.GetById(id));
        }

        // POST: PasswordsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PasswordItem passwordItem)
        {
            try
            {
                _passwordItem.Entity.Update(passwordItem);
                _passwordItem.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                _passwordItem.Entity.Delete(id);
                _passwordItem.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
