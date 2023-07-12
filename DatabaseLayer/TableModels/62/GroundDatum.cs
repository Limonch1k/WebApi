using DatabaseLayer.ModelInterface;
using GeneralObject.MyCustomAttribute;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.TableModels;

public partial class GroundDatum  : ISynopProperty
{
    [DateObservationAttribute]
    public DateTime DateObservation { get; set; }

    [StationIdAttribute] 
    public int ResourceId { get; set; }

    public decimal AdvanceTime { get; set; }

    public int ModelId { get; set; }

    public decimal? WindU { get; set; }

    public decimal? WindV { get; set; }

    [NotMapped]
    public double? WindSp 
    { 
        get 
        {
            return Math.Sqrt((double)this.WindU + (double)this.WindSp);
        } 
        set 
        {
            ; 
        }
    }

    public decimal? Pressure { get; set; }

    public decimal? TempAir { get; set; }

    public decimal? RelHum { get; set; }

    public decimal? Precip { get; set; }

    public decimal? Visby { get; set; }

    public decimal? DirectW { get; set; }

    public DateTime DateWrite { get; set; }
}
