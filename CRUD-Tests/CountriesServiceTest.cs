
using Moq;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit.Sdk;



namespace CRUD_Tests
{
    public class CountriesServiceTest
    {

        private readonly ICountryService _countryService;
        public CountriesServiceTest()
        {

            _countryService = new CountriesServices(false);
            //  new CountriesServices();
        }

        #region AddCountry
        // Requirments 
        // 1 => when countryAddRequest is null , it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            // Arrange 
            CountryRequestDTO? requestDTO = null;


            // Assert 
            Assert.Throws<ArgumentNullException>(() =>
            {
                _countryService.AddCountry(requestDTO);
            });



        }
        // 2 => weh  the countryName is null , it should throu ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // Arragne 
            CountryRequestDTO requestDTO = new CountryRequestDTO() { CountryName = null };

            // Assert 
            Assert.Throws<ArgumentException>(() =>
            {
                // Act 
                _countryService.AddCountry(requestDTO);

            });




        }




        // 3 => when the countryName is duplicate , it should throuw ArgumentExceptioin

        [Fact]
        public void AddCountry_DuplicateName()
        {
            // Arrange 
            CountryRequestDTO requestDTO1 = new CountryRequestDTO() { CountryName = "Egypt" };
            CountryRequestDTO requestDTO2 = new CountryRequestDTO() { CountryName = "Egypt" };

            // Assert 
            Assert.Throws<ArgumentException>(() =>
            {
                _countryService.AddCountry(requestDTO1);
                _countryService.AddCountry(requestDTO2);
            });

        }

        // 4 => when you supply proper country name it should insert (add) the country to the existing list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            // Arrange
            CountryRequestDTO requestDTO = new CountryRequestDTO() { CountryName = "Japan" };
            // Act 
            CountryResponseDTO responseDTO = _countryService.AddCountry(requestDTO);
            List<CountryResponseDTO> CountriesFromGetAllCountries =
                _countryService.GetAllCountries();
            // Assert 
            Assert.True(responseDTO.CountryId != Guid.Empty);
            Assert.Contains(responseDTO,CountriesFromGetAllCountries);



        }


        #endregion

        #region GetAllCountries

        // empty list of Countries 
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            // Act 
            List<CountryResponseDTO> countries =
                 _countryService.GetAllCountries();

            // Assert 
            Assert.Empty(countries);


        }
        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            // Arrange 
            List<CountryRequestDTO> countryRequestDTOs = new List<CountryRequestDTO>()
            {
                new CountryRequestDTO(){CountryName="USA"},
                new CountryRequestDTO(){CountryName="UK"}
            };


            // Act 
            List<CountryResponseDTO> ExcepectedCountriesList = new List<CountryResponseDTO>();

            foreach (CountryRequestDTO countryRequestDTO in countryRequestDTOs)
            {
                ExcepectedCountriesList.Add(
                _countryService.AddCountry(countryRequestDTO));
            }

           List<CountryResponseDTO> AcualCountriesList =
                _countryService.GetAllCountries();


            foreach (CountryResponseDTO expectedCountry in ExcepectedCountriesList)
            {
                Assert.Contains(expectedCountry, AcualCountriesList);


            }





            
        }
        #endregion

        #region GetCountryById

        [Fact]
        public void GetCountryById_NullCountryId()
        {
            // Arrange 
            Guid? id = null;

            // Act 
            CountryResponseDTO? response = _countryService.GetCountryById(id);

            // Assert 
            Assert.Null(response);
        }

        [Fact]
        public void GetCountryById_ValidCountryId()
        {
            CountryRequestDTO request = new CountryRequestDTO() { CountryName= "China"};
            CountryResponseDTO ExcepectedCountry = _countryService.AddCountry(request);

            CountryResponseDTO? actualCountry = _countryService.GetCountryById(ExcepectedCountry.CountryId);
            Assert.Equal(ExcepectedCountry, actualCountry);
        
        
        }





        #endregion
    }
}
