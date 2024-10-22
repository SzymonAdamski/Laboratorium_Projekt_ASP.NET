using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models
{
    public class Book
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę podać tytuł!")]
        [StringLength(100, ErrorMessage = "Tytuł nie może przekraczać 100 znaków!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Proszę podać autora!")]
        [StringLength(50, ErrorMessage = "Autor nie może przekraczać 50 znaków!")]
        public string Producent { get; set; }

        [Required(ErrorMessage = "Proszę podać Cene!")]
        [Range(1, int.MaxValue, ErrorMessage = "Cena musi być dodatnia!")]
        public int Cena { get; set; }

        [Required(ErrorMessage = "Proszę podać numer ISBN!")]
        [RegularExpression(@"\d{3}-\d{1,5}-\d{1,7}-\d{1,7}-\d{1}", ErrorMessage = "Podaj poprawny numer ISBN!")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Proszę podać rok wydania!")]
        [Range(1450, 2100, ErrorMessage = "Rok wydania musi być w zakresie od 1450 do 2100!")]
        public int YearPublished { get; set; }

        [Required(ErrorMessage = "Proszę podać wydawnictwo!")]
        [StringLength(50, ErrorMessage = "Nazwa wydawnictwa nie może przekraczać 50 znaków!")]
        public string Opis { get; set; }
    }
}