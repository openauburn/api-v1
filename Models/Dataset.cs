using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace open_auburn_api.Models
{
    public partial class Dataset
    {
        [Key]
        [Column("id")]
        public int? Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("url")]
        public string? Url { get; set; }
        [Column("license")]
        public string? License { get; set; }
        [Column("source_url")]
        public string? SourceUrl { get; set; }
    }
}
