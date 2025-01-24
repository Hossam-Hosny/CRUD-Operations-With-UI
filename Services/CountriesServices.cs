using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesServices : ICountryService 
    {
        private readonly List<Country> _countries;
        public CountriesServices(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>()
                {

                new Country() { Id = Guid.Parse("91DACC61-EB91-4D83-BAB6-9B4C7140D7C2"), Name = "USA" },

                new Country() { Id = Guid.Parse("13A2F5BB-547F-454F-BBCC-D4CC5DA730B4"), Name = "Canada" },

                new Country() { Id = Guid.Parse("310CF50C-A71D-4C70-B6B8-DE058DC90DAA"), Name = "UK" },

                new Country() { Id = Guid.Parse("6F3C65B8-F52B-4B6B-9753-B91CC496F47E"), Name = "Egypt" },

                new Country() { Id = Guid.Parse("667E51CE-7B23-4E6C-8966-D30260A9C7E3"), Name = "Palastine" },
                });

               





               
               
               
              
               
            }
        }



        public CountryResponseDTO AddCountry(CountryRequestDTO? dto)
        {
            if (dto == null) { throw new ArgumentNullException(nameof(dto)); }
            if (dto.CountryName is null) { throw new ArgumentException(nameof(dto.CountryName)); }
            if (_countries.Where(temp => temp.Name == dto.CountryName).Count() > 0) { throw new ArgumentException("Given country exist"); }
            Country country = dto.ToCountry();
            country.Id = Guid.NewGuid();
            
            _countries.Add(country);

            return country.ToCountryResponse();

        }

        public List<CountryResponseDTO> GetAllCountries()
        {
           return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponseDTO? GetCountryById(Guid? Id)
        {
           if (Id is null) { return null; }


          Country? country =
                _countries.FirstOrDefault(temp => temp.Id == Id);

            if (country is null) return null;

            return country.ToCountryResponse();


        }
    }
}
