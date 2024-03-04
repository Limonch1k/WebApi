using System;
using System.Collections.Generic;

namespace BusinessLayer.Models;

public partial class GmfTotalBL
{
   public int Tkn { get; set; }

    public int Tgod { get; set; }

    public short? Gr3est { get; set; }

    public int? All5est { get; set; }

    public int?  Bclass3 {get;set;}

    public decimal? Besb3 {get;set;}

    public decimal? Besgr1 {get;set;}

    public decimal? Besgr2 {get;set;}

    public decimal? Besgr3 {get;set;}

    public decimal? Aesb5 {get;set;}

    public decimal? Aesgr1 {get;set;}

    public decimal? Aesgr2 {get;set;}

    public decimal? Aesgr3 {get;set;}

    public int? Aclass5 {get;set;}


    public decimal? Aeszona1 {get;set;}

    public decimal? Aeszona2 {get;set;}

    public decimal? Aeszona3 {get;set;}

    public decimal? Beszona1 {get;set;}

    public decimal? Beszona2 {get;set;}

    public decimal? Beszona3 {get;set;}

    public int? GmfKlass {get;set;}

    public decimal? Sr3zonaball { get; set; }

    public decimal? Sr5ball { get; set; }

    public short? Zona3est { get; set; }

    public virtual GmfPunctBL gmfPunct {get;set;} = null!;

    public virtual GmfClass3BL gmfClass3 {get;set;} = null!;

    public virtual GmfClass5BL gmfClass5 {get;set;} = null!;
}
