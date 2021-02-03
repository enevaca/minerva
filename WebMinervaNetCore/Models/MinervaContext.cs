using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebMinervaNetCore.Models
{
    public partial class MinervaContext : DbContext
    {
        public MinervaContext()
        {
        }

        public MinervaContext(DbContextOptions<MinervaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<CompraDetalle> CompraDetalles { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1\\SQL2016;Database=Minerva;User ID=usrminerva;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("Compra");

                entity.HasIndex(e => e.Transaccion, "UQ__Compra__1A1E5EEFA2C3E26E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Transaccion)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("transaccion");

                entity.Property(e => e.UsuarioRegistro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuarioRegistro")
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_Proveedor");
            });

            modelBuilder.Entity<CompraDetalle>(entity =>
            {
                entity.ToTable("CompraDetalle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCompra).HasColumnName("idCompra");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("precioUnitario");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total");

                entity.Property(e => e.UsuarioRegistro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuarioRegistro")
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.CompraDetalles)
                    .HasForeignKey(d => d.IdCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompraDetalle_Compra");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.CompraDetalles)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompraDetalle_Producto");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleado");

                entity.HasIndex(e => e.CedulaIdentidad, "UQ__Empleado__9FE1EA247EF08D5D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cargo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("cargo");

                entity.Property(e => e.CedulaIdentidad)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cedulaIdentidad");

                entity.Property(e => e.Celuar).HasColumnName("celuar");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Materno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("materno");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.Paterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paterno");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UsuarioRegistro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuarioRegistro")
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.HasIndex(e => e.Codigo, "UQ__Producto__40F9A2063C8D3FC3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("precioVenta");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("saldo");

                entity.Property(e => e.UnidadMedida)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("unidadMedida");

                entity.Property(e => e.UsuarioRegistro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuarioRegistro")
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("Proveedor");

                entity.HasIndex(e => e.Nit, "UQ__Proveedo__DF97D0E4C80B3750")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nit).HasColumnName("nit");

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("razonSocial");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Representante)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("representante");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.UsuarioRegistro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuarioRegistro")
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Usuario1, "UQ__Usuario__9AFF8FC6B4E7B8A1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Usuario1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usuario");

                entity.Property(e => e.UsuarioRegistro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuarioRegistro")
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Empleado");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
