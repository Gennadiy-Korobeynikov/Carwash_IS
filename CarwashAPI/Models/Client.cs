using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarwashAPI.Models;

public partial class Client
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("telNumber")]
    [StringLength(11)]
    [Unicode(false)]
    public string TelNumber { get; set; } = null!;

    [Column("name")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("lastName")]
    [StringLength(255)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("firstName")]
    [StringLength(255)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("midName")]
    [StringLength(255)]
    [Unicode(false)]
    public string? MidName { get; set; }

    [Column("carInfo")]
    [StringLength(512)]
    [Unicode(false)]
    public string? CarInfo { get; set; }

    [Column("preferences")]
    [StringLength(512)]
    [Unicode(false)]
    public string? Preferences { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
