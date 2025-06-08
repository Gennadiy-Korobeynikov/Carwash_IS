using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarwashAPI.Models;

public partial class PriceChange
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("newPrice", TypeName = "decimal(7, 2)")]
    public decimal NewPrice { get; set; }

    [Column("dateTime", TypeName = "datetime")]
    public DateTime DateTime { get; set; }

    [Column("serviceId")]
    public int ServiceId { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("PriceChanges")]
    public virtual Service Service { get; set; } = null!;
}
