﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Pre_maritalCounSeling.DAL.Base;
using System;
using System.Collections.Generic;

namespace Pre_maritalCounSeling.DAL.Entities;

public partial class QuizResultDetail : BaseEntity
{
    public long Id { get; set; }

    public long QuizResultId { get; set; }

    //can be stored as choice or text
    public string UserAnswer { get; set; }
    //can be stored as choice or text
    public string RecommendedAnswer { get; set; }

    public virtual QuizResult QuizResult { get; set; }
}