﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer.Model
{
    public class UserDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string  UserName { get; set; }

        [Required(ErrorMessage ="Enter Valid Email Address")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Minimum 6 characters allowed")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^Owner|^Security$|^Police$|^Driver$")]
        public string UserType { get; set; }
    }
}
