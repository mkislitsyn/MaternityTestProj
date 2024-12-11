using Maternity.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Maternity.Repository.DbContexts
{
    public class MaternityContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public MaternityContext(DbContextOptions<MaternityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Name)
                .WithOne(n => n.Patient)
                .HasForeignKey<Name>(n => n.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Name>()
                .Property(n => n.PatientId)
                .HasConversion<long>();
        }
    }
}
