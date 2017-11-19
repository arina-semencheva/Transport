using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Transport.DataModel;
using Transport.Models;

namespace Transport.DAO.Person
{
    public class PersonDAO : IPersonDAO
    {
        private TransportDBEntities _edmx = new TransportDBEntities();

        public PersonDAO()
        {

        }

        public async Task<IEnumerable<PersonViewModel>> GetPersons()
        {
            var persons = await (from person in _edmx.Person
                                 select new PersonViewModel
                                 {
                                     PersonId = person.PersonId,
                                     Name = person.Name,
                                     Surname = person.Surname,
                                     BirthDate = person.BirthDate,
                                     ExperienceWork = person.ExperienceWork,
                                     PersonType = new PersonTypeViewModel
                                     {
                                         PersonTypeId = person.PersonTypeId,
                                         PersonTypeName = _edmx.PersonType.FirstOrDefault(x => x.PersonTypeId == person.PersonTypeId).Name
                                     },
                                     PersonTypeId = person.PersonTypeId
                                 })
                                 .ToListAsync();
            return persons;
        }

        public async Task CreatePerson(PersonViewModel model)
        {
            DataModel.Person person = new DataModel.Person
            {
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = model.BirthDate,
                ExperienceWork = model.ExperienceWork,
                PersonTypeId = model.PersonType.PersonTypeId,
                TransportId = model.TransportId
            };
            _edmx.Person.Add(person);
            await _edmx.SaveChangesAsync();
        }

        public async Task DeletePerson(int personId)
        {
            var personEntity = await _edmx.Person.FirstOrDefaultAsync(x => x.PersonId == personId);
            if (personEntity == null)
                throw new Exception("Ooouupess!");
            await _edmx.SaveChangesAsync();
        }

        public async Task EditPerson(PersonViewModel model)
        {
            var personEntity = await (from person in _edmx.Person
                                      where person.PersonId == model.PersonId
                                      select person)
                                      .FirstOrDefaultAsync();
            if (personEntity == null)
                throw new Exception("Oouupss!");
            personEntity.Name = model.Name;
            personEntity.Surname = model.Surname;
            personEntity.BirthDate = model.BirthDate;
            personEntity.PersonTypeId = model.PersonTypeId;
            personEntity.TransportId = model.TransportId;
            await _edmx.SaveChangesAsync();
        }

        public async Task<PersonViewModel> GetPersonById(int personId)
        {
            var personEntity = await (from person in _edmx.Person
                                      where person.PersonId == personId
                                      select new PersonViewModel
                                      {
                                          PersonId = person.PersonId,
                                          Name = person.Name,
                                          Surname = person.Surname,
                                          BirthDate = person.BirthDate,
                                          ExperienceWork = person.ExperienceWork,
                                          PersonType = new PersonTypeViewModel
                                          {
                                              PersonTypeId = person.PersonTypeId,
                                              PersonTypeName = _edmx.PersonType.FirstOrDefault(x => x.PersonTypeId == person.PersonTypeId).Name
                                          },
                                          PersonTypeId = person.PersonTypeId,
                                          TransportId = _edmx.Transport.FirstOrDefault(
                                              x => x.Person.FirstOrDefault(y => y.PersonId == personId
                                              ).PersonId == person.PersonId)
                                              .TransportId
                                      })
                                 .FirstOrDefaultAsync();
            return personEntity;
        }


    }
}