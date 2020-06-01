using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Enter Valid Email Address")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is InValid")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^Owner|^Security$|^Police$|^Driver$")]
        public string UserType { get; set; }
    }
}
