﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Pre_maritalCounSeling.DAL.Base;
using System;
using System.Collections.Generic;

namespace Pre_maritalCounSeling.DAL.Entities;

public partial class Role : BaseEntity
{
    public short Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; } = "Undefined";

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}