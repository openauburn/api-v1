using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace open_auburn_api.Models
{
    public partial class Fire
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("incident_id")]
        public string? IncidentId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("cause")]
        public string? Cause { get; set; }
        [Column("damage_cost")]
        public string? DamageCost { get; set; }
        [Column("injuries")]
        public int? Injuries { get; set; }
        [Column("deaths")]
        public int? Deaths { get; set; }
        [Column("reported_on", TypeName = "date")]
        public DateTime? ReportedOn { get; set; }
        [Column("occurred_at", TypeName = "datetime")]
        public DateTime? OccurredAt { get; set; }
        [Column("campus")]
        public string? Campus { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("latitude")]
        public double? Latitude { get; set; }
        [Column("longitude")]
        public double? Longitude { get; set; }
    }
}
