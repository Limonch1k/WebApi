﻿using System;
using System.Collections.Generic;

namespace DatabaseLayer.DBModel;

public partial class GmfTotalDB
{
    public int Tkn { get; set; }

    public int Tgod { get; set; }


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

    public virtual GmfPunctDB gmfPunct {get;set;} = null!;

    public virtual GmfClass3DB gmfClass3 {get;set;} = null!;

    public virtual GmfClass5DB gmfClass5 {get;set;} = null!;
}
