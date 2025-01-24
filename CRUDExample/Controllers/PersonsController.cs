using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDExample.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IPersonsServiece _personServices;
        private readonly ICountryService _countryServices;

       

        public PersonsController(IPersonsServiece personServices  , ICountryService countryServices)
        {
            _personServices = personServices;
            _countryServices = countryServices;
        }





        [Route("/")]
        [Route("persons/index")]
        public IActionResult Index(string searchBy , string searchString , string sortBy = nameof(PersonResponseDTO.Name) , SortOrderOptions sortOrder = SortOrderOptions.Ascending)
        {

            ViewBag.SearchFields = new Dictionary<string,string>()
            {
                { nameof(PersonResponseDTO.Name) , "Person Name" },
                { nameof(PersonResponseDTO.Email) , "Email" },
                { nameof(PersonResponseDTO.DateOfBirth) , "Date of Birth" },
                { nameof(PersonResponseDTO.Gender) , "Person " },
                { nameof(PersonResponseDTO.CountryId) , "Country" },
              
                { nameof(PersonResponseDTO.Address) , "Address" },
                
            };

            var result = _personServices.GetFilteredPersons(searchBy , searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            var sortedPersons = _personServices.GetSortedPersons(result, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSrotOrder = sortOrder.ToString();

            return View(sortedPersons);
           
        }

        [Route("persons/create")]
        [HttpGet]
        public IActionResult Create()
        {

          var countries=  _countryServices.GetAllCountries();
            ViewBag.Countries = countries;
            return View();
        }
        [HttpPost]
        [Route("persons/create")]

        public IActionResult Create(PersonRequestDTO person)
        {
            if (!ModelState.IsValid)
            {
                var countries = _countryServices.GetAllCountries();
                ViewBag.Countries = countries;
                ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(error => error.ErrorMessage).ToList();
                return View();
            }
            try
            {
                var personAdded = _personServices.AddPerson(person);
                Console.WriteLine("Person added successfully: " + personAdded);
                return RedirectToAction("Index", "Persons");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: " + ex.Message);
                ModelState.AddModelError(string.Empty, "An error occurred.");
                ViewBag.Countries = _countryServices.GetAllCountries();
                return View(person);
            }

        }

    }
}
