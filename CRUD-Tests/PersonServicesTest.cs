using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

using Xunit.Abstractions;

namespace CRUD_Tests
{
    public class PersonServicesTest
    {
        private readonly IPersonsServiece _personsServiece;
        private readonly ICountryService _countryService;
        private readonly ITestOutputHelper _testoutputHelper;
        public PersonServicesTest(ITestOutputHelper testOutputHelper)
        {
            _personsServiece = new PersonServices();
            _countryService = new CountriesServices(false);
            _testoutputHelper = testOutputHelper;
        }

        #region AddPerson

        // when we supply null value as PersonAddRequest, 
        // it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            // Argument 
            PersonRequestDTO? personRequest = null;

            // Assert 
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act 
                _personsServiece.AddPerson(personRequest);
            });


        }
        // when we supply null values as PersonName , it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            // Arrange 
            PersonRequestDTO personRequest = new PersonRequestDTO() { Name = null };

            // Assert 
            Assert.Throws<ArgumentException>(() =>
            {
                // Act 
                _personsServiece.AddPerson(personRequest);
            });
        }


        // when we supply proper person details , it should insert the person into ther persons list 

        [Fact]
        public void AddPerson_ProperDetails()
        {
            // Arrange 
            PersonRequestDTO personRequest = new PersonRequestDTO()
            {
                Name = "Hossam",
                Address = "Fayoum",
                CountryId = Guid.NewGuid(),
                DateOfBirth = Convert.ToDateTime("2001-10-25"),
                Email = "HossamHosny@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true


            };

            // Act 
            PersonResponseDTO ExcepectedPerson = _personsServiece.AddPerson(personRequest);
            List<PersonResponseDTO> ActualPersonList = _personsServiece.GetAllPersons();


            // Assert 
            Assert.True(ExcepectedPerson.Id != Guid.Empty);

            Assert.Contains(ExcepectedPerson, ActualPersonList);

        }




        #endregion


        #region GetPersonByPersonId


        [Fact]
        public void GetPersonById_NullPersonId()
        {

            // Arrange 
            Guid? PersonId = null;

            // Act 
            PersonResponseDTO result = _personsServiece.GetPersonById(PersonId);

            // Assert 
            Assert.Null(result);




        }

        [Fact]
        public void GetPersonById_ProperDetails()
        {
            // Arrange
            CountryRequestDTO country = new CountryRequestDTO() { CountryName = "Canada" };
            CountryResponseDTO ExcepctedCountry = _countryService.AddCountry(country);

            PersonRequestDTO personRequest = new PersonRequestDTO()
            {
                Name = "Hossam",
                Address = "Fayoum",
                CountryId = ExcepctedCountry.CountryId,
                DateOfBirth = Convert.ToDateTime("2001-10-25"),
                Email = "HossamHosny@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            // Act
            PersonResponseDTO ExcpectedPerson = _personsServiece.AddPerson(personRequest);

            PersonResponseDTO? ActualPerson = _personsServiece.GetPersonById(ExcpectedPerson.Id);

            // Assert 
            Assert.Equal(ExcpectedPerson, ActualPerson);




        }





        #endregion


        #region GetAllPersons

        // check empty list 
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            // Act 
            List<PersonResponseDTO> persons_from_get = _personsServiece.GetAllPersons();

            // Assert 
            Assert.Empty(persons_from_get);
        }

        [Fact]
        public void GetAllPersons_ProperDetails()
        {
            // Arrange 
            CountryRequestDTO country1 = new CountryRequestDTO() { CountryName = "Canada" };
            CountryRequestDTO country2 = new CountryRequestDTO() { CountryName = "USA" };

            CountryResponseDTO ExcpectedCountry1 = _countryService.AddCountry(country1);
            CountryResponseDTO ExcpectedCountry2 = _countryService.AddCountry(country2);


            PersonRequestDTO Person1 = new PersonRequestDTO()
            {
                Name = "Mohamed",
                Address = "Itsa",
                CountryId = ExcpectedCountry1.CountryId,
                DateOfBirth = Convert.ToDateTime("2000-10-05"),
                Email = "MohamedMohamed111@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true
            };
            PersonRequestDTO Person2 = new PersonRequestDTO()
            {
                Name = "Hamed",
                Address = "Itsa",
                CountryId = ExcpectedCountry1.CountryId,
                DateOfBirth = Convert.ToDateTime("2002-10-05"),
                Email = "HamedMohamed111@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true
            };
            PersonRequestDTO Person3 = new PersonRequestDTO()
            {
                Name = "Mahmoud",
                Address = "Itsa",
                CountryId = ExcpectedCountry1.CountryId,
                DateOfBirth = Convert.ToDateTime("2003-10-05"),
                Email = "MahmoudMohamed111@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            List<PersonRequestDTO> people = new List<PersonRequestDTO> { Person2, Person1, Person3 };
            List<PersonResponseDTO> Persons_response_list_from_add = new List<PersonResponseDTO>();

            foreach (PersonRequestDTO person in people)
            {
                PersonResponseDTO peroneResponse = _personsServiece.AddPerson(person);
                Persons_response_list_from_add.Add(peroneResponse);
            }
            // Excepcted values 

            _testoutputHelper.WriteLine("Excepcting... ");
            foreach (PersonResponseDTO person in Persons_response_list_from_add)
            {
                _testoutputHelper.WriteLine(person.ToString());
            }



            // Act 
            List<PersonResponseDTO> persons_list_from_get = _personsServiece.GetAllPersons();

            _testoutputHelper.WriteLine("Acual values");
            foreach (PersonResponseDTO person in persons_list_from_get)
            {
                _testoutputHelper.WriteLine(person.ToString());
            }



            // Assert 
            foreach (PersonResponseDTO person in Persons_response_list_from_add)
            {
                Assert.Contains(person, persons_list_from_get);
            }









        }
        #endregion


        #region GetFilteredPersons

        // when the search text is empty 
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            // Arrange 
            CountryRequestDTO country1 = new CountryRequestDTO() { CountryName = "Canada" };
            CountryRequestDTO country2 = new CountryRequestDTO() { CountryName = "USA" };

            CountryResponseDTO CountryResponse1 = _countryService.AddCountry(country1);
            CountryResponseDTO CountryResponse2 = _countryService.AddCountry(country2);


            PersonRequestDTO Person1 = new PersonRequestDTO()
            {
                Name = "Mohamed",
                Address = "Itsa",
                CountryId = CountryResponse1.CountryId,
                DateOfBirth = Convert.ToDateTime("2000-10-05"),
                Email = "MohamedMohamed111@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true
            };
            PersonRequestDTO Person2 = new PersonRequestDTO()
            {
                Name = "Hamed",
                Address = "Itsa",
                CountryId = CountryResponse1.CountryId,
                DateOfBirth = Convert.ToDateTime("2002-10-05"),
                Email = "HamedMohamed111@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true
            };
            PersonRequestDTO Person3 = new PersonRequestDTO()
            {
                Name = "Mahmoud",
                Address = "Itsa",
                CountryId = CountryResponse1.CountryId,
                DateOfBirth = Convert.ToDateTime("2003-10-05"),
                Email = "MahmoudMohamed111@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            List<PersonRequestDTO> people = new List<PersonRequestDTO> { Person2, Person1, Person3 };
            List<PersonResponseDTO> Persons_response_list_from_add = new List<PersonResponseDTO>();

            foreach (PersonRequestDTO person in people)
            {
                PersonResponseDTO peroneResponse = _personsServiece.AddPerson(person);
                Persons_response_list_from_add.Add(peroneResponse);
            }
            // Excepcted values 

            _testoutputHelper.WriteLine("Excepcting... ");
            foreach (PersonResponseDTO person in Persons_response_list_from_add)
            {
                _testoutputHelper.WriteLine(person.ToString());
            }



            // Act 
            List<PersonResponseDTO> persons_list_from_Search = _personsServiece.GetFilteredPersons(nameof(Person.Name), "");

            _testoutputHelper.WriteLine("Acual values");
            foreach (PersonResponseDTO person in persons_list_from_Search)
            {
                _testoutputHelper.WriteLine(person.ToString());
            }



            // Assert 
            foreach (PersonResponseDTO person in Persons_response_list_from_add)
            {
                Assert.Contains(person, persons_list_from_Search);
            }

        }

        // first we will add few persons and then we will search based on ther person name
        // with some string. It should return the matching persons
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            // Arrange 
            CountryRequestDTO country1 = new CountryRequestDTO() { CountryName = "Canada" };
            CountryRequestDTO country2 = new CountryRequestDTO() { CountryName = "USA" };

            CountryResponseDTO CountryResponse1 = _countryService.AddCountry(country1);
            CountryResponseDTO CountryResponse2 = _countryService.AddCountry(country2);


            PersonRequestDTO Person1 = new PersonRequestDTO()
            {
                Name = "Mohamed",
                Address = "Itsa",
                CountryId = CountryResponse1.CountryId,
                DateOfBirth = Convert.ToDateTime("2000-10-05"),
                Email = "MohamedMohamed111@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true
            };
            PersonRequestDTO Person2 = new PersonRequestDTO()
            {
                Name = "Hamed",
                Address = "Itsa",
                CountryId = CountryResponse1.CountryId,
                DateOfBirth = Convert.ToDateTime("2002-10-05"),
                Email = "HamedMohamed111@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true
            };
            PersonRequestDTO Person3 = new PersonRequestDTO()
            {
                Name = "Mahmoud",
                Address = "Itsa",
                CountryId = CountryResponse1.CountryId,
                DateOfBirth = Convert.ToDateTime("2003-10-05"),
                Email = "MahmoudMohamed111@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            List<PersonRequestDTO> people = new List<PersonRequestDTO> { Person2, Person1, Person3 };
            List<PersonResponseDTO> Persons_response_list_from_add = new List<PersonResponseDTO>();

            foreach (PersonRequestDTO person in people)
            {
                PersonResponseDTO peroneResponse = _personsServiece.AddPerson(person);
                Persons_response_list_from_add.Add(peroneResponse);
            }
            // Excepcted values 

            _testoutputHelper.WriteLine("Excepcting... ");
            foreach (PersonResponseDTO person in Persons_response_list_from_add)
            {
                _testoutputHelper.WriteLine(person.ToString());
            }



            // Act 
            List<PersonResponseDTO> persons_list_from_Search = _personsServiece.GetFilteredPersons(nameof(Person.Name), "ma");

            _testoutputHelper.WriteLine("Acual values");
            foreach (PersonResponseDTO person in persons_list_from_Search)
            {
                _testoutputHelper.WriteLine(person.ToString());
            }



            // Assert 
            foreach (PersonResponseDTO person in Persons_response_list_from_add)
            {

                if (person.Name is not null)
                {
                    if (person.Name.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person, persons_list_from_Search);
                    }
                }


            }

        }



        #endregion


        #region GetSortedPersons

        // when we sort base ond personName in Descending order , it should return persons list in descending on person Name
        [Fact]
        public void GetSortedPersons()
        {
            CountryRequestDTO country1 = new CountryRequestDTO()
            {
                CountryName = "Egypt"
            };
            CountryRequestDTO country2 = new CountryRequestDTO()
            {
                CountryName = "Canada"
            };


            CountryResponseDTO countryResponse1 = _countryService.AddCountry(country1);
            CountryResponseDTO countryResponse2 = _countryService.AddCountry(country2);


            PersonRequestDTO personRequest1 = new PersonRequestDTO()
            {
                Name = "Ahmed",
                Email = "Smith@example.com",
                Gender = GenderOptions.Male,
                Address = "Address of smith",
                CountryId = countryResponse1.CountryId,
                DateOfBirth = Convert.ToDateTime("2000-09-09"),
                ReceiveNewsLetters = true

            };
            PersonRequestDTO personRequest2 = new PersonRequestDTO()
            {
                Name = "Hossam",
                Email = "PersonRequest2@example.com",
                Gender = GenderOptions.Male,
                Address = "Address of smith",
                CountryId = countryResponse2.CountryId,
                DateOfBirth = Convert.ToDateTime("2000-09-09"),
                ReceiveNewsLetters = true

            };
            PersonRequestDTO personRequest3 = new PersonRequestDTO()
            {
                Name = "Osama",
                Email = "personRequest3@example.com",
                Gender = GenderOptions.Male,
                Address = "Address of smith",
                CountryId = countryResponse1.CountryId,
                DateOfBirth = Convert.ToDateTime("2000-09-09"),
                ReceiveNewsLetters = true

            };

            List<PersonRequestDTO> PersonRequests = new List<PersonRequestDTO>() { personRequest1, personRequest2, personRequest3 };

            List<PersonResponseDTO> PersonResponseList = new List<PersonResponseDTO>();


            foreach (PersonRequestDTO person in PersonRequests)
            {
                PersonResponseDTO personResponse = _personsServiece.AddPerson(person);
                PersonResponseList.Add(personResponse);
            }

            PersonResponseList = PersonResponseList.OrderByDescending(temp => temp.Name).ToList();

            // print the Expected person list 
            _testoutputHelper.WriteLine("Expected ....");
            foreach (PersonResponseDTO person in PersonResponseList)
            {
                _testoutputHelper.WriteLine(person.ToString());
            }

            // Act 
            List<PersonResponseDTO> GetAllPersons = _personsServiece.GetAllPersons();
            List<PersonResponseDTO> person_List_From_GetSortedPersons =
                _personsServiece.GetSortedPersons(GetAllPersons, nameof(Person.Name), SortOrderOptions.Descending);


            // print persons list from GetSortedPersons (The acual output)

            _testoutputHelper.WriteLine("Actual ....");
            foreach (PersonResponseDTO person in person_List_From_GetSortedPersons)
            {
                _testoutputHelper.WriteLine(person.ToString());
            }

            // Assert 
            for (int i = 0; i < PersonResponseList.Count; i++)
            {
                Assert.Equal(PersonResponseList[i], person_List_From_GetSortedPersons[i]);
            }



        }



        #endregion


        #region UpdatePerson

        // when we supply null as PersonUpdateRequest, it should throw ArgumentNullException
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            // Arrange 
            PersonUpdateRequest? personUpdate = null;


            // Assert 
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act 
                _personsServiece.UpdatePerson(personUpdate);


            });

        }



        // when we pass a person not valid it should throw ArrgumnetException
        [Fact]
        public void UpdatePerson_InvalidPerson()
        {
            // Arrange 
            PersonUpdateRequest personUpdated = new PersonUpdateRequest() { Id = Guid.NewGuid() };

            // Assert 
            Assert.Throws<ArgumentException>(() =>
            {
                // Act 
                _personsServiece.UpdatePerson(personUpdated);
            });

        }

        // when PersonName is Null 
        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            // Arrange
            CountryRequestDTO countryRequest = new CountryRequestDTO() { CountryName = "canada" };

            CountryResponseDTO countryResponse = _countryService.AddCountry(countryRequest);

            PersonRequestDTO PersonRequest = new PersonRequestDTO() { Name = "Hossam", CountryId = countryResponse.CountryId, Email = "Hossam@example.com" , Gender = GenderOptions.Male };

            PersonResponseDTO personResponse = _personsServiece.AddPerson(PersonRequest);

            PersonUpdateRequest PersonUpdated = personResponse.ToPersonUpdateRequest();

            PersonUpdated.Name = null;


            // Assert 
            Assert.Throws<ArgumentException>(() =>
            {
                // Act 
                _personsServiece.UpdatePerson(PersonUpdated);
            });

        }

        // when we pass a vaild details it should pass 
        [Fact]
        public void UpdatePerson_ProperDetails()
        {
            // Arrange
            CountryRequestDTO countryRequest = new CountryRequestDTO() { CountryName = "canada" };

            CountryResponseDTO countryResponse = _countryService.AddCountry(countryRequest);

            PersonRequestDTO PersonRequest = new PersonRequestDTO()
            {
                Name = "Hossam",
                CountryId = countryResponse.CountryId,
                Address = "HossamAddres",
                DateOfBirth = DateTime.Parse("2001-10-25"),
                Email = "Hossam@example.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true

            };

            PersonResponseDTO personResponse = _personsServiece.AddPerson(PersonRequest);

            PersonUpdateRequest personUpdate = personResponse.ToPersonUpdateRequest();

            personUpdate.Name = "HossamUpdated";

            // Act 
            PersonResponseDTO PersonResponse_From_Updated = _personsServiece.UpdatePerson(personUpdate);
            PersonResponseDTO? PersonResponse_from_Get = _personsServiece.GetPersonById(personUpdate.Id);

            // Assert 
            Assert.Equal(PersonResponse_From_Updated, PersonResponse_from_Get);




        }


        #endregion


        #region DeletePerson

        // if we suplly a vaild  person id , it should return true 
        [Fact]
        public void DeletePerson_ValidPerson()
        {
            CountryRequestDTO countryRequest = new CountryRequestDTO() { CountryName = "Canada" };

            CountryResponseDTO countryResponse = _countryService.AddCountry(countryRequest);

            PersonRequestDTO personRequest = new PersonRequestDTO()
            {
                Name = "Hossam",
                Address = "HossamAddress",
                CountryId = countryResponse.CountryId,
                DateOfBirth = DateTime.Parse("2001-10-25"),
                Email = "Hossam@test.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true
            };

            PersonResponseDTO personResponse = _personsServiece.AddPerson(personRequest);
            
            // Act 
            bool isDeleted = _personsServiece.DeletePerson(personResponse.Id);

            // Assert 
            Assert.True(isDeleted);


        }
        // when we supply invalid person id , it should return false
        [Fact]
        public void DeletePerson_InvalidPersonId()
        {
            // Act 
            bool isDeleted = _personsServiece.DeletePerson(Guid.NewGuid());

            // Assert 
            Assert.False(isDeleted);
        }
         
      




        #endregion
    }
}