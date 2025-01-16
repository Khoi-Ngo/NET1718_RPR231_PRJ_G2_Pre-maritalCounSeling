﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Pre_maritalCounSeling.DAL.Base;
using System;
using System.Collections.Generic;

namespace Pre_maritalCounSeling.DAL.Entities;

public partial class Service : BaseEntity
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public string Currency { get; set; }

    public double? Commission { get; set; }

    public double? CommissionRate { get; set; }

    public double? EstimatedDuration { get; set; }

    public string? DurationUnit { get; set; }

    public long ExpertId { get; set; }

    public long CategoryId { get; set; }

    public string? Image { get; set; }

    public string ServiceCode { get; set; } = "SERVICE-" + Guid.NewGuid().ToString().ToUpper();

    public double? AvgRating { get; set; }

    public virtual ServiceCategory Category { get; set; }

    public virtual User Expert { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}