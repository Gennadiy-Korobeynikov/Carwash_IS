using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarwashAPI.Models;

[Index("SpotId", Name = "IX_Relationship4")]
[Index("EmployeeId", Name = "IX_Какому мойщику назначено")]
[Index("ClientId", Name = "IX_Кто записан")]
[Index("StatusId", Name = "IX_Текущий статус")]
[Index("DateTime", "SpotId", Name = "UQ_Appointments_", IsUnique = true)]
public partial class Appointment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("dateTime", TypeName = "datetime")]
    public DateTime DateTime { get; set; }

    [Column("cost", TypeName = "decimal(12, 2)")]
    public decimal Cost { get; set; }

    [Column("clientId")]
    public int ClientId { get; set; }

    [Column("employeeId")]
    public int EmployeeId { get; set; }

    [Column("statusId")]
    public int StatusId { get; set; }

    [Column("spotId")]
    public int SpotId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Appointments")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("EmployeeId")]
    [InverseProperty("Appointments")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("SpotId")]
    [InverseProperty("Appointments")]
    public virtual Spot Spot { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Appointments")]
    public virtual Status Status { get; set; } = null!;

    [ForeignKey("AppointmentId")]
    [InverseProperty("Appointments")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
