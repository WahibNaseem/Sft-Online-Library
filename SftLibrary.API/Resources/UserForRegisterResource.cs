using System;
using System.ComponentModel.DataAnnotations;


namespace SftLibrary.API.Models
{
    public class UserForRegisterResource
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [StringLength(maximumLength: 8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 to 8 characters")]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime Created { get; set; }

        public UserForRegisterResource()
        {
            this.Created = DateTime.Now;
        }
    }
}
