

using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// This is a DTO class used as a return type for most of CountriesService methods
    /// </summary>
    public class CountryResponseDTO
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }



        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(CountryResponseDTO)) {  return false; }
            CountryResponseDTO country = (CountryResponseDTO)obj;
            return country.CountryId == CountryId&&
                country.CountryName==CountryName;
        }



    }

    public static class CountryExtensions
    {
        public static CountryResponseDTO ToCountryResponse(this Country country)
        {

            return new CountryResponseDTO { CountryId = country.Id, CountryName = country.Name };
        }

    }
}
