using Codehub.Data;
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
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Codehub
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CodehubService(userId);
            var model = service.GetCodehub();

            return View(model);
        }
       
        public ActionResult CssIndex3(int Id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CodehubService(userId);
            var model = service.GetAllCssByCodeHubId(Id);

            return View(model);
        }
        //GET
        public ActionResult ConnectCss()
        {
            //all of this code is used for the drop down list
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CodehubService(userId);
            List<CodeHub1> Hubs = service.GetCodeHubs().ToList();
            ViewBag.CodehubId = Hubs.Select(h => new SelectListItem()
            {
                Value = h.CodehubId.ToString(),
                Text = h.Title
                
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConnectCss(OnlyCodehubId model)
        {
            //Grabbing your id from the url and saving it to a variable
            var id = RouteData.Values["id"] + Request.Url.Query;
            var cssId = Convert.ToInt32(id);

            if (!ModelState.IsValid) return View(model);

            var service = CreateCodehubService();

            if (service.ConnectCodeWithAHub(cssId, model))
            {
                TempData["SaveRessult"] = "Css Code was added to Codehub";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Css Code could not be added to Codehub");

            return View(model);

        }
        public ActionResult ConnectBootstrap()
        {
            //all of this code is used for the drop down list
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CodehubService(userId);
            List<CodeHub1> Hubs = service.GetCodeHubs().ToList();
            ViewBag.CodehubId = Hubs.Select(h => new SelectListItem()
            {
                Value = h.CodehubId.ToString(),
                Text = h.Title
            });
        
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConnectBootstrap(OnlyCodehubId model)
        {
            var id = RouteData.Values["id"] + Request.Url.Query;
            var bootstrapId = Convert.ToInt32(id);

            if (!ModelState.IsValid) return View(model);
            var service = CreateCodehubService();
            if (service.ConnectCodeWithAHub(bootstrapId, model))
            {
                TempData["SaveRessult"] = "Bootstrap Code was added to Codehub";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bootstrap code could not be added to Codehub");

            return View(model);

        }
        public ActionResult Create()
        {
            ViewBag.CssId = new SelectList(db.CssCodes, "CssId", "CssTitle");
            ViewBag.BootstrapId = new SelectList(db.BootstrapCodes, "BootstrapId", "BootstrapTitle");
            ViewBag.CssCode = db.CssCodes;
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
            var userId = Guid.Parse(User.Identity.GetUserId());//line 61-63 are just for showing all bootstrap need to change it to show a certain bootstrap
            var bootstrapsvc = new BootstrapService(userId);
            model.bootstrapListItems = bootstrapsvc.GetBootstrap();
            ViewBag.CssByCodehubId = svc.GetAllCssByCodeHubId(id);

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
                    Description = detail.Description,

                };
            return View(model);
        }
      
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, CodehubEdit model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    if (model.CodehubId != id)
        //    {
        //        ModelState.AddModelError("", "Id Mismatch");
        //        return View(model);
        //    }

        //    var service = CreateCodehubService();
        //    //either css or boostrap id = coded
        //    service.ConnectCodeWithAHub(model.CodeId, model.CodehubId);
            

        //    if (service.UpdateCodehub(model))
        //    {
        //        TempData["SaveResult"] = "Your Hub was updated.";
        //        return RedirectToAction("Index");
        //    }

        //    ModelState.AddModelError("", "Your Hub could not be updated.");
        //    return View(model);
        //}
        //public ActionResult Add(int codeId, CodeHubId model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    if (model.CodehubId != id)
        //    {
        //        ModelState.AddModelError("", "Id Mismatch");
        //        return View(model);
        //    }

        //    var service = CreateCodehubService();

        //    service.ConnectCodeWithAHub(codeId, model);
        //}
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