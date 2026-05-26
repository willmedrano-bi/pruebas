using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.Models.Unificada;

public partial class UnificadaSpkeyContext : DbContext
{
    public UnificadaSpkeyContext()
    {
    }

    public UnificadaSpkeyContext(DbContextOptions<UnificadaSpkeyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LogsApiExterna> LogsApiExterna { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.3.181;Database=unificada_spkey;User Id=AdminDesarrrollo;Password=D3S@ROL10;Trusted_Connection=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogsApiExterna>(entity =>
        {
            entity.HasKey(e => e.IdLogApiExterna).HasName("PK__Logs_Api__A06F75F8A2373A8B");

            entity.ToTable("Logs_Api_Externa", "auditoria");

            entity.Property(e => e.IdLogApiExterna).HasColumnName("id_log_api_externa");
            entity.Property(e => e.Exito).HasColumnName("exito");
            entity.Property(e => e.FechaConsulta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_consulta");
            entity.Property(e => e.OrigenIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("origen_ip");
            entity.Property(e => e.PayloadRespuesta).HasColumnName("payload_respuesta");
            entity.Property(e => e.PayloadSolicitud).HasColumnName("payload_solicitud");
            entity.Property(e => e.SistemaOrigen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sistema_origen");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
