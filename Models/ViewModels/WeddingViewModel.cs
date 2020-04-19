using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models.ViewModels
{
    public class WeddingVM
    {   
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Name can only contain letters")]
        public string Bride {get; set;}

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Name can only contain letters")]
        public string Groom {get; set;}

        [Required]
        [DateValidator]
        public DateTime Date {get; set;}

        [Required]
        public string Location {get; set;}
    }

    public class DateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if((DateTime) value < DateTime.Now)
            {
                return new ValidationResult("Date cannot be in the past!!!");
            }
            return ValidationResult.Success;
        }
    }
}