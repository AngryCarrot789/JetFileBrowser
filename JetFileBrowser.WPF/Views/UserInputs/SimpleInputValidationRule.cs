using System.Globalization;
using System.Windows.Controls;
using JetFileBrowser.Views.Dialogs.UserInputs;

namespace JetFileBrowser.WPF.Views.UserInputs {
    public class SimpleInputValidationRule : ValidationRule {
        public InputValidator Validator { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            if (this.Validator != null && this.Validator.IsInvalidFunc(value?.ToString(), out string msg)) {
                return new ValidationResult(false, msg ?? "Invalid input");
            }
            else {
                return ValidationResult.ValidResult;
            }
        }
    }
}