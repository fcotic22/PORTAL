﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class OrnamentGlass
{
    public int id { get; set; }

    public string name { get; set; }

    public string glassThicknesses { get; set; }

    public virtual ICollection<GlazedGlass> GlazedGlasses { get; set; } = new List<GlazedGlass>();
}