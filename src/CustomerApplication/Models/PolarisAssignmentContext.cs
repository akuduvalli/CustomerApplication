using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerApplication.Models
{
    public partial class PolarisAssignmentContext : DbContext
    {
        public virtual DbSet<CountryMaster> CountryMaster { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<License> License { get; set; }
        public virtual DbSet<StateMaster> StateMaster { get; set; }

        public PolarisAssignmentContext(DbContextOptions<PolarisAssignmentContext> options)
            : base(options)
        { }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=njo-lpt-akuduva;Database=PolarisAssignment;Trusted_Connection=True;");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryMaster>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__CountryM__10D160BF1273C1CD");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasColumnType("varchar(255)");

                entity.Property(e => e.FirstName).HasColumnType("varchar(255)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Phone).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__Customer__091C2A1B0BC6C43E");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("address_countryId_FK");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_CustomerId");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.LicenseType })
                    .HasName("pk_CustomerId_LicenseType");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LicenseType).HasColumnType("varchar(255)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.License)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("license_customerId_FK");
            });

            modelBuilder.Entity<StateMaster>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__StateMas__C3BA3B5A164452B1");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.StateMaster)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("fk_CountryId_statemaster");
            });
        }
    }
}