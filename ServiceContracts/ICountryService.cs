using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business login for manipulating Country entity
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Adds a Country object to the list of Countries
        /// </summary>
        /// <param name="dto">Country object to add</param>
        /// <returns>Returns the country object after adding it (including newly generated country id)</returns>
        CountryResponseDTO AddCountry(CountryRequestDTO? dto);

        /// <summary>
        /// Returns All Countries 
        /// </summary>
        /// <returns>All Countries from the list as list of CountryResponseDTO</returns>
        List<CountryResponseDTO> GetAllCountries();

        /// <summary>
        /// Returns a country object based on the given id 
        /// </summary>
        /// <param name="Id"> Country Id (guid) to search</param>
        /// <returns>Matching country as countryResponse object </returns>
        CountryResponseDTO? GetCountryById(Guid? Id);
    }
}
