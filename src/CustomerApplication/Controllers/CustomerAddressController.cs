using CustomerApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApplication.Controllers
{
    public class CustomerAddressController : Controller
    {
        private PolarisAssignmentContext _context;
        public CustomerAddressController(PolarisAssignmentContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? CustomerID)
        {
            ViewData["CustomerId"] = CustomerID;
            PopulateCountryDropDownList();
           // PopulateStateDropDownList();
            return View();
        }
        public IActionResult AddNewAddress(int? id)
        {
            ViewData["CustomerId"] = id;
            PopulateCountryDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerAddress customerAddress)
        {
            if (ModelState.IsValid)
            {
                _context.CustomerAddress.Add(customerAddress);
                _context.SaveChanges();
                return RedirectToAction("Index", "License", new { CustomerID = customerAddress.CustomerId });
            }
            ViewData["CustomerId"] = customerAddress.CustomerId;
            PopulateCountryDropDownList();
            return View("~/Views/CustomerAddress/Index.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAnother(CustomerAddress customerAddress)
        {
            if (ModelState.IsValid)
            {
                _context.CustomerAddress.Add(customerAddress);
                _context.SaveChanges();
                return RedirectToAction("AddressList", "CustomerAddress", new { CustomerID = customerAddress.CustomerId });
            }
            ViewData["CustomerId"] = customerAddress.CustomerId;
            PopulateCountryDropDownList();
            return View("~/Views/CustomerAddress/AddNewAddress.cshtml");
        }
        public ActionResult AddressList(int CustomerID)
        {
               ViewData["CustomerId"] = CustomerID;
               /* return View((_context.CustomerAddress.Where(x => x.CustomerId == CustomerID)).ToList());*/
            List<CustomerAddressViewModel> customerAddressList = new List<CustomerAddressViewModel>();
            IEnumerable<CustomerAddress> addressList;
            /*Here ToList is used to avoid the error : openDataReader associated with this command */
            addressList = _context.CustomerAddress.Where(x => x.CustomerId == CustomerID).ToList();
            foreach (CustomerAddress customer in addressList)
            {
                CustomerAddressViewModel model = new CustomerAddressViewModel();
                model.AddressId = customer.AddressId;
                model.Street = customer.Street;
                model.City = customer.City;
                var stateInfo = from d in _context.StateMaster.Where(d => d.StateId == customer.StateorProvince).ToList()
                                select new { d.StateName };
                foreach (var info in stateInfo)
                {
                    model.StateorProvince = info.StateName;
                    // model.Country = info.CountryName;
                }
                var countryInfo = from b in _context.CountryMaster.Where(b => b.CountryId == customer.Country).ToList()
                                  select new { b.CountryName };
                foreach (var info in countryInfo)
                {
                    // model.StateorProvince = info.CountryName;
                    model.Country = info.CountryName;
                }

                customerAddressList.Add(model);
            }
            ViewBag.CustomerAddress = customerAddressList;
            return View();
        }
        public ActionResult Edit(int? id)
        {
            CustomerAddress customerAddress = null;
            if (id != null)
            {
                customerAddress = _context.CustomerAddress.Find(id);
            }
            PopulateCountryDropDownList();
            PopulateStateDropDownList(customerAddress.Country);
            return View(customerAddress);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CustomerAddress customerAddress)
        {
            if (ModelState.IsValid)
            {
                _context.CustomerAddress.Update(customerAddress);
                _context.SaveChanges();
                return RedirectToAction("AddressList","CustomerAddress", new { CustomerID = customerAddress.CustomerId });
            }
            PopulateCountryDropDownList();
            PopulateStateDropDownList(customerAddress.Country);
            return View("~/Views/CustomerAddress/Edit.cshtml");
        }
        private void PopulateCountryDropDownList(object selectedCountry = null)
        {
            var CountryQuery = from d in _context.CountryMaster
                                   orderby d.CountryName
                                   select new { d.CountryId, d.CountryName };
            ViewBag.CountryList = CountryQuery;
        }
        private void PopulateStateDropDownList(int country)
        {
            if (country == 2 || country == 3)
            {
                var StateQuery = from d in _context.StateMaster.Where(c => c.CountryId == country)
                                 orderby d.StateName
                                 select new { d.StateId, d.StateName };
                ViewBag.StateList = StateQuery;
            }
            else
            {
                var StateQuery = from d in _context.StateMaster.Where(c => c.StateId == 82)
                                 orderby d.StateName
                                 select new { d.StateId, d.StateName };
                ViewBag.StateList = StateQuery;
            }
        }
        public JsonResult PopulateState(int country)
        {
            var states = _context.StateMaster.Where(c => c.CountryId == country);
            return Json(states);
        }
    }
}
