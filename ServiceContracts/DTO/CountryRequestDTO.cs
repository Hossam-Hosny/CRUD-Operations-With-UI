

using Entities;

namespace ServiceContracts.DTO
{

    /// <summary>
    /// this is DTO class for adding a new Country
    /// </summary>
    public class CountryRequestDTO
    {
        public string? CountryName { get; set; }


        public Country ToCountry()
        {
            return new Country() { Name = CountryName};
        }
    }
}
