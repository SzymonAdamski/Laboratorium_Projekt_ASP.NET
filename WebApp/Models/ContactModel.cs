using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Musisz podać imię!")]
    [MaxLength(20, ErrorMessage = "Imię nie może być dłuższe niż 20 znaków!")]
    [MinLength(2, ErrorMessage = "Imię musi być dłuższe niż 1 znak")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Musisz podać nazwisko!")]
    [MaxLength(50, ErrorMessage = "Nazwisko nie może być dłuższe niż 50 znaków!")]
    [MinLength(2, ErrorMessage = "Nazwisko musi być dłuższe niż 1 znak")]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }
    
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }

    [Display(Name = "Telefon")]
    [RegularExpression("\\d{3} \\d{3} \\d{3}", ErrorMessage = "Wpisz numer według wzoru: xxx xxx xxx")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Kategorie")]
    public Category Category { get; set; }
}