using System;
using System.Collections.Generic;
using DatabaseLayer.IDbContext;
using DB.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DBLayer.Context;

public partial class MeteoContext : DbContext, IDbContext<DbSet<Synop>>, IDbContext<DbSet<MeasuringAm>>
{
    private ILogger _logger {get;set;}

    public MeteoContext(ILoggerProvider loggerProvider)
    {
        _logger = loggerProvider.CreateLogger("MeteoDbLogger");
    }

    public MeteoContext(DbContextOptions<MeteoContext> options, ILoggerProvider loggerProvider)
        : base(options)
    {
        _logger = loggerProvider.CreateLogger("MeteoDbLogger");
    }

    public virtual DbSet<Synop> Synops { get; set; }
    public virtual DbSet<MeasuringAm> MeasuringAms { get; set; }

    public DbSet<Synop> SetTable()
    {
        return this.Synops;
    }

    DbSet<MeasuringAm> IDbContext<DbSet<MeasuringAm>>.SetTable()
    {
        return this.MeasuringAms;
    }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    {
        optionsBuilder.UseNpgsql("Host=192.168.211.136;Port=5432;Database=meteoportal_db;Username=meteo;Password=3HFM4mO6NIJK5k2");
        Action<string> a = str =>
        {
            _logger.LogTrace(str);
            //Console.WriteLine(str);
        };
        optionsBuilder.LogTo(a);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<MeasuringAm>(entity =>
        {
            entity.HasKey(e => new { e.DateObservation, e.ResourceId }).HasName("MeasuringAMS_pkey");

            entity.ToTable("MeasuringAMS", "meteo");

            entity.Property(e => e.DateObservation).HasColumnType("timestamp without time zone");
            /*entity.Property(e => e.BtendCh)
                .HasComment("0 - не изменяется\n1 - растет\n2 - падает")
                .HasColumnName("BTendCh");*/
            entity.Property(e => e.DateWrite)
                .HasDefaultValueSql("(now() - '03:00:00'::interval)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Date_write");
            //entity.Property(e => e.DewPoint).HasPrecision(3, 1);
            //entity.Property(e => e.IntenPrecip).HasPrecision(6, 2);
            entity.Property(e => e.LackSatur).HasPrecision(3, 1);
            entity.Property(e => e.Precip)
                .HasPrecision(4, 1)
                .HasComment("Сумма осадков с начала метеосуток");
            /*entity.Property(e => e.Precip10)
                .HasPrecision(4, 1)
                .HasComment("Cумма осадков за 10 минут");*/
            //entity.Property(e => e.PrecipSol).HasPrecision(4, 1);
            /*entity.Property(e => e.PresSl)
                .HasPrecision(5, 1)
                .HasColumnName("PresSL");*/
            entity.Property(e => e.PstLevGPa)
                .HasPrecision(5, 1)
                .HasColumnName("PStLev_gPa");
            entity.Property(e => e.PstLevMm).HasColumnName("PStLev_mm");
            entity.Property(e => e.Pvapor)
                .HasPrecision(5, 2)
                .HasColumnName("PVapor");
            entity.Property(e => e.SpeedW).HasPrecision(3, 1);
            entity.Property(e => e.SpeedWmax).HasPrecision(3, 1);
            entity.Property(e => e.TempAir).HasPrecision(3, 1);
            /*entity.Property(e => e.TempAir2)
                .HasPrecision(3, 1)
                .HasColumnName("TempAir_2");*/
            /*entity.Property(e => e.TempEf)
                .HasPrecision(3, 1)
                .HasComment("Индекс комфортности");
            entity.Property(e => e.TempSoil0)
                .HasPrecision(3, 1)
                .HasColumnName("TempSoil_0");
            entity.Property(e => e.TempSoil3)
                .HasPrecision(3, 1)
                .HasColumnName("TempSoil_3");*/
            entity.Property(e => e.TempUndSurf).HasPrecision(3, 1);
            //entity.Property(e => e.TempWater).HasPrecision(5, 1);
        });

        modelBuilder.Entity<Synop>(entity =>
        {
            entity.HasKey(e => new { e.ResourceId, e.DateObservation });

            entity.ToTable("SYNOP", "meteo", tb => tb.HasComment("Метеорологическая информация (код КН01)"));
            ///
            entity.Property(e => e.ResourceId).HasColumnName("Station_id");
            ///
            entity.Property(e => e.DateObservation)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Date_obs");
            ///
            /*entity.Property(e => e.BtendCh)
                .HasComment("Значение - определение\n0 - не изменилось\n1 - растет\n2 - падает")
                .HasColumnName("BTendCh");*/
            //entity.Property(e => e.CldTot).HasComment("Значение - определение\n0 - ясно\n1 - облачно\n2 - облачно с прояснениями");
            entity.Property(e => e.DateWrite)
                .HasDefaultValueSql("(now() - '03:00:00'::interval)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Date_write");
            //entity.Property(e => e.ImgWwCode).HasColumnName("ImgWW_code");
            //entity.Property(e => e.PresSl).HasPrecision(6, 1);
            entity.Property(e => e.PresSt).HasPrecision(6, 1);
            entity.Property(e => e.RelHum)
                .HasPrecision(5, 1)
                .HasComment("относительная влажность");
            entity.Property(e => e.TempDb).HasPrecision(5, 1);
            //entity.Property(e => e.TempDbMax).HasPrecision(5, 1);
            //entity.Property(e => e.TempDbMin).HasPrecision(5, 1);
            //entity.Property(e => e.TempDp).HasPrecision(5, 1);
            /*entity.Property(e => e.TempEf)
                .HasPrecision(3, 1)
                .HasComment("Индекс комфортности");*/
            //entity.Property(e => e.Wwterm).HasColumnName("WWterm");
        });
        modelBuilder.HasSequence("air_id_seq", "collect");
        modelBuilder.HasSequence("assertive_events_id_seq", "collect");
        modelBuilder.HasSequence("categories_gid_seq");
        modelBuilder.HasSequence("colors_gid_seq");
        modelBuilder.HasSequence("hydrology_id_seq", "collect");
        modelBuilder.HasSequence("info_message_id_seq", "collect");
        modelBuilder.HasSequence("layers_gid_seq");
        modelBuilder.HasSequence("layers_nc_gid_seq");
        modelBuilder.HasSequence("layers_wrf_gid_seq");
        modelBuilder.HasSequence("legends_gid_seq");
        modelBuilder.HasSequence("log_admin_id_seq", "collect");
        modelBuilder.HasSequence("naspunkt_id_seq");
        modelBuilder.HasSequence("punkttype_id_seq");
        modelBuilder.HasSequence("radiation_id_seq", "collect");
        modelBuilder.HasSequence("region_id_seq");
        modelBuilder.HasSequence("roles_gid_seq");
        modelBuilder.HasSequence("synoptic_seq");
        modelBuilder.HasSequence("synoptic_user_seq");
        modelBuilder.HasSequence("user_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
