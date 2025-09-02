namespace CalculatorApi.Models
{
    public class CalculatorResponse
    {
        public double Result { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}