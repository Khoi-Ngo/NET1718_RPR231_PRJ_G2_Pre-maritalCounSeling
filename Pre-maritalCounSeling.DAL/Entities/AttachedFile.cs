﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Pre_maritalCounSeling.DAL.Base;
using System;
using System.Collections.Generic;

namespace Pre_maritalCounSeling.DAL.Entities;

public partial class AttachedFile : BaseEntity
{
    public long Id { get; set; }

    public string Type { get; set; }

    public string ReferedLink { get; set; }

    public long? FeedbackId { get; set; }
    public long? ScheduleId { get; set; }
    public virtual Feedback Feedback { get; set; }
    public virtual Schedule Schedule { get; set; }
}