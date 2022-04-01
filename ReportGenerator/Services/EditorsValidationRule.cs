using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace ReportGenerator.Services
{
    public class EditorsValidationRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public EditorsValidationRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int val = 0;

            try
            {
                if (((string)value).Length > 0)
                    val = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Некорректные символы в {e.Message}");
            }

            if ((val < Min) || (val > Max))
            {
                return new ValidationResult(false,
                    $"Введите значение в диапазоне: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
