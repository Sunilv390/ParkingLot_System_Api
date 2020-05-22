using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer.Model
{
    public class ParkingPortal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string OwnerName { get; set; }

        [Required]
        public string VehicleType { get; set; }
        
        [Required]
        [RegularExpression(@"^([A-Z]{2}\s?(\d{2})?(-)?([A-Z]{1}|\d{1})?([A-Z]{1}|\d{1})?( )?(\d{4}))$")]
        public string VehicleNumber { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-D]$")]
        public string Slot { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime InTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OutTime { get; set; }
        
        [Required]
        public int Charges { get; set; }
    }
}
