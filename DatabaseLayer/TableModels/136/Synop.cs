using System;
using System.Collections.Generic;
using DatabaseLayer.ModelInterface;
using GeneralObject.MyCustomAttribute;

namespace DB.TableModels;

/// <summary>
/// Метеорологическая информация (код КН01)
/// </summary>
/// 
[TableNameAttribute(TableName = "SYNOP")]
public partial class Synop : ISynopProperty
{
    [StationIdAttribute]

    //public int StationId { get; set; }
    public int ResourceId { get; set; }

    [DateObservationAttribute]

    //public DateTime DateObs { get; set; }
    public DateTime DateObservation { get; set; }
    public DateTime DateWrite { get; set; }

    public decimal? TempAir { get; set; }

    //public decimal? TempDp { get; set; }

    //public decimal? TempDbMax { get; set; }

    //public decimal? TempDbMin { get; set; }

    //public int? TempSoil { get; set; }

    //public int? TempSurf { get; set; }

    public int? WindSp { get; set; }

    public int? DirectW { get; set; }

    //public int? SnowHei { get; set; }

    public int? Precip { get; set; }

    public int? Visby { get; set; }

    public decimal? Pressure { get; set; }

    //public decimal? PresSl { get; set; }

    //public int? SpecPhen1 { get; set; }

    //public int? SpecPhen2 { get; set; }

    //public int? SpecPhen3 { get; set; }

    //public int? SpecPhen4 { get; set; }

    //public int? SpecPhen5 { get; set; }

    //public int? TypeStation { get; set; }
    //public int? Wwterm { get; set; }

    //public int? TempMin2 { get; set; }

    /// <summary>
    /// Значение - определение
    /// 0 - не изменилось
    /// 1 - растет
    /// 2 - падает
    /// </summary>
    //public int? BtendCh { get; set; }

    /// <summary>
    /// Значение - определение
    /// 0 - ясно
    /// 1 - облачно
    /// 2 - облачно с прояснениями
    /// </summary>
    //public int? CldTot { get; set; }

    //public int? ImgWwCode { get; set; }

    //public int? WndMax { get; set; }

    /// <summary>
    /// Индекс комфортности
    /// </summary>
    //public decimal? TempEf { get; set; }

    /// <summary>
    /// относительная влажность
    /// </summary>
    public decimal? RelHum { get; set; }
}
