using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace open_auburn_api.Models
{
    public partial class Crime
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("incident_id")]
        public string? IncidentId { get; set; }
        [Column("incident_type")]
        public string? IncidentType { get; set; }
        [Column("clery_class")]
        public string? CleryClass { get; set; }
        [Column("notes")]
        public string? Notes { get; set; }
        [Column("disposition")]
        public string? Disposition { get; set; }
        [Column("supplement_disposition")]
        public string? SupplementDisposition { get; set; }
        [Column("supplemented_on", TypeName = "date")]
        public DateTime? SupplementedOn { get; set; }
        [Column("supplement_disposition_2")]
        public string? SupplementDisposition2 { get; set; }
        [Column("supplemented_on_2", TypeName = "date")]
        public DateTime? SupplementedOn2 { get; set; }
        [Column("reported_on", TypeName = "date")]
        public DateTime? ReportedOn { get; set; }
        [Column("started_at", TypeName = "datetime")]
        public DateTime? StartedAt { get; set; }
        [Column("ended_at", TypeName = "datetime")]
        public DateTime? EndedAt { get; set; }
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
