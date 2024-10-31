using MappingApp.Interfaces;
using System;
using System.Collections.Generic;

namespace MappingApp.Models;

public partial class PointDto : BaseEntity
{
    public double PointX { get; set; }

    public double PointY { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
