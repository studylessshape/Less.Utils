using System.Globalization;
using System.Windows.Controls;

namespace Less.Utils.WPF
{
    public class EventValidation : ValidationRule
    {
        public delegate ValidationResult ValidationEvent(object value, CultureInfo cultureInfo);
        public event ValidationEvent ValidateEvent;
        public bool DefaultResult { get; set; } = true;
        public string DefaultErrorContent { get; set; } = "";
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = ValidateEvent?.Invoke(value, cultureInfo);
            if (result == null)
            {
                return new ValidationResult(DefaultResult, DefaultErrorContent);
            }
            return result;
        }
    }
}
