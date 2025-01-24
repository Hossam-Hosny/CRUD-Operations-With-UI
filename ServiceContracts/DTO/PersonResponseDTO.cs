

using Entities;
using ServiceContracts.Enums;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents DTO class that is used as return type of most Methods of Person Service
    /// </summary>
    public class PersonResponseDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Country {  get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        public double? Age { get; set; }






        /// <summary>
        /// Compares the Current object data with the parameter object 
        /// </summary>
        /// <param name="obj">The PersonResponse DTO Object to compare</param>
        /// <returns>True or False , indicating whether all person details are match </returns>

        public override bool Equals(object? obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != typeof(PersonResponseDTO)) { return false; }
            PersonResponseDTO Person = (PersonResponseDTO)obj;
            return Person.Id == Id &&
                    Person.Name == Name &&
                    Person.Email == Email &&
                    Person.DateOfBirth == DateOfBirth &&
                    Person.Gender == Gender &&
                    Person.CountryId == CountryId &&
                    Person.Address == Address &&
                    Person.ReceiveNewsLetters == ReceiveNewsLetters &&
                    Person.Country == Country;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"PersonId {Id}  PersonName {Name} Email {Email}" +
                $"DateOfBirth {DateOfBirth}   Gender {Gender}  CountryId {CountryId} " +
                $"Country {Country}  Address {Address}  ReceiveNewsLetters  {ReceiveNewsLetters}";
        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                Id = Id,
                Address = Address,
                CountryId = CountryId,
                DateOfBirth = DateOfBirth,
                Email = Email,
                Gender=(GenderOptions) Enum.Parse(typeof(GenderOptions),Gender,true),
                Name = Name,
                ReceiveNewsLetters = ReceiveNewsLetters
            };
        }



    }
    public static class PersonExtensions
    {
        /// <summary>
        /// An extension method to convert an object of Person class into PersonResponse class
        /// </summary>
        /// <param name="person">Returns the converted PersonResponse object </param>
        public static PersonResponseDTO ToPersonResponse( this  Person person)
        {
            return new PersonResponseDTO()
            {
                Id = person.Id,
                Name = person.Name,
                Address = person.Address,
                CountryId = person.CountryId,
                
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                Email = person.Email,
                ReceiveNewsLetters = person.ReceiveNewsLetters,


                Age = (person.DateOfBirth is not null)? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) :null

                

            };
            
        }

        
    }
    
}
