using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarwashAPI.Models;

public partial class CarwashDbContext : DbContext
{
    public CarwashDbContext()
    {
    }

    public CarwashDbContext(DbContextOptions<CarwashDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Box> Boxes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<PriceChange> PriceChanges { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Spot> Spots { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=Carwash;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Unique_Identifier2");

            entity.Property(e => e.StatusId).HasDefaultValue(1);

            entity.HasOne(d => d.Client).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Кто записан");

            entity.HasOne(d => d.Employee).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Какому мойщику назначено");

            entity.HasOne(d => d.Spot).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Relationship4");

            entity.HasOne(d => d.Status).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Текущий статус");

            entity.HasMany(d => d.Services).WithMany(p => p.Appointments)
                .UsingEntity<Dictionary<string, object>>(
                    "AppointmentsService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Какие услуги выбраны_Service"),
                    l => l.HasOne<Appointment>().WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Какие услуги выбраны_Appointment"),
                    j =>
                    {
                        j.HasKey("AppointmentId", "ServiceId").HasName("Key1");
                        j.ToTable("Appointments_Services");
                        j.IndexerProperty<int>("AppointmentId").HasColumnName("appointmentId");
                        j.IndexerProperty<int>("ServiceId").HasColumnName("serviceId");
                    });
        });

        modelBuilder.Entity<Box>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Unique_Identifier3");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Unique_Identifier1");

            entity.Property(e => e.TelNumber).IsFixedLength();
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Unique_Identifier5");
        });

        modelBuilder.Entity<PriceChange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Unique_Identifier6");

            entity.Property(e => e.DateTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Service).WithMany(p => p.PriceChanges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Имела обновление цены");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Unique_Identifier4");

            entity.ToTable(tb => tb.HasTrigger("trg_AfterInsertUpdate_Services"));
        });

        modelBuilder.Entity<Spot>(entity =>
        {
            entity.HasOne(d => d.Box).WithMany(p => p.Spots)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Имеет позиции");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Unique_Identifier7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
