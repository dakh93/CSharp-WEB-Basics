using System;
using System.Collections.Generic;
using System.Text;
using DATANOTATION = System.ComponentModel.DataAnnotations;

namespace BankSystem.Services
{
    public static class Validation
    {
        public static List<DATANOTATION.ValidationResult> ValidationResults { get; set; }

        public static bool IsValid(object obj)
        {
            ValidationResults = new List<DATANOTATION.ValidationResult>();

            var validationContext = new DATANOTATION.ValidationContext(obj);

            var tryValidateObject = DATANOTATION.Validator.TryValidateObject(obj, validationContext, ValidationResults, true);

            return tryValidateObject;
        }
    }
}
