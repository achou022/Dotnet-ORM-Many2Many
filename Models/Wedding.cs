using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Wedding
    {  
        [Key]
        public int WeddingId {get; set;}

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

        [Required]
        public int CreatorId {get; set;}

        public User Creator {get; set;}

        public List<RSVP> RSVPs {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt{get; set;} = DateTime.Now;
    }

    public class DateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if((DateTime) value > DateTime.Now)
            {
                return new ValidationResult("Not time travelers!!!");
            }
            return ValidationResult.Success;
        }
    }
}