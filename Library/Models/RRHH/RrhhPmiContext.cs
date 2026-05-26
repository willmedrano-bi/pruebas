using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.Models.RRHH;

public partial class RrhhPmiContext : DbContext
{
    public RrhhPmiContext()
    {
    }

    public RrhhPmiContext(DbContextOptions<RrhhPmiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.3.181;Database=rrhh;User Id=AdminDesarrrollo;Password=D3S@ROL10;Trusted_Connection=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__88B513947AC6FF50");

            entity.ToTable("Empleado", "FichaEmpleado", tb =>
                {
                    tb.HasTrigger("MovimientoSalarioInsert");
                    tb.HasTrigger("MovimientoSalarioUpdate");
                });

            entity.HasIndex(e => e.CodigoEmpleado, "UQ__Empleado__CDEF1DDF33D90A35").IsUnique();

            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.Altura).HasColumnName("altura");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.CelularInstitucional)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("celular_institucional");
            entity.Property(e => e.CelularPersonal)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("celular_personal");
            entity.Property(e => e.CodigoEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo_empleado");
            entity.Property(e => e.CodigoPais)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("codigo_pais");
            entity.Property(e => e.CorreoInstitucional)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("correo_institucional");
            entity.Property(e => e.CorreoPersonal)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("correo_personal");
            entity.Property(e => e.DireccionEmpleado)
                .IsUnicode(false)
                .HasColumnName("direccion_empleado");
            entity.Property(e => e.DireccionResponsable)
                .IsUnicode(false)
                .HasColumnName("direccion_responsable");
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_contratacion");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("datetime")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.FechaRenuncia)
                .HasColumnType("datetime")
                .HasColumnName("fecha_renuncia");
            entity.Property(e => e.FotoEmpleado)
                .IsUnicode(false)
                .HasColumnName("foto_empleado");
            entity.Property(e => e.IdCiudadNacimiento).HasColumnName("id_ciudad_nacimiento");
            entity.Property(e => e.IdDepartamentoNacimiento).HasColumnName("id_departamento_nacimiento");
            entity.Property(e => e.IdDetEstadoUsuario).HasColumnName("id_detEstadoUsuario");
            entity.Property(e => e.IdDiscapacidad).HasColumnName("id_discapacidad");
            entity.Property(e => e.IdEstadoUsuario).HasColumnName("id_estado_usuario");
            entity.Property(e => e.IdEstadocivil).HasColumnName("id_estadocivil");
            entity.Property(e => e.IdGenero).HasColumnName("id_genero");
            entity.Property(e => e.IdMarcacion).HasColumnName("id_marcacion");
            entity.Property(e => e.IdMunicipioNacimiento).HasColumnName("id_municipio_nacimiento");
            entity.Property(e => e.IdProfesion).HasColumnName("id_profesion");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdTipocontratacion).HasColumnName("id_tipocontratacion");
            entity.Property(e => e.IdTipocontrato).HasColumnName("id_tipocontrato");
            entity.Property(e => e.IdTiposangre).HasColumnName("id_tiposangre");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.NombreConyuge)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("nombre_conyuge");
            entity.Property(e => e.Nombres)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Peso).HasColumnName("peso");
            entity.Property(e => e.PrefijoAcademico)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("prefijo_academico");
            entity.Property(e => e.Responsable)
                .IsUnicode(false)
                .HasColumnName("responsable");
            entity.Property(e => e.Salario)
                .IsUnicode(false)
                .HasColumnName("salario");
            entity.Property(e => e.TelefonoConyuge)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("telefono_conyuge");
            entity.Property(e => e.TelefonoInstitucional)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("telefono_institucional");
            entity.Property(e => e.TelefonoPersonal)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("telefono_personal");
            entity.Property(e => e.TelefonoResponsable)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono_responsable");
            entity.Property(e => e.Titular).HasColumnName("titular");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
