using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarwashAPI.Models;

public partial class Spot
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("number")]
    public int Number { get; set; }

    [Column("boxId")]
    public int BoxId { get; set; }

    [InverseProperty("Spot")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [ForeignKey("BoxId")]
    [InverseProperty("Spots")]
    public virtual Box Box { get; set; } = null!;
}
