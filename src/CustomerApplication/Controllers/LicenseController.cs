using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CustomerApplication.Controllers
{
    public class LicenseController : Controller
    {
        private PolarisAssignmentContext _context;
        static String typeLic = "";
        public LicenseController(PolarisAssignmentContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? CustomerID)
        {
            ViewData["CustomerId"] = CustomerID;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(License license)
        {
            if (ModelState.IsValid)
            {
                _context.License.Add(license);
                _context.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
            ViewData["CustomerId"] = license.CustomerId;
            return View("~/Views/License/Index.cshtml");
        }
        public ActionResult LicenseList(int CustomerID)
        {
            ViewData["CustomerId"] = CustomerID;
            return View((_context.License.Where(x => x.CustomerId == CustomerID)).ToList());
        }
        public IActionResult AddNewLicense(int? id)
        {
            ViewData["CustomerId"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAnother(License license)
        {
            if (ModelState.IsValid)
            {
                _context.License.Add(license);
                _context.SaveChanges();
                return RedirectToAction("LicenseList", "License", new { CustomerID = license.CustomerId });
            }
            ViewData["CustomerId"] = license.CustomerId;
            return View("~/Views/License/AddNewLicense.cshtml");
        }
        public ActionResult Edit(int? id,String licenseType)
        {
            License license = null;
            if (id != null)
            {
                ViewData["TypeLicense"] = licenseType;
                typeLic = licenseType;
                license = _context.License.Find(id,licenseType);
            }
            return View(license);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(License license)
        {
            License toDelete;
            if (ModelState.IsValid)
            {
                toDelete = _context.License.Find(license.CustomerId,typeLic);
                _context.License.Attach(toDelete);
                _context.License.Remove(toDelete);
                _context.SaveChanges();
                _context.License.Add(license);
                _context.SaveChanges();
                return RedirectToAction("LicenseList", "License", new { CustomerID = license.CustomerId });
            }

            return View("~/Views/License/Edit.cshtml");
        }
    }
}
