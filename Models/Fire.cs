using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace open_auburn_api.Models
{
    [Table("Fire")]
    public partial class Fire
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("OAID")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Oaid { get; set; }
        [Column("IncidentID")]
        public string? IncidentId { get; set; }
        public string? Campus { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FireDate { get; set; }
        public TimeSpan? FireTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateReported { get; set; }
        public string? Description { get; set; }
        public string? Cause { get; set; }
        public string? DamageCost { get; set; }
        public int? Injuries { get; set; }
        public int? Deaths { get; set; }
        public string? Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
