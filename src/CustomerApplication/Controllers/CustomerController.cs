using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApplication.Controllers
{
    public class CustomerController : Controller
    {
        private PolarisAssignmentContext _context;
        public CustomerController(PolarisAssignmentContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Customer.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customer.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index", "CustomerAddress", new { CustomerID = customer.Id});
            }

            return View(customer);
        }
        public ActionResult Edit(int? id)
        {
            Customer customer = null;
            if (id != null)
            {
                customer = _context.Customer.Find(id);
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customer.Update(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Views/Customer/Edit.cshtml");
        }
        public ActionResult Address(int? id)
        {
            return RedirectToAction("AddressList", "CustomerAddress", new { CustomerID = id });
        }
        public ActionResult License(int? id)
        {
            return RedirectToAction("LicenseList", "License", new { CustomerID = id });
        }
        public ActionResult Detail(int? id)
        {
            List<CustomerAddressViewModel> customerAddressList = new List<CustomerAddressViewModel>();
            IEnumerable<License> licenseList;
            IEnumerable<CustomerAddress> addressList;
            ViewBag.Customer = _context.Customer.Find(id);
            /*Here ToList is used to avoid the error : openDataReader associated with this command */
            addressList = _context.CustomerAddress.Where(x => x.CustomerId == id).ToList();
            foreach(CustomerAddress customer in addressList)
            {
                CustomerAddressViewModel model = new CustomerAddressViewModel();
                model.AddressId = customer.AddressId;
                model.Street = customer.Street;
                model.City = customer.City;
                var stateInfo = from d in _context.StateMaster.Where(d => d.StateId == customer.StateorProvince).ToList()
                                select new { d.StateName };
                foreach(var info in stateInfo)
                {
                    model.StateorProvince = info.StateName;
                   // model.Country = info.CountryName;
                }
                var countryInfo = from b in _context.CountryMaster.Where(b => b.CountryId == customer.Country).ToList()
                                select new {b.CountryName };
                foreach (var info in countryInfo)
                {
                   // model.StateorProvince = info.CountryName;
                    model.Country = info.CountryName;
                }

                customerAddressList.Add(model);
            }
            ViewBag.CustomerAddress = customerAddressList;
            licenseList = _context.License.Where(x => x.CustomerId == id);
            ViewBag.License = licenseList;
            return View();
        }
    }
}
