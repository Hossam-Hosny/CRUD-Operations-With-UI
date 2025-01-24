

using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class PersonRequestDTO
    {
        [Required(ErrorMessage ="Person Name can't be blank")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Email can't be blanck")]
        [EmailAddress(ErrorMessage ="Email value should be a valid email")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }


        /// <summary>
        /// Converts the Current object of PersonRequestDTO into a  new object of Person type
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            return new Person()
            {
                Name = Name,
                Address = Address,
                CountryId = CountryId,
                DateOfBirth = DateOfBirth,
                Email = Email,
                Gender = Convert.ToString(Gender),
                ReceiveNewsLetters = ReceiveNewsLetters,
                Id = Guid.NewGuid()

            };
        }

    }
}
