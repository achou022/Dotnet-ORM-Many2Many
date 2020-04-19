using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId{get; set;}

        [Required]
        [Display(Name="First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Name can only contain letters")]
        public string FirstName{get; set;}

        [Required]
        [Display(Name="Last  Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Name can only contain letters")]
        public string LastName {get; set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password{get; set;}

        public List<Wedding> Weddings {get; set;}

        public List<RSVP> RSVPs {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;

        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [Required(ErrorMessage="Confirm Password is required!")]
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPW {get; set;}
    }
    public class LogUser
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string LogEmail{get; set;}
        [Required]
        [MinLength(8)]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string LogPassword{get; set;}
    }
}