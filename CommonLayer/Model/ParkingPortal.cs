using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer.Model
{
    public class ParkingPortal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceiptNo { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string DriverName { get; set; }

        [Required]
        public string VehicleColor { get; set; }
        
        [Required]
        [RegularExpression(@"^([A-Z]{2}\s?(\d{2})?(-)?([A-Z]{1}|\d{1})?([A-Z]{1}|\d{1})?( )?(\d{4}))$")]
        public string VehicleNumber { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [RegularExpression(@"^[A-D]$")]
        public string Slot { get; set; }

        public string Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ParkingDate { get; set; }
    }
}
