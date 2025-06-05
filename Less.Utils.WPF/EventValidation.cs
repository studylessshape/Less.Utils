using System.Globalization;
using System.Windows.Controls;

namespace Less.Utils.WPF
{
    /// <summary>
    /// Custom validation by using event
    /// </summary>
    public class EventValidation : ValidationRule
    {
        /// <summary>
        /// Call in <see cref="Validate(object, CultureInfo)"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public delegate ValidationResult ValidationEvent(object value, CultureInfo cultureInfo);
        /// <summary>
        /// Call in <see cref="Validate(object, CultureInfo)"/>
        /// </summary>
        public event ValidationEvent ValidateEvent;
        /// <summary>
        /// When result is <see langword="null"/>, the result will use default result
        /// </summary>
        public bool DefaultResult { get; set; } = true;
        /// <summary>
        /// When result is <see langword="null"/>, the error content will use default result
        /// </summary>
        public string DefaultErrorContent { get; set; } = "";

        /// <inheritdoc/>
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
