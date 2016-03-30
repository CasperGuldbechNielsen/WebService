namespace WebService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ViewContext : DbContext
    {
        public ViewContext()
            : base("name=ViewContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<SingleRoom> SingleRooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SingleRoom>()
                .Property(e => e.Hotel_Name)
                .IsUnicode(false);

            modelBuilder.Entity<SingleRoom>()
                .Property(e => e.Room_Type)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
