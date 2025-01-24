using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO
{/// <summary>
/// Represents the DTO class that contains the person details to update
/// </summary>
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage ="Person Id is Required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Person Name can't be blank")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email can't be blanck")]
        [EmailAddress(ErrorMessage = "Email value should be a valid email")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }


        /// <summary>
        /// Converts the Current object of PersonRequestDTO into a  new object of Person type
        /// </summary>
        /// <returns>Returns the person object </returns>
        public Person ToPerson()
        {
            return new Person()
            {
                Id = Id,
                Name = Name,
                Address = Address,
                CountryId = CountryId,
                DateOfBirth = DateOfBirth,
                Email = Email,
                Gender = Convert.ToString(Gender),
                ReceiveNewsLetters = ReceiveNewsLetters,
               

            };
        }



    }
}
