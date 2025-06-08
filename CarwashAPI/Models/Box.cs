using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarwashAPI.Models;

public partial class Box
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("spotCount")]
    public int SpotCount { get; set; }

    [Column("internalDimensions")]
    [StringLength(8)]
    [Unicode(false)]
    public string? InternalDimensions { get; set; }

    [InverseProperty("Box")]
    public virtual ICollection<Spot> Spots { get; set; } = new List<Spot>();
}
