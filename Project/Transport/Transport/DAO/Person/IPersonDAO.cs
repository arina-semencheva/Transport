using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Transport.Models;

namespace Transport.DAO.Person
{
    public interface IPersonDAO
    {
        Task<IEnumerable<PersonViewModel>> GetPersons();
        Task EditPerson(PersonViewModel model);
        Task DeletePerson(int personId);
        Task CreatePerson(PersonViewModel model);
        Task<PersonViewModel> GetPersonById(int personId);
    }
}