using System;
using System.Collections.Generic;
using DatabaseLayer.IDbContext;
using DB.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DBLayer.Context;

public partial class NWPContext: DbContext, IDbContext<DbSet<GroundDatum>>
{
    private ILogger _logger { get; set; }

    public NWPContext(ILoggerProvider loggerProvider)
    {
        _logger = loggerProvider.CreateLogger("NWPDbLogger");
    }

    public NWPContext(DbContextOptions<NWPContext> options, ILoggerProvider _loggerProvider)
        : base(options)
    {
        _logger = _loggerProvider.CreateLogger("NWPDbLogger");
    }

    public DbSet<GroundDatum> SetTable()
    {
        return this.GroundData;
    }

    public virtual DbSet<GroundDatum> GroundData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    {
        optionsBuilder.UseOracle("User Id=nwp;Password=modeli;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=192.168.211.62)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=serv)));");
        Action<string> a = str =>
        {
            _logger.LogTrace(str);
            //Console.WriteLine(str);
        };
        optionsBuilder.LogTo(a);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("NWP")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<GroundDatum>(entity =>
        {
            entity.HasKey(e => new { e.DateObservation, e.ResourceId, e.AdvanceTime, e.ModelId }).HasName("GROUND_DATA_PK");

            entity.ToTable("GROUND_DATA");

            //.HasColumnType("NUMBER") можно менять NUMBER(10,0)
            //.HasColumnType("NUMBER(10,10)") можно менять NUMBER(10,0)
            entity.Property(e => e.DateObservation)
                .HasColumnType("DATE")
                .HasColumnName("DATAS");
            entity.Property(e => e.ResourceId)
                .HasColumnType("NUMBER(10,0)")
                .HasColumnName("CITY_ID")
                .HasConversion<int>();
            entity.Property(e => e.AdvanceTime)
                .HasColumnType("NUMBER")
                .HasColumnName("ADVANCE_TIME");
            entity.Property(e => e.ModelId)
                .HasColumnType("NUMBER(10,0)")
                .HasColumnName("MODEL_ID")
                .HasConversion<int>();
            /*entity.Property(e => e.CloudConv)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("CLOUD_CONV");
            entity.Property(e => e.CloudLow)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("CLOUD_LOW");
            entity.Property(e => e.CloudMid)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("CLOUD_MID");
            entity.Property(e => e.CloudTotal)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("CLOUD_TOTAL");
            entity.Property(e => e.CloudUpp)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("CLOUD_UPP");
            entity.Property(e => e.DPDeficit)
                .HasColumnType("NUMBER")
                .HasColumnName("D_P_DEFICIT");
            entity.Property(e => e.DateUpdate)
                .HasColumnType("DATE")
                .HasColumnName("DATE_UPDATE");*/
            entity.Property(e => e.DateWrite)
                .HasColumnType("DATE")
                .HasColumnName("DATE_WRITE");
            /*entity.Property(e => e.FogAdvKosh)
                .HasColumnType("NUMBER")
                .HasColumnName("FOG_ADV_KOSH");
            entity.Property(e => e.FogRad)
                .HasColumnType("NUMBER")
                .HasColumnName("FOG_RAD");
            entity.Property(e => e.GrozaReshetov)
                .HasColumnType("NUMBER")
                .HasColumnName("GROZA_RESHETOV");
            entity.Property(e => e.GrozaSlavin)
                .HasColumnType("NUMBER")
                .HasColumnName("GROZA_SLAVIN");
            entity.Property(e => e.GrozaVaiting)
                .HasColumnType("NUMBER")
                .HasColumnName("GROZA_VAITING");
            entity.Property(e => e.HeightZero)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("HEIGHT_ZERO");
            entity.Property(e => e.Ice)
                .HasColumnType("NUMBER")
                .HasColumnName("ICE");
            entity.Property(e => e.IceSnitk)
                .HasColumnType("NUMBER")
                .HasColumnName("ICE_SNITK");
            entity.Property(e => e.PrecipContin)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("PRECIP_CONTIN");
            entity.Property(e => e.PrecipConv)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("PRECIP_CONV");*/
            entity.Property(e => e.PrecipTotal)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("PRECIP_TOTAL");
            entity.Property(e => e.Pressure)
                .HasColumnType("NUMBER(8,3)")
                .HasColumnName("PRESSURE");
            entity.Property(e => e.RHumid)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("R_HUMID");
            /*entity.Property(e => e.RHumidZero)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("R_HUMID_ZERO");
            entity.Property(e => e.SHumid)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("S_HUMID");
            entity.Property(e => e.Temp)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("TEMP");
            entity.Property(e => e.TempMax)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("TEMP_MAX");
            entity.Property(e => e.TempMin)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("TEMP_MIN");
            entity.Property(e => e.Visib)
                .HasColumnType("NUMBER(9,3)")
                .HasColumnName("VISIB");
            entity.Property(e => e.WindSp)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("WIND_SP");*/
            entity.Property(e => e.WindU)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("WIND_U");
            /*entity.Property(e => e.WindU100)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("WIND_U100");*/
            entity.Property(e => e.WindV)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("WIND_V");
            /*entity.Property(e => e.WindV100)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("WIND_V100");*/
            entity.Property(e => e.Winddirection)
                .HasColumnType("NUMBER")
                .HasColumnName("WINDDIRECTION");
            /*entity.Property(e => e.Winddirection100)
                .HasColumnType("NUMBER")
                .HasColumnName("WINDDIRECTION100");*/
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
