namespace WebApp.Models
{
    public class Birth
    {
        public string Name { get; set; }
        public string Surname { get; set; } // Nowa właściwość
        public DateTime? BirthDate { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Surname) &&
                   BirthDate.HasValue &&
                   BirthDate.Value < DateTime.Now;
        }

        public int CalculateAge()
        {
            if (!BirthDate.HasValue)
                throw new InvalidOperationException("Birth date is not set.");

            var today = DateTime.Today;
            var age = today.Year - BirthDate.Value.Year;
            if (BirthDate.Value.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}