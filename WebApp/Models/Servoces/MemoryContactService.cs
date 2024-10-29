namespace WebApp.Models.Servoces;

public class MemoryContactService: IContactServices
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
                Category = Category.Family,
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
                Category = Category.Family,
                Email = "Waga@wsei.edu.pl",
                BirthDate = new DateOnly(2001, 9, 10),
                PhoneNumber = "998 998 999"
            }
        },
    };
    public void Add(ContactModel model)
    {
        throw new NotImplementedException();
    }

    public void Update(ContactModel model)
    {
        throw new NotImplementedException();
    }

    public void Delete(ContactModel model)
    {
        throw new NotImplementedException();
    }

    public List<ContactModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public ContactModel? GetById(int id)
    {
        throw new NotImplementedException();
    }
}