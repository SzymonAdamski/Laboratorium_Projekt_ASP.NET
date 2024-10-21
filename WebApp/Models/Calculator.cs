namespace WebApp.Models
{
    public enum Operators
    {
        Add,
        Sub,
        Div,
        Mul
    }

    public class Calculator
    {
        public Operators? Operator { get; set; }
        public double? a { get; set; }
        public double? b { get; set; }

        public string op
        {
            get
            {
                return Operator switch
                {
                    Operators.Add => "+",
                    Operators.Sub => "-",
                    Operators.Div => "/",
                    Operators.Mul => "*",
                    _ => ""
                };
            }
        }

        public bool IsValid()
        {
            return Operator != null && a != null && b != null && (Operator != Operators.Div || b != 0);
        }

        public double Calculate()
        {
            return Operator switch
            {
                Operators.Add => (a ?? 0) + (b ?? 0),
                Operators.Sub => (a ?? 0) - (b ?? 0),
                Operators.Div => b != 0 ? (a ?? 0) / (b ?? 0) : throw new DivideByZeroException("Nie można dzielić przez zero!"),
                Operators.Mul => (a ?? 0) * (b ?? 0),
                _ => double.NaN
            };
        }
    }
}