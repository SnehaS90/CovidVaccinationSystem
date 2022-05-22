using CovidVaccinationSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace CovidVaccinationSystem.Data
{
    public class DBContext : DbContext 
    {
        //this class reponsible for physical connection to SQL Server DB and CURD operations
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<VaccinationRecord> VaccinationRecord { get; set; }

        // mapping entiites to Database (Code First Approach)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //map Patient Entity
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(x => x.PatientId);
                entity.ToTable("Patient");
                entity.Property(e => e.PatientId).HasColumnName("PatientId");
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired(); ;
                entity.Property(e => e.DOB).IsRequired();
                entity.Property(e => e.Gender).HasMaxLength(10).IsRequired();
                entity.Property(e => e.ContactNo).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(50).IsRequired(); 
                entity.Property(e => e.Nationality).HasMaxLength(20).IsRequired();
                entity.Property(e => e.PassportNo).HasMaxLength(20).IsRequired();
                entity.Property(e => e.EmiratesId).HasMaxLength(20).IsRequired();
            });

            //map VaccinationRecord Entity
            modelBuilder.Entity<VaccinationRecord>(entity =>
            {
                entity.HasKey(x => x.VaccinationId);
                entity.ToTable("VaccinationRecord");
                entity.Property(e => e.VaccinationId).HasMaxLength(50).HasColumnName("VaccinationId");
                entity.Property(e => e.VaccinationName).IsRequired();
                entity.Property(e => e.VaccinationCentre).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Dose).HasMaxLength(50).IsRequired(); ;
                entity.Property(e => e.VaccinationDate).IsRequired();
                entity.Property(e => e.PatientId).IsRequired();
            });

            //map relationship  patient enitity to VaccinationRecord 
            modelBuilder.Entity<VaccinationRecord>()
            .HasOne(p => p.PatientDtl)
            .WithMany(v => v.VaccinationRecords);

            base.OnModelCreating(modelBuilder);
        }
    }
}
