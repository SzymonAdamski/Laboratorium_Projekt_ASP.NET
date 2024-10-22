using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Musisz podać imię!")]
    [MaxLength(length:20, ErrorMessage = "Imię nie może być dłuższe niż 20 znaków!")]
    [MinLength(length:2, ErrorMessage = "Imię musi być dłuższe niż 1 znak")]
    public string FirstName {get; set; }
    
    [Required(ErrorMessage = "Musisz podać Nazwisko!")]
    [MaxLength(length:50, ErrorMessage = "Nazwisko nie może być dłuższe niż 50 znaków!")]
    [MinLength(length:2, ErrorMessage = "Nazwisko musi być dłuższe niż 1 znak")]
    public string LasttName {get; set; }
    
    [EmailAddress]
    public string Email {get; set; }
    
    [DataType(DataType.Date)]
    public DateOnly BirthDate {get; set; }
    
    [RegularExpression("\\d\\d\\d \\d\\d\\d \\d\\d\\d", ErrorMessage = "wpisz numer według wzoru:xxx xxx xxx")]
    public string PhoneNumber {get; set; }
}