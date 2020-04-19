using System;
using System.ComponentModel.DataAnnotations;


namespace WeddingPlanner.Models
{
    public class RSVP
    {
        [Key]
        public int RSVPId {get; set;}

        // [Required]
        // public int HostId {get; set;}

        // public User Host {get; set;}

        [Required]
        public int WeddingId {get; set;}

        [Required]
        public Wedding Wedding{get; set;}

        [Required]
        public int AttendeeId {get; set;}
        public User Attendee {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;

        public DateTime UpdatedAt {get; set;} = DateTime.Now;
    }
}