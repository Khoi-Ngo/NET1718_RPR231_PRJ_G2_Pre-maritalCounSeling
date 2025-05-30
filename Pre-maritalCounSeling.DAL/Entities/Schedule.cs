﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Pre_maritalCounSeling.DAL.Base;
using System;
using System.Collections.Generic;

namespace Pre_maritalCounSeling.DAL.Entities;

public partial class Schedule : BaseEntity
{
    public long Id { get; set; }

    public long ServiceId { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime Time { get; set; }

    public long ScheduleTypeId { get; set; }

    public string? ExpertNote { get; set; }
    //auto generating the gg meeting link (if possible)
    public string? ReferedLink { get; set; }

    public string? CustomerNote { get; set; }

    public string? ExpertResponse { get; set; }

    public DateTime? RescheduledTime { get; set; }

    public short RescheduledCount { get; set; } = 0;

    public string? AppointmentMode { get; set; }

    public string? Location { get; set; }

    public string? Priority { get; set; }

    public bool ReminderSent { get; set; } = false;

    public virtual ScheduleType ScheduleType { get; set; }

    public virtual ICollection<ScheduleUser> ScheduleUsers { get; set; } = new List<ScheduleUser>();
    public virtual ICollection<AttachedFile> AttachedFiles { get; set; } = new List<AttachedFile>();

    public virtual Service Service { get; set; }
}