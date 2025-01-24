using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPersonsServiece
    {
        /// <summary>
        /// Adds a new person into the list of persons 
        /// </summary>
        /// <param name="PersonRequest"> Person to add </param>
        /// <returns>Returns the same person details , along with newly generated Person Id </returns>
        PersonResponseDTO AddPerson(PersonRequestDTO? PersonRequest);

        /// <summary>
        /// Returns all persons 
        /// </summary>
        /// <returns>Returns a list of Objects of Persons </returns>
        List<PersonResponseDTO> GetAllPersons();

        /// <summary>
        /// Returns the Person object based on the given person id
        /// </summary>
        /// <param name="Id"> person id to search </param>
        /// <returns>Returns the Matching person object </returns>
        PersonResponseDTO? GetPersonById(Guid? Id);

        /// <summary>
        /// Returns all person objects that matches with the given search fild and search string 
        /// </summary>
        /// <param name="SearchBy">search field to search </param>
        /// <param name="SearchString">search string to search</param>
        /// <returns>Returns all matching persons bbased on the given search field and search string </returns>
      List<PersonResponseDTO>  GetFilteredPersons(string SearchBy, string? SearchString);


        /// <summary>
        /// Returns sorted list of Persons
        /// </summary>
        /// <param name="allPersons">Represents list of Persons to sort</param>
        /// <param name="sortBy">Name of the property (Key) , based on which the persons should be sorted </param>
        /// <param name="sortOrder">Ascending or Descending order</param>
        /// <returns>Returns the list of Persons after Sorting</returns>
        List<PersonResponseDTO> GetSortedPersons(List<PersonResponseDTO> allPersons, string sortBy, SortOrderOptions sortOrder);


        /// <summary>
        /// Updates the specified person details based on the given person id
        /// </summary>
        /// <param name="person">person details to update , including person id </param>
        /// <returns>Returns the Updated Person</returns>
        PersonResponseDTO UpdatePerson(PersonUpdateRequest? person);


        /// <summary>
        /// Deletes a Person based on the given person id 
        /// </summary>
        /// <param name="PersonId">PersonId to delete </param>
        /// <returns>True , if the deletion is successfull; otherwise false </returns>

       bool DeletePerson(Guid? PersonId);
    }
}
