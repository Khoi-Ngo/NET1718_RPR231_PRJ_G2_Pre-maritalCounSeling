﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Pre_maritalCounSeling.DAL.Base;
using System;
using System.Collections.Generic;

namespace Pre_maritalCounSeling.DAL.Entities;

public partial class ScheduleUser : BaseEntity
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public long ScheduleId { get; set; }

    public virtual User Customer { get; set; }

    public virtual Schedule Schedule { get; set; }
}