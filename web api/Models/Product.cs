using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace web_api.Models;

[Table("product")]
public partial class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("price")]
    [Precision(10, 2)]
    public decimal Price { get; set; }

    [Column("shelflife")]
    public int? Shelflife { get; set; }
}
