using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarwashAPI.Models;

[Index("EmployeeNumber", Name = "UQ_Employees_employeeNumber", IsUnique = true)]
public partial class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("lastName")]
    [StringLength(255)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("firstName")]
    [StringLength(255)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [Column("midName")]
    [StringLength(255)]
    [Unicode(false)]
    public string? MidName { get; set; }

    [Column("employmentDate")]
    public DateOnly EmploymentDate { get; set; }

    [Column("employeeNumber")]
    public int EmployeeNumber { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
