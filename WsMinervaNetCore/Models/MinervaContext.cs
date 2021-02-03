using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WsMinervaNetCore.Models
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

        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<CompraDetalle> CompraDetalle { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=127.0.0.1\\SQL2016;Database=Minerva;User ID=usrminerva;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasIndex(e => e.Transaccion)
                    .HasName("UQ__Compra__1A1E5EEFA2C3E26E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Transaccion)
                    .IsRequired()
                    .HasColumnName("transaccion")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioRegistro)
                    .HasColumnName("usuarioRegistro")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Compra)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_Proveedor");
            });

            modelBuilder.Entity<CompraDetalle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCompra).HasColumnName("idCompra");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnName("precioUnitario")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UsuarioRegistro)
                    .HasColumnName("usuarioRegistro")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.CompraDetalle)
                    .HasForeignKey(d => d.IdCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompraDetalle_Compra");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.CompraDetalle)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompraDetalle_Producto");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasIndex(e => e.CedulaIdentidad)
                    .HasName("UQ__Empleado__9FE1EA247EF08D5D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cargo)
                    .HasColumnName("cargo")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CedulaIdentidad)
                    .IsRequired()
                    .HasColumnName("cedulaIdentidad")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Celuar).HasColumnName("celuar");

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Materno)
                    .HasColumnName("materno")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("nombres")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Paterno)
                    .HasColumnName("paterno")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UsuarioRegistro)
                    .HasColumnName("usuarioRegistro")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasIndex(e => e.Codigo)
                    .HasName("UQ__Producto__40F9A2063C8D3FC3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnName("precioVenta")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Saldo)
                    .HasColumnName("saldo")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UnidadMedida)
                    .IsRequired()
                    .HasColumnName("unidadMedida")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioRegistro)
                    .HasColumnName("usuarioRegistro")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasIndex(e => e.Nit)
                    .HasName("UQ__Proveedo__DF97D0E4C80B3750")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nit).HasColumnName("nit");

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasColumnName("razonSocial")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Representante)
                    .HasColumnName("representante")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioRegistro)
                    .HasColumnName("usuarioRegistro")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Usuario1)
                    .HasName("UQ__Usuario__9AFF8FC6B4E7B8A1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnName("clave")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.RegistroActivo)
                    .HasColumnName("registroActivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Usuario1)
                    .IsRequired()
                    .HasColumnName("usuario")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioRegistro)
                    .HasColumnName("usuarioRegistro")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Empleado");
            });
        }
    }
}
