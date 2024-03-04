using System;
using System.Collections.Generic;
using DatabaseLayer.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DatabaseLayer.Context;

public partial class GidroMorpfContext : DbContext
{

    private ILogger _logger { get; set; }
    public GidroMorpfContext(ILoggerProvider loggerProvider)
    {
        _logger = loggerProvider.CreateLogger("GidroChemDbLogger");
    }

    public GidroMorpfContext(DbContextOptions<GidroMorpfContext> options, ILoggerProvider loggerProvider)
        : base(options)
    {
        _logger = loggerProvider.CreateLogger("GidroChemDbLogger");
    }

    public virtual DbSet<GmfCategory> GmfCategories { get; set; }

    public virtual DbSet<GmfClass3> GmfClass3s { get; set; }

    public virtual DbSet<GmfClass5> GmfClass5s { get; set; }

    public virtual DbSet<GmfEstamination> GmfEstaminations { get; set; }

    public virtual DbSet<GmfIndicator> GmfIndicators { get; set; }

    public virtual DbSet<GmfProtocol> GmfProtocols { get; set; }

    public virtual DbSet<GmfPunct> GmfPuncts { get; set; }

    public virtual DbSet<GmfTotal> GmfTotals { get; set; }

    public virtual DbSet<GmfZona> GmfZonas { get; set; }

    public virtual DbSet<GmfRegion> GmfRegions {get;set;}

    public virtual DbSet<GmfRiver> GmfRivers {get;set;}

