namespace WebApp.Models.Servoces;

public interface IContactServices
{
    void Add(ContactModel model);
    void Update(ContactModel model);
    void Delete(ContactModel model);
    List<ContactModel> GetAll();
    ContactModel? GetById(int id);
}