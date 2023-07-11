using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DatabaseLayer.ModelInterface;
using GeneralObject.MyCustomAttribute;

namespace DB.TableModels;

[TableNameAttribute(TableName = "MeasuringAMS")]
public partial class MeasuringAm : ISynopProperty
{
    [DateObservationAttribute]
    //public DateTime DateTime { get; set; }
    public DateTime DateObservation { get; set; }

    [StationIdAttribute]
    //public int StationId { get; set; }
    public int ResourceId { get; set; }

    public decimal? TempAir { get; set; }

    public int? RelHum { get; set; }

    //public decimal? DewPoint { get; set; }

    public decimal? Pressure { get; set; }

    public int? Visby { get; set; }

    //public decimal? IntenPrecip { get; set; }

    //public decimal? TempSoil3 { get; set; }

    //public decimal? TempUndSurf { get; set; }

    //public decimal? TempAir2 { get; set; }

    //public decimal? TempWater { get; set; }

    //public int? LevelW { get; set; }
    
    //public decimal? TempSoil0 { get; set; }

    public decimal? WindSp { get; set; }

    public int? DirectW { get; set; }

    //public decimal? SpeedWmax { get; set; }

    //public decimal? Pvapor { get; set; }

    //public decimal? LackSatur { get; set; }

    //public int? PstLevMm { get; set; }

    /// <summary>
    /// Сумма осадков с начала метеосуток
    /// </summary>
    public decimal? Precip { get; set; }

    //public decimal? PrecipSol { get; set; }

    //public int? CodeW { get; set; }

    /// <summary>
    /// 0 - не изменяется
    /// 1 - растет
    /// 2 - падает
    /// </summary>
    //public int? BtendCh { get; set; }

    //public decimal? PresSl { get; set; }

    /// <summary>
    /// Индекс комфортности
    /// </summary>
    //public decimal? TempEf { get; set; }

    //public DateTime? DateWrite { get; set; }
    public DateTime DateWrite { get; set; }

    /// <summary>
    /// Cумма осадков за 10 минут
    /// </summary>
    //public decimal? Precip10 { get; set; }
}
