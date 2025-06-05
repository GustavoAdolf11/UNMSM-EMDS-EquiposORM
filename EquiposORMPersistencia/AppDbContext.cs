using EquiposORM.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EquiposORM.Persistencia;

public class AppDbContext : DbContext
{
    public DbSet<Equipo> Equipos { get; set; }
    public DbSet<Tecnico> Tecnicos { get; set; }
    public DbSet<Topico> Topicos { get; set; }
    public DbSet<OTM> OTMs { get; set; }
    public DbSet<OTMTopico> OTMTopicos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=equipos.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OTMTopico>()
            .HasKey(ot => new { ot.OTMId, ot.TopicoId });
        modelBuilder.Entity<OTMTopico>()
            .HasOne(ot => ot.OTM)
            .WithMany(o => o.OTMTopicos)
            .HasForeignKey(ot => ot.OTMId);
        modelBuilder.Entity<OTMTopico>()
            .HasOne(ot => ot.Topico)
            .WithMany()
            .HasForeignKey(ot => ot.TopicoId);
    }
}