using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarwashAPI.Models;

[Index("Name", Name = "UQ_Services_name", IsUnique = true)]
public partial class Service
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("price", TypeName = "decimal(7, 2)")]
    public decimal Price { get; set; }

    [Column("duration")]
    public int Duration { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<PriceChange> PriceChanges { get; set; } = new List<PriceChange>();

    [ForeignKey("ServiceId")]
    [InverseProperty("Services")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
