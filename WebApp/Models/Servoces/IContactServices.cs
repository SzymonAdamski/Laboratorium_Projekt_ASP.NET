using WebApplication1.Models;
namespace WebApplication1.Models.Services;

public interface IContactServices
{
    void Add(ContactModel contact);
    void Update(ContactModel contact);
    void Delete(int id);
    List<ContactModel> GetAll();
    ContactModel GetById(int id);
}
