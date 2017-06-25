using System.Linq;
using CustomerApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CustomerApplication.Controllers
{
    public class SearchController : Controller
    {
        private PolarisAssignmentContext _context;
    
        public SearchController(PolarisAssignmentContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            PopulateCountryDropDownList();
            return View();
        }
        [HttpGet]
        public IActionResult SearchCustomer(CustomerViewModel customerSearch)
        {
            CustomerViewModel customer = customerSearch;

            /*SqlParameter custId = new SqlParameter("@Id", customer.Id);
             // Create parameter with Direction as Output (and correct name and type)
             SqlParameter outputIdParam = new SqlParameter("@CustomerId",System.Data.SqlDbType.Int);
             outputIdParam.Direction = System.Data.ParameterDirection.Output;

             var result = _context.Database.ExecuteSqlCommand("GetCustomerResults @Id, @CustomerId out", custId , outputIdParam);
             int custo = (int)outputIdParam.Value;
             return View(result.ToList());*/
            if (ModelState.IsValid)
            {
                SqlConnection connection = new SqlConnection("Server=njo-lpt-akuduva;Database=PolarisAssignment;Trusted_Connection=True;");
                SqlParameter custId = new SqlParameter("@Id", customer.Id);
                SqlParameter custFirstName = new SqlParameter("@FName", customer.FirstName);
                SqlParameter custLastName = new SqlParameter("@LName", customer.LastName);
                SqlParameter custEmail = new SqlParameter("@Email", customer.Email);
                SqlParameter custPhone = new SqlParameter("@Phone", customer.Phone);
                SqlParameter custStreet = new SqlParameter("@Street", customer.Street);
                SqlParameter custCity = new SqlParameter("@City", customer.City);
                SqlParameter custState = new SqlParameter("@State", customer.StateorProvince);
                SqlParameter custCountry = new SqlParameter("@Country", customer.Country);
                SqlParameter custLicType = new SqlParameter("@LicType", customer.LicenseType);
                SqlParameter custLicNum = new SqlParameter("@LicNum", customer.Value);
                SqlCommand command = new SqlCommand("GetCustomerResults", connection);
                command.Parameters.Add(custId);
                command.Parameters.Add(custFirstName);
                command.Parameters.Add(custLastName);
                command.Parameters.Add(custEmail);
                command.Parameters.Add(custPhone);
                command.Parameters.Add(custStreet);
                command.Parameters.Add(custCity);
                command.Parameters.Add(custState);
                command.Parameters.Add(custCountry);
                command.Parameters.Add(custLicType);
                command.Parameters.Add(custLicNum);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                IDataReader reader = command.ExecuteReader();
                Customer customer1;
                List<Customer> customerList = new List<Customer>();

                while (reader.Read())
                {

                    int cId = int.Parse(reader["ID"].ToString());
                    string cFname = reader["FirstName"].ToString();
                    string cLname = reader["LastName"].ToString();
                    string cEmail = reader["Email"].ToString();
                    string cPhone = reader["Phone"].ToString();
                    customer1 = new Customer(cId, cLname, cFname, cEmail, cPhone);
                    customerList.Add(customer1);
                }
                return View(customerList);
            }
            
            /* if (ModelState.IsValid)
              {
                  HashSet<int> customerID = new HashSet<int>();
                  var results = from a in _context.Customer
                                join b in _context.CustomerAddress on a.Id equals b.CustomerId
                                join c in _context.License on a.Id equals c.CustomerId
                                select new { a.Id, a.LastName, a.FirstName, a.Phone, a.Email, b.Street, b.City, b.StateorProvince, b.Country, c.LicenseType, c.Value };
                  if (customer.Id > 0)
                  {
                      results = results.Where(a => a.Id == customer.Id);
                  }
                  if (customer.LastName != null && customer.LastName.ToString() != "")
                  {
                      results = results.Where(a => a.LastName == customer.LastName);
                  }
                  if (customer.FirstName != null && customer.FirstName.ToString() != "")
                  {
                      results = results.Where(a => a.FirstName == customer.FirstName);
                  }
                  if (customer.Email != null && customer.Email.ToString() != "")
                  {
                      results = results.Where(a => a.Email == customer.Email);
                  }
                  if (customer.Phone != null && customer.Phone.ToString() != "")
                  {
                      results = results.Where(a => a.Phone == customer.Phone);
                  }
                  if (customer.Street != null && customer.Street.ToString() != "")
                  {
                      results = results.Where(b => b.Street == customer.Street);
                  }
                  if (customer.City != null && customer.City.ToString() != "")
                  {
                      results = results.Where(b => b.City == customer.City);
                  }
                  if (customer.StateorProvince > 0)
                  {
                      results = results.Where(b => b.StateorProvince == customer.StateorProvince);
                  }
                  if (customer.Country > 0)
                  {
                      results = results.Where(b => b.Country == customer.Country);
                  }
                  if (customer.LicenseType != null && customer.LicenseType.ToString() != "")
                  {
                      results = results.Where(c => c.LicenseType == customer.LicenseType);
                  }
                  if (customer.Value != null && customer.Value.ToString() != "")
                  {
                      results = results.Where(c => c.Value == customer.Value);
                  }

                  foreach (var x in results)
                  {
                      customerID.Add(x.Id);
                  }
                  var customerResults = from e in _context.Customer
                                        where customerID.Contains(e.Id)
                                        select new Customer(e.Id, e.LastName, e.FirstName, e.Email, e.Phone);

                  return View(customerResults.ToList());
              }*/
            PopulateCountryDropDownList();
            return View("~/Views/Search/Index.cshtml");
        }
        private void PopulateCountryDropDownList(object selectedCountry = null)
        {
            var CountryQuery = from d in _context.CountryMaster
                               orderby d.CountryName
                               select new { d.CountryId, d.CountryName };
            ViewBag.CountryList = CountryQuery;
        }
    }
}
