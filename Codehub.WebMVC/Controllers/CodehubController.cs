using Codehub.Models;
using Codehub.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Codehub.WebMVC.Controllers
{
    [Authorize]
    public class CodehubController : Controller
    {
        // GET: Codehub
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CodehubService(userId);
            var model = service.GetCodehub();

            return View(model);
        }
        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CodehubCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var service = CreateCodehubService();

            if (service.CreateCodehub(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateCodehubService();
            var model = svc.GetCodehubById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateCodehubService();
            var detail = service.GetCodehubById(id);
            var model =
                new CodehubEdit
                {
                    CodehubId = detail.CodehubId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CodehubEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CodehubId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCodehubService();

            if (service.UpdateCodehub(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCodehubService();
            var model = svc.GetCodehubById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCodehubService();

            service.DeleteCodehub(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private CodehubService CreateCodehubService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CodehubService(userId);
            return service;
        }
    }
}