    public virtual DbSet<GmfBass> GmfBasss {get;set;} 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    { 
        optionsBuilder.UseNpgsql("User Id=dbgidrohim;Password=*****;Server=*****;Port=5432;Database=******;");
        Action<string> a = str =>
        {
            _logger.LogTrace(str);
            //Console.WriteLine(str);

        };
        optionsBuilder.LogTo(a);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GmfCategory>(entity =>
        {
            entity.HasKey(e => e.Idcat).HasName("gmf_category_pkey");

            entity.ToTable("gmf_category", "gxav");

            entity.HasIndex(e => e.Idcat, "gmf_category_idcat_key").IsUnique();

            entity.Property(e => e.Idcat)
                .ValueGeneratedNever()
                .HasColumnName("idcat");
            entity.Property(e => e.Namecat)
                .HasMaxLength(100)
                .IsFixedLength()    
                .HasColumnName("namecat");

            entity.Property(e => e.Nzona).HasColumnName("nzona");

            entity.HasOne(d => d.NzonaNavigation).WithMany(p => p.GmfCategories)
                .HasForeignKey(d => d.Nzona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKZONA");
        });

        modelBuilder.Entity<GmfClass3>(entity =>
        {
            entity.HasKey(e => e.Class3).HasName("gmf_class3_pkey");

            entity.ToTable("gmf_class3", "gxav");

            entity.Property(e => e.Class3)
                .ValueGeneratedNever()
                .HasColumnName("class3");
            entity.Property(e => e.Color3).HasColumnName("color3");
            entity.Property(e => e.Np3).HasColumnName("np3");
            entity.Property(e => e.Sost3)
                .HasMaxLength(100)
                .HasColumnName("sost3");
            entity.Property(e => e.Vp3).HasColumnName("vp3");
        });

        modelBuilder.Entity<GmfClass5>(entity =>
        {
            entity.HasKey(e => e.Class5).HasName("gmf_class5_pkey");

            entity.ToTable("gmf_class5", "gxav");

            entity.Property(e => e.Class5)
                .ValueGeneratedNever()
                .HasColumnName("class5");
            entity.Property(e => e.Color5).HasColumnName("color5");
            entity.Property(e => e.Np5).HasColumnName("np5");
            entity.Property(e => e.Sost5)
                .HasMaxLength(100)
                .HasColumnName("sost5");
            entity.Property(e => e.Vp5).HasColumnName("vp5");
        });

        modelBuilder.Entity<GmfEstamination>(entity =>
        {
            entity.HasKey(e => e.Np).HasName("gmf_estamination_pkey");

            entity.ToTable("gmf_estamination", "gxav");

            entity.Property(e => e.Np)
                .ValueGeneratedNever()
                .HasColumnName("np");
            entity.Property(e => e.Estam)
                .HasMaxLength(150)
                .HasColumnName("estam");
        });

        modelBuilder.Entity<GmfIndicator>(entity =>
        {
            entity.HasKey(e => e.Idindicator).HasName("gmf_indicator_pkey");

            entity.ToTable("gmf_indicator", "gxav");

            entity.HasIndex(e => e.Idindicator, "u_iden").IsUnique();

            entity.Property(e => e.Idindicator)
                .HasMaxLength(5)
                .HasColumnName("idindicator");
            entity.Property(e => e.Cat).HasColumnName("cat");
            entity.Property(e => e.GroupIndic).HasColumnName("group_indic");
            entity.Property(e => e.Iddop).HasColumnName("iddop");
            entity.Property(e => e.Idmain).HasColumnName("idmain");
            entity.Property(e => e.Mark).HasColumnName("mark");
            entity.Property(e => e.Namindicator)
                .HasMaxLength(150)
                .HasColumnName("namindicator");

            entity.Property(e => e.Zona_indic).HasColumnName("zona_indic");

            entity.HasOne(d => d.CatNavigation).WithMany(p => p.GmfIndicators)
                .HasForeignKey(d => d.Cat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCAT");
        });

        modelBuilder.Entity<GmfProtocol>(entity =>
        {
            entity.HasKey(e => new { e.Kn, e.God, e.Pindicator }).HasName("gmf_protocol_pkey");

            entity.ToTable("gmf_protocol", "gxav");

            entity.Property(e => e.Kn).HasColumnName("kn");
            entity.Property(e => e.God).HasColumnName("god");
            entity.Property(e => e.Pindicator)
                .HasMaxLength(5)
                .HasColumnName("pindicator");
            entity.Property(e => e.Aestimation)
                .HasMaxLength(10)
                .HasColumnName("aestimation");
            entity.Property(e => e.Bestimation)
                .HasMaxLength(10)
                .HasColumnName("bestamination");
            entity.Property(e => e.MarkEstam).HasColumnName("mark_estam");
            entity.Property(e => e.Pcat).HasColumnName("pcat");
            entity.Property(e => e.Prim)
                .HasMaxLength(250)
                .HasColumnName("prim");
            entity.Property(e => e.Opisanie).HasColumnName("opisanie");
            entity.Property(e => e.Date).HasColumnName("date_obs");

            entity.HasOne(d => d.KnNavigation).WithMany(p => p.GmfProtocols)
                .HasForeignKey(d => d.Kn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fpn");

            entity.HasOne(d => d.PindicatorNavigation).WithMany(p => p.GmfProtocols)
                .HasForeignKey(d => d.Pindicator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Find");
        });

        modelBuilder.Entity<GmfPunct>(entity =>
        {
            entity.HasKey(e => e.Kod).HasName("gmf_pn_pkey");

            entity.ToTable("gmf_punct", "gxav");

            entity.Property(e => e.Kod)
                .ValueGeneratedNever()
                .HasColumnName("kod");
            entity.Property(e => e.Bass)
                .HasMaxLength(150)
                .HasColumnName("bass");
            entity.Property(e => e.Gmf).HasColumnName("gmf");
            entity.Property(e => e.Gx).HasColumnName("gx");
            entity.Property(e => e.Lat).HasColumnName("lat");
            entity.Property(e => e.Lon).HasColumnName("lon");
            entity.Property(e => e.Pasp)
                .HasMaxLength(150)
                .HasColumnName("pasp");
            entity.Property(e => e.Punkt)
                .HasMaxLength(150)
                .HasColumnName("punkt");
            entity.Property(e => e.Reestr)
                .HasMaxLength(21)
                .HasColumnName("reestr");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            entity.Property(e => e.River)
                .HasMaxLength(150)
                .HasColumnName("river");
            entity.Property(e => e.Trans).HasColumnName("trans");
        });

        modelBuilder.Entity<GmfTotal>(entity =>
        {
            entity.HasKey(e => new { e.Tkn, e.Tgod }).HasName("gmf_total_pkey");

            entity.ToTable("gmf_total", "gxav");

            entity.Property(e => e.Tkn).HasColumnName("tkn");
            entity.Property(e => e.Tgod).HasColumnName("tgod");
            //entity.Property(e => e.All5est).HasColumnName("all5est");
            //entity.Property(e => e.Gr3est).HasColumnName("gr3est");
            //entity.Property(e => e.Sr3zonaball).HasColumnName("sr3zonaball");
            //entity.Property(e => e.Sr5ball).HasColumnName("sr5ball");
            //entity.Property(e => e.Zona3est).HasColumnName("zona3est");
            entity.Property(e => e.Bclass3).HasColumnName("Bclass3");
            entity.Property(e => e.Besb3).HasColumnName("Bsb3");
            entity.Property(e => e.Aclass5).HasColumnName("Aclass5");
            entity.Property(e => e.Aesb5).HasColumnName("Aesa5");
            entity.Property(e => e.GmfKlass).HasColumnName("gmfklass");
            entity.Property(e => e.Aesgr1).HasColumnName("Aesgr1");
            entity.Property(e => e.Aesgr2).HasColumnName("Aesgr2");
            entity.Property(e => e.Aesgr3).HasColumnName("Aesgr3");
            entity.Property(e => e.Besgr1).HasColumnName("Besgr1");
            entity.Property(e => e.Besgr2).HasColumnName("Besgr2");
            entity.Property(e => e.Besgr3).HasColumnName("Besgr3");
            entity.Property(e => e.Aeszona1).HasColumnName("Aeszona1");
            entity.Property(e => e.Aeszona2).HasColumnName("Aeszona2");
            entity.Property(e => e.Aeszona3).HasColumnName("Aeszona3");
            entity.Property(e => e.Beszona1).HasColumnName("Beszona1");
            entity.Property(e => e.Beszona2).HasColumnName("Beszona2");
            entity.Property(e => e.Beszona3).HasColumnName("Beszona3");

            entity.HasOne(e => e.gmfClass3).WithMany(p => p.gmfTotal)
                .HasForeignKey(d => d.Bclass3)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fclass3");

            entity.HasOne(e => e.gmfClass5).WithMany(p => p.gmfTotal)
                .HasForeignKey(d => d.Aclass5)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fclass5");

            entity.HasOne(e => e.gmfPunct).WithOne(p => p.gmfTotal)
                .HasForeignKey<GmfTotal>(d => d.Tkn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FT_kn");
        });

        modelBuilder.Entity<GmfZona>(entity =>
        {
            entity.HasKey(e => e.Nz).HasName("gmf_zona_pkey");

            entity.ToTable("gmf_zona", "gxav");

            entity.Property(e => e.Nz)
                .ValueGeneratedNever()
                .HasColumnName("nz");
            entity.Property(e => e.Grzona)
                .HasMaxLength(50)
                .HasColumnName("grzona");
            entity.Property(e => e.Namezoa)
                .HasMaxLength(50)
                .HasColumnName("namezoa");
        });


        modelBuilder.Entity<GmfRiver>(entity => 
        {
            entity.HasKey(e => new {e.namriver, e.riverbass}).HasName("gmf_river_pkey");

            entity.ToTable("gmf_river", "gxav");

            entity.Property(e => e.namriver).HasColumnName("namriver");
            entity.Property(e => e.riverbass).HasColumnName("riverbass");

            entity.HasOne(e => e.basses).WithOne(p => p.river)
                .HasForeignKey<GmfRiver>(e => e.riverbass)
                .HasPrincipalKey<GmfBass>(p => p.Nambass);
                
        });

        modelBuilder.Entity<GmfRegion>(entity => 
        {
            entity.ToTable("gmf_region", "gxav");
            entity.HasKey(e => new {e.tregion}).HasName("gmf_region_pkey");

            entity.Property(e => e.tregion).HasColumnName("tregion");
        });

        modelBuilder.Entity<GmfBass>(entity => 
        {
            entity.ToTable("gmf_bass", "gxav");
            entity.HasKey(e => new {e.Nambass}).HasName("gmf_bass_pkey");

            entity.Property(e => e.Nambass).HasColumnName("Nambass");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
