namespace WebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RoomFacility
    {
        [Key]
        public int Room_Facility_Id { get; set; }

        public int Facility_Number { get; set; }

        public int Room_Number { get; set; }

        public int Hotel_Number { get; set; }

        public virtual Facility Facility { get; set; }

        public virtual Room Room { get; set; }
    }
}
