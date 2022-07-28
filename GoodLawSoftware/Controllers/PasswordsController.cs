using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GoodLawSoftware.Helpers;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.AspNetCore;

namespace GoodLawSoftware.Controllers
{
    public class PasswordsController : Controller
    {
        private readonly IUnitOfWork<LoginItem> _loginItem;
        private readonly ILogger<PasswordsController> _logger;
        private readonly IValidator<LoginItem> _validator;

        public PasswordsController(IUnitOfWork<LoginItem> loginItem,ILogger<PasswordsController> logger,IValidator<LoginItem> validator)
        {
            _loginItem = loginItem;
            _logger = logger;
            _validator = validator;
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
            ValidationResult result = _validator.Validate(loginItem);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState, null);
                return View();
            }
            _loginItem.Entity.Add(loginItem);
            _loginItem.Save();
            return RedirectToAction(nameof(Index));
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
