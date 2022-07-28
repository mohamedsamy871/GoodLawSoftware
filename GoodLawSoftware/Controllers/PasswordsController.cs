using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GoodLawSoftware.Helpers;
namespace GoodLawSoftware.Controllers
{
    public class PasswordsController : Controller
    {
        private readonly IUnitOfWork<LoginItem> _loginItem;
        private readonly ILogger<PasswordsController> _logger;

        public PasswordsController(IUnitOfWork<LoginItem> loginItem,ILogger<PasswordsController> logger)
        {
            _loginItem = loginItem;
            _logger = logger;
        }
        // GET: PasswordsController
        public ActionResult Index()
        {
            var loginItem = _loginItem.Entity.GetAll().ToList();
            foreach(var item in loginItem)
            {
                item.Password = EncryptionManager.Decrypt(item.Password);
            }
            return View(loginItem);
        }

        // GET: PasswordsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PasswordsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginItem loginItem)
        {
            var encryptedPassword = EncryptionManager.Encrypt(loginItem.Password);
            loginItem.Password = encryptedPassword;
            try
            {
                _loginItem.Entity.Add(loginItem);
                _loginItem.Save();
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
            var loginItem = _loginItem.Entity.GetById(id);
            loginItem.Password = EncryptionManager.Decrypt(loginItem.Password);
            return View(loginItem);
        }

        // POST: PasswordsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoginItem loginItem)
        {
            var encryptedPassword = EncryptionManager.Encrypt(loginItem.Password);
            loginItem.Password = encryptedPassword;
            try
            {
                _loginItem.Entity.Update(loginItem);
                _loginItem.Save();
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
                _loginItem.Entity.Delete(id);
                _loginItem.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
