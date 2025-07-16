using Microsoft.EntityFrameworkCore;

namespace PersonelUygulaması.Models

{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite(@"Data Source=C:\Users\ahmet\source\repos\PersonelUygulaması\PersonelUygulaması\bin\Debug\personel.db");
           

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<LeaveTypes> LeaveTypes { get; set; }
        public DbSet<LeaveRequests> LeaveRequests { get; set; }
        public DbSet<EquipmentItems> EquipmentItems { get; set; }
        public DbSet<EquipmentRequests> EquipmentRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentRequests>()
                .HasOne(e => e.Onaylayan)
                .WithMany()
                .HasForeignKey(e => e.OnaylayanId)
                .OnDelete(DeleteBehavior.Restrict); // yanlışlıkla kullanıcı silinince talep silinmesin
        }



    }
}
