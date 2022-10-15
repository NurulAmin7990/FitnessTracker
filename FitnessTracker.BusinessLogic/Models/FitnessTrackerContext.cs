using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FitnessTracker.BusinessLogic.Models
{
    public partial class FitnessTrackerContext : DbContext
    {
        public FitnessTrackerContext()
        {
        }

        public FitnessTrackerContext(DbContextOptions<FitnessTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BodyPart> BodyParts { get; set; } = null!;
        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<Workout> Workouts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSqlLocalDb;Database=FitnessTracker;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BodyPart>(entity =>
            {
                entity.ToTable("BodyPart");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BodyPart1)
                    .HasMaxLength(255)
                    .HasColumnName("BodyPart");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.ToTable("Exercise");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ExerciseName).HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.UnitType).HasMaxLength(10);
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.ToTable("Workout");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BodyPartId).HasColumnName("BodyPartID");

                entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.Reps).HasMaxLength(255);

                entity.Property(e => e.TimeLength)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.Weights).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.BodyPart)
                    .WithMany(p => p.Workouts)
                    .HasForeignKey(d => d.BodyPartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workout_BodyPart");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.Workouts)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workout_Exercise");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Workouts)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Workout_Unit");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
