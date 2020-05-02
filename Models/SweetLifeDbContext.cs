using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SweetLife.Models
{
    public partial class SweetLifeDbContext : DbContext
    {
        public SweetLifeDbContext()
        {
        }

        public SweetLifeDbContext(DbContextOptions<SweetLifeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Factory> Factory { get; set; }
        public virtual DbSet<FactoryUnit> FactoryUnit { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<IngredientStorage> IngredientStorage { get; set; }
        public virtual DbSet<ManufacturingOrder> ManufacturingOrder { get; set; }
        public virtual DbSet<ManufacturingOrderItem> ManufacturingOrderItem { get; set; }
        public virtual DbSet<ManufacturingOrderStatus> ManufacturingOrderStatus { get; set; }
        public virtual DbSet<MeasurementUnit> MeasurementUnit { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Sweet> Sweet { get; set; }
        public virtual DbSet<SweetIngredient> SweetIngredient { get; set; }
        public virtual DbSet<SweetStorage> SweetStorage { get; set; }
        public virtual DbSet<UnitWorker> UnitWorker { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=sweet_life_v3;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Factory>(entity =>
            {
                entity.ToTable("factory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<FactoryUnit>(entity =>
            {
                entity.ToTable("factory_unit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ControllerId).HasColumnName("controller_id");

                entity.Property(e => e.FactoryId).HasColumnName("factory_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FactoryUnit)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factory_unit_category");

                entity.HasOne(d => d.Controller)
                    .WithMany(p => p.FactoryUnit)
                    .HasForeignKey(d => d.ControllerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factory_unit_user");

                entity.HasOne(d => d.Factory)
                    .WithMany(p => p.FactoryUnit)
                    .HasForeignKey(d => d.FactoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factory_unit_factory");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredient");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.MeasurementUnitId).HasColumnName("measurement_unit_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.Ingredient)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ingredient_measurement_unit");
            });

            modelBuilder.Entity<IngredientStorage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ingredient_storage");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.FactoryId).HasColumnName("factory_id");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.HasOne(d => d.Factory)
                    .WithMany()
                    .HasForeignKey(d => d.FactoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ingredient_storage_factory");

                entity.HasOne(d => d.Ingredient)
                    .WithMany()
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ingredient_storage_ingredient");
            });

            modelBuilder.Entity<ManufacturingOrder>(entity =>
            {
                entity.ToTable("manufacturing_order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.FactoryUnitId).HasColumnName("factory_unit_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.FactoryUnit)
                    .WithMany(p => p.ManufacturingOrder)
                    .HasForeignKey(d => d.FactoryUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_manufacturing_order_factory_unit");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ManufacturingOrder)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_manufacturing_order_manufacturnig_order_status");
            });

            modelBuilder.Entity<ManufacturingOrderItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("manufacturing_order_item");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.ManufacturingOrderId).HasColumnName("manufacturing_order_id");

                entity.Property(e => e.SweetId).HasColumnName("sweet_id");

                entity.HasOne(d => d.ManufacturingOrder)
                    .WithMany()
                    .HasForeignKey(d => d.ManufacturingOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_manufacturing_order_item_manufacturing_order");

                entity.HasOne(d => d.Sweet)
                    .WithMany()
                    .HasForeignKey(d => d.SweetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_manufacturing_order_item_sweet");
            });

            modelBuilder.Entity<ManufacturingOrderStatus>(entity =>
            {
                entity.ToTable("manufacturing_order_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MeasurementUnit>(entity =>
            {
                entity.ToTable("measurement_unit");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.Entity<Sweet>(entity =>
            {
                entity.ToTable("sweet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Sweet)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category_id");
            });

            modelBuilder.Entity<SweetIngredient>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sweet_ingredient");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.SweetId).HasColumnName("sweet_id");

                entity.HasOne(d => d.Ingredient)
                    .WithMany()
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sweet_ingredient_ingredient");

                entity.HasOne(d => d.Sweet)
                    .WithMany()
                    .HasForeignKey(d => d.SweetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sweet_ingredient_sweet");
            });

            modelBuilder.Entity<SweetStorage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sweet_storage");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.FactoryId).HasColumnName("factory_id");

                entity.Property(e => e.SweetId).HasColumnName("sweet_id");

                entity.HasOne(d => d.Factory)
                    .WithMany()
                    .HasForeignKey(d => d.FactoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sweet_storage_factory");

                entity.HasOne(d => d.Sweet)
                    .WithMany()
                    .HasForeignKey(d => d.SweetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sweet_storage_sweet");
            });

            modelBuilder.Entity<UnitWorker>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("unit_worker");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.HasOne(d => d.Unit)
                    .WithMany()
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_unit_worker_factory_unit");

                entity.HasOne(d => d.Worker)
                    .WithMany()
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_unit_worker_user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(300);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
