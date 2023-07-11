using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PL.PLModels;

/// <summary>
/// Метеорологическая информация (код КН01)
/// </summary>
[Serializable]
[XmlRoot(ElementName = "MeteoData")]
public class SynopPL
{
    public int StationId { get; set; }

    public DateTime DateObs { get; set; }

    public DateTime DateWrite { get; set; }


    public decimal? TempDb { get; set; }

    public bool ShouldSerializeTempDb()
    {
        return TempDb.HasValue;
    }

    public decimal? TempDp { get; set; }

    public bool ShouldSerializeTempDp()
    {
        return TempDb.HasValue;
    }

    public decimal? TempDbMax { get; set; }

    public bool ShouldSerializeTempDbMax()
    {
        return TempDb.HasValue;
    }

    public decimal? TempDbMin { get; set; }

    public bool ShouldSerializeTempDbMin()
    {
        return TempDb.HasValue;
    }

    public int? TempSoil { get; set; }

    public bool ShouldSerializeTempSoil()
    {
        return TempDb.HasValue;
    }

    public int? TempSurf { get; set; }

    public bool ShouldSerializeTempSurf()
    {
        return TempDb.HasValue;
    }

    public int? WndSp { get; set; }

    public bool ShouldSerializeWndSp()
    {
        return TempDb.HasValue;
    }

    public int? WndDir { get; set; }

    public bool ShouldSerializeWndDir()
    {
        return TempDb.HasValue;
    }

    public int? SnowHei { get; set; }

    public bool ShouldSerializeSnowHei()
    {
        return TempDb.HasValue;
    }

    public int? Precip { get; set; }

    public bool ShouldSerializePrecip()
    {
        return TempDb.HasValue;
    }

    public int? Visby { get; set; }

    public bool ShouldSerializeVisby()
    {
        return TempDb.HasValue;
    }

    public decimal? PresSt { get; set; }

    public bool ShouldSerializePresSt()
    {
        return TempDb.HasValue;
    }

    public decimal? PresSl { get; set; }

    public bool ShouldSerializePresSl()
    {
        return TempDb.HasValue;
    }

    public int? SpecPhen1 { get; set; }

    public bool ShouldSerializeSpecPhen1()
    {
        return TempDb.HasValue;
    }

    public int? SpecPhen2 { get; set; }

    public bool ShouldSerializeSpecPhen2()
    {
        return TempDb.HasValue;
    }

    public int? SpecPhen3 { get; set; }

    public bool ShouldSerializeSpecPhen3()
    {
        return TempDb.HasValue;
    }

    public int? SpecPhen4 { get; set; }

    public bool ShouldSerializeSpecPhen4()
    {
        return TempDb.HasValue;
    }

    public int? SpecPhen5 { get; set; }

    public bool ShouldSerializePhen5()
    {
        return TempDb.HasValue;
    }

    public int? TypeStation { get; set; }

    public bool ShouldSerializeTypeStation()
    {
        return TempDb.HasValue;
    }

    public int? Wwterm { get; set; }

    public bool ShouldSerializeWwterm()
    {
        return TempDb.HasValue;
    }

    public int? TempMin2 { get; set; }

    public bool ShouldSerializeTempMin2()
    {
        return TempDb.HasValue;
    }

    /// <summary>
    /// Значение - определение
    /// 0 - не изменилось
    /// 1 - растет
    /// 2 - падает
    /// </summary>
    public int? BtendCh { get; set; }

    public bool ShouldSerializeBtendCh()
    {
        return TempDb.HasValue;
    }

    /// <summary>
    /// Значение - определение
    /// 0 - ясно
    /// 1 - облачно
    /// 2 - облачно с прояснениями
    /// </summary>
    public int? CldTot { get; set; }

    public bool ShouldSerializeCldTot()
    {
        return TempDb.HasValue;
    }

    public int? ImgWwCode { get; set; }

    public bool ShouldSerializeImgWwCode()
    {
        return TempDb.HasValue;
    }

    public int? WndMax { get; set; }

    public bool ShouldSerializeWndMax()
    {
        return TempDb.HasValue;
    }

    /// <summary>
    /// Индекс комфортности
    /// </summary>
    public decimal? TempEf { get; set; }

    public bool ShouldSerializeTempEf()
    {
        return TempDb.HasValue;
    }

    /// <summary>
    /// относительная влажность
    /// </summary>
    public decimal? RelHum { get; set; }

    public bool ShouldSerializeRelHum()
    {
        return TempDb.HasValue;
    }
}
