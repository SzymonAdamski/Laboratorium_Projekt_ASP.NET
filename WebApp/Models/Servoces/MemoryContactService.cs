using System.Collections.Generic;
using System.Linq;
using WebApp.Models;
using WebApplication1.Models;
namespace WebApplication1.Models.Services
{
    public class MemoryContactService : IContactServices
    {
        private static Dictionary<int, ContactModel> _contacts = new()
        {
            {
                1, new ContactModel()
                {
                    Id = 1,
                    FirstName = "Adam",
                    LastName = "Placek", 
                    Email = "adam@wsei.edu.pl",
                    Category = CategoryModel.Family,
                    BirthDate = new DateOnly(2002, 9, 10),
                    PhoneNumber = "999 999 999"
                }
            },
            {
                2, new ContactModel()
                {
                    Id = 2,
                    FirstName = "Waga",
                    LastName = "Placek", 
                    Category = CategoryModel.Family,
                    Email = "Waga@wsei.edu.pl",
                    BirthDate = new DateOnly(2001, 9, 10),
                    PhoneNumber = "998 998 999"
                }
            },
        };

        public void Add(ContactModel contact)
        {
            if (!_contacts.ContainsKey(contact.Id))
            {
                _contacts[contact.Id] = contact;
            }
            else
            {
                throw new ArgumentException("Kontakt o podanym ID już istnieje.");
            }
        }

        public void Update(ContactModel contact)
        {
            if (_contacts.ContainsKey(contact.Id))
            {
                _contacts[contact.Id] = contact;
            }
            else
            {
                throw new KeyNotFoundException("Kontakt o podanym ID nie istnieje.");
            }
        }

        public void Delete(int id)
        {
            if (_contacts.ContainsKey(id))
            {
                _contacts.Remove(id);
            }
            else
            {
                throw new KeyNotFoundException("Kontakt o podanym ID nie istnieje.");
            }
        }

        public List<ContactModel> GetAll()
        {
            return _contacts.Values.ToList();
        }

        public ContactModel GetById(int id)
        {
            if (_contacts.TryGetValue(id, out var contact))
            {
                return contact;
            }
            else
            {
                throw new KeyNotFoundException("Kontakt o podanym ID nie istnieje.");
            }
        }

        public List<OrganizationEntity> GetOrganization()
        {
            throw new NotImplementedException();
        }
    }

}
