namespace WebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        [Key]
        public int Booking_Id { get; set; }

        public int Hotel_Number { get; set; }

        public int Guest_Number { get; set; }

        public int Room_Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime Booking_DateFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime Booking_DateTo { get; set; }

        public virtual Room Room { get; set; }

        public virtual Guest Guest { get; set; }
    }
}
