using System.ComponentModel.DataAnnotations;
public enum Category
{
   [Display(Name = "Rodzina", Order = 1)]
   Family = 1,
   [Display(Name = "Znajomi", Order = 3)]
   Friend = 3,
   [Display(Name = "Kontakt zawodowy", Order = 2)]
   Job = 2,
